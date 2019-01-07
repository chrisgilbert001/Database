using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{   
    class Row : RowBase
    {
        public Row()
        {
        }

        public Row(Table table, List<string> entries)
        {
            Entries = entries;
            table.AddRow(this);
        }

        public Row(List<string> entries)
        {
            Entries = entries;
        }
    }
}
