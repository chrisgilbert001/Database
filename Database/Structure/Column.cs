using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    [Serializable]
    class Column
    {
        string ColumnName;

        public Column(string columnName) {
            ColumnName = columnName;
        }
    }
}
 