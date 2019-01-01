using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class Column
    {
        public string Name { get; set; }
        public int ColumnID { get; set; }

        public Column(string name, int columnID)
        {
            Name = name;
            ColumnID = columnID;
        }
    }
}
 