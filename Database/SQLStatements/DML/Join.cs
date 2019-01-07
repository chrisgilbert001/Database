using Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQLStatements.DML
{
    /* 
     * Class representing a Join Clause.
     * Contains information about the join to do.    
     */  
    class Join
    {
        public string JoinTable { get; set; }
        public List<Tuple<string, string>> JoinColumns = new List<Tuple<string, string>>();     
    }
}
