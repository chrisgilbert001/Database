using Database.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.SQLStatements
{
    abstract class Statement
    {
        public abstract void Execute(Db database);
    }
}
