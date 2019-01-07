using Database.SQLStatements.DML;
using Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQLStatements
{    
    /* 
     * Class to represent all Data manipulation queries that can be performed in the database.
     */
    abstract class Query : Statement
    {
        public Query()
        {

        }       
    }
}
