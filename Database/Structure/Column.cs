using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class Column :  ColumnBase
    {
        public Column()
        {
        }

        public Column(string name, int columnID, string tableName)
        {
            ColumnName = name;
            ColumnIndex = columnID;
            TableName = tableName;
        }
    }
}
 