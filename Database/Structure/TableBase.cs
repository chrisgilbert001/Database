using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class TableBase
    {
        
        public List<Row> Rows = new List<Row>();
        public Dictionary<string, Column> Columns = new Dictionary<string, Column>();
        public List<Column> ColumnList = new List<Column>();
        public int? UniqueColumnIndex = null;
        public BTree<string, Row> UniqueIndex;
    }
}
