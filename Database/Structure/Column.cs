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
        public Table Table { get; set; }

        public Column(string name)
        {
            Name = columnName;
        }

        public Column(Table table, string name)
        {
            this.Table = table;
            this.Name = name;
            table.AddColumn(this);
        }
    }
}
 