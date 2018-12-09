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
        private List<Column> _columns = new List<Column>();
        private List<Row> _rows = new List<Row>();
        public string TableName { get; set; }

        public List<Column> Columns
        {
             get { return _columns; }
        }

        public List<Row> Rows
        {
            get { return _rows; }
        }

        public Table(string name)
        {
            this.TableName = name;
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
