using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class TableList : List<Table>
    {
        public List<Table> tables;

        public TableList()
        {
        }

        public int FindTable(string tableName)
        {
            return FindIndex(x => x.TableName.Equals(tableName));
        }
    }
}
