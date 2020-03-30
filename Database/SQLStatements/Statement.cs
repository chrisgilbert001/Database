using Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQLStatements
{
    /* 
     * Class to represent all statements that can be performed in the database.
     */
    abstract class Statement
    {
        public abstract void Execute(Db database);
    }
}
