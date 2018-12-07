using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class Row : List<string>
    {
        public List<string> Entries { get; set; }
        public Table Table { get; set; }

        public Row()
        {

        }

        public Row(Table table, List<string> entries)
        {
            this.Table = table;
            this.Entries = entries;
            table.AddRow(this);
        }
    }
}
