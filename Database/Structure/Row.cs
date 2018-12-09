using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{   /// <summary>
    /// Row Class.
    /// Contains one row worth of data belonging to a Table.
    /// </summary>
    class Row : List<string>
    {
        private List<string> _entries = new List<string>();
        public Table Table { get; set; }

        public List<string> Entries
        {
            get { return _entries; }
        }

        public Row()
        {

        }

        public Row(List<string> entries)
        {
            this._entries = entries;
        }

        public Row(Table table, List<string> entries)
        {
            this.Table = table;
            this._entries = entries;
            table.AddRow(this);
        }
    }
}
