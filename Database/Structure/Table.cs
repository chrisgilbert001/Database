using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    /// <summary>
    /// Table Class bla bla bla
    /// </summary>
    class Table
    {
        private List<Row> _rows = new List<Row>();
        public string TableName { get; set; }
        public Dictionary<string, Column> Columns = new Dictionary<string, Column>();

        public List<Row> Rows
        {
            get { return _rows; }
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
            Column newColumn = new Column(columnName, Columns.Count());
            this.Columns.Add(columnName, newColumn);
        }

        public Column GetColumn(string columnName)
        {
            Column column;
            if (Columns.TryGetValue(columnName, out column))
            {
                return column;
            }
            else
            {
                // TODO: Actually create an error class and error properly.
                throw new NotImplementedException();
            }
        }

        public void CheckRowLength()
        {

        }
    }
}
