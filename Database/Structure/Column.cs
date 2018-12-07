using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class Column
    {
        public string ColumnName;
        public Table Table;

        public Column()
        {

        }

        public Column(string columnName)
        {
            ColumnName = columnName;
        }

        public void KillFreeman()
        {
            Console.WriteLine("fREEMAN DEAD");
        }
    }
}
 