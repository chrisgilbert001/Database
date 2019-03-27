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
                Rows.Add(row);
                return row;
            }
            else
            {
                //TODO: Error when the row size doesn't match the table size.
                throw new NotImplementedException();
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
                JObject jobject = Columns[columnName] as JObject;
                return column = jobject.ToObject<Column>();
                //return column = (Column)Columns[columnName];

            }
            else
            {
                // TODO: Actually create an error class and error properly.
                throw new NotImplementedException();
            }
        }
    }
}
