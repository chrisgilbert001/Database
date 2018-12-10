using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Structure;

namespace Database.SQLStatements.DML
{
    class Select
    {
        public string FromTable { get; set; }
        private List<string> _fromColumns = new List<string>();

        public List<string> FromColumns
        {
            get { return FromColumns; }
        }

        public Select()
        {

        }
    }
}
