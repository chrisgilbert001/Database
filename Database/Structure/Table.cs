using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    /// <summary>
    /// Table Class
    /// </summary>
    class Table
    {
        public string TableName { get; set; }
        public List<Column> Columns = new List<Column>();
        public List<Row> Rows { get; set; }

        public Table(string name)
        {
            TableName = name;
        }

        /// <summary>
        /// Adds a 'Row' to the table.
        /// </summary>
        /// <param name="row"></param>
        public void AddRow(Row row)
        {
            this.Rows.Add(row);
            row.Table = this;
        }

        /// <summary>
        /// Adds a 'Column' to the table.
        /// </summary>
        /// <param name="column"></param>
        public void AddColumn(Column column)
        {
            this.Columns.Add(column);
            column.Table = this;
        }
    }
}
