using Database.SQLStatements;
using Database.SQLStatements.DML;
using Database.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.QueryEngine
{
    class Optimiser
    {
        List<Statement> queryOrder = new List<Statement>();

        // Optimise the query.
        public List<Statement> OptimiseSelectWithJoins(Db db, Select select )
        {
            Table person = db.GetTable("Person");
            // If there is a where clause
            if (select.Where != null)
            {
                select.Where.Execute(db);
                // If Unique - perform first - this will affect the cardinality of the joins
                if (select.Where.Column_Column.IsUnique)
                {
                    Table whereTable = new Table();
                    whereTable.ColumnList.AddRange(select.Where.Table.ColumnList);
                    whereTable.Rows = select.Where.GetMatchingRows(db);
                    queryOrder.Add(select.Where);
                    EstimateJoins(db, select, whereTable);
                }
                // if not - perform last
                else
                {
                    EstimateJoins(db, select, null);
                    queryOrder.Add(select.Where);
                }
            }
            else
                EstimateJoins(db, select, null);

            return queryOrder;
        }

        public void EstimateJoins(Db db, Select select, Table whereTable)
        {
            // Now we need to estimate an optimal join order
            // First - estimate how each predicate affects the cardinality.
            foreach (Join join in select.Joins)
            {
                join.Execute(db);
                int selectivity = EstimateJoinSelectivity(join, whereTable) ;
                join.Selectivity = selectivity;
            }

            // Get the best join to start
            Join firstJoin = select.Joins.OrderBy(x => x.Selectivity).FirstOrDefault();
            select.Joins.Remove(firstJoin);
            queryOrder.Add(firstJoin);

            // All subsequent joins must be able to chain to eachother
            // Find the least selectivity out of the remaining connecting predicates
            for (int i = 0; i < select.Joins.Count; i++)
            {
                Join join = select.Joins.Where(x => x.Table1 == ((Join)queryOrder.Last()).Table1
                                                    || x.Table1 == ((Join)queryOrder.Last()).Table2
                                                    || x.Table2 == ((Join)queryOrder.Last()).Table1
                                                    || x.Table2 == ((Join)queryOrder.Last()).Table2
                                                ).OrderBy(x => x.Selectivity).FirstOrDefault();
                queryOrder.Add(join);
            }
        }

        /// <summary>
        /// Calculate the worst case selectivity of the predicate
        /// </summary>
        /// <param name="join"></param>
        /// <returns></returns>
        private int EstimateJoinSelectivity(Join join, Table whereTable)
        {
            if (whereTable != null)
            {
                if (join.Table1.TableName.Equals(whereTable.TableName))
                {
                    join.Table1 = whereTable;
                }
                else if (join.Table2.TableName.Equals(whereTable.TableName))
                {
                    join.Table2 = whereTable;
                }
            }

            int table1Cardinality = join.Table1.Rows.Count;
            int table2Cardinality = join.Table2.Rows.Count;
            bool table1Unique = join.Table1Column.IsUnique;
            bool table2Unique = join.Table1Column.IsUnique;

            // Max rows returned could be...
            int card = table1Cardinality * table2Cardinality;

            // Unless there is a unique index on at least one of the columns.
            if (table1Unique)
            {
                card = 1 * table2Cardinality;
            }
            if (table2Unique)
            {
                card = Math.Min(card, table1Cardinality);
            }

            return card;
        }
        
    }
}
