using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    /// <summary>
    /// Table Class bla bla bla
    /// </summary>
    class Table : TableBase
    {
        public string TableName { get; set; }

        [JsonConstructor]
        public Table()
        {
        }

        public Table(string name)
        {
            this.TableName = name;
        }

        public void AddRow(Row row)
        {
            this.Rows.Add(row);
        }

        public Row AddAndCreateRow(List<string> entries)
        {
            if (entries.Count == Columns.Count)
            {
                Row row = new Row(entries);
                try
                {
                    Rows.Add(row);
                }
                catch
                {

                }

                // If the table has a unique key we need to add it to the tree
                if (this.UniqueColumnIndex != null)
                {
                    UniqueIndex.Insert(Convert.ToInt32(entries[(int)UniqueColumnIndex]), row);
                }
                return row;
            }
            else
            {
                throw new Exception("Invalid number of entries");
            }
        }

        public void AddAndCreateColumn(string columnName)
        {
            int index = Columns.Count;
            Column newColumn = new Column(columnName, TableName, index);
            this.Columns.Add(columnName, newColumn);
            this.ColumnList.Add(newColumn);
        }

        /// <summary>
        /// Searches the tables columns and retrieves it by name.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public Column GetColumn(string columnName)
        {
            Column column;
            if (Columns.ContainsKey(columnName))
            {
                Columns.TryGetValue(columnName, out column);

                return column = (Column)Columns[columnName];            
            }
            else
            {
                throw new Exception($"Error: Column {columnName} does not exist in Table {TableName}");
            }
        }
    }
}
