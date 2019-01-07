using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class ColumnBase
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int ColumnIndex { get; set; }
        public Table Table { get; set; }
    }
}
