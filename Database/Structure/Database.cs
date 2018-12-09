using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class Database
    {
        public string Name { get; set; }
        public TableList Tables { get; set; }

        public Database(string name)
        {

        }
    }
}
