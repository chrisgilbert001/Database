using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class CustomString
    {
        // Store a string value.
        public string value;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="v"></param>
        public CustomString(string v)
        {
            this.value = v;
        }

        /// <summary>
        /// Overrides the GetHashCode to always return the same value no matter the object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// Overrides the Equals method. Checks whether a string matches the string stored.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            CustomString customString = (CustomString)obj;
            return customString.value.Equals(value);
        }
    }
}
