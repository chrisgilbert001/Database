using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Structure
{
    class BTreeKey<TKey, TValue> where TKey : IComparable
    {
        public TKey Key { get; set; }
        public TValue Pointer { get; set; }

        public bool Equals(TKey anotherKey)
        {
            return this.Key.Equals(anotherKey);
        }

        public int CompareTo(BTreeKey<TKey, TValue> anotherKey)
        {
            return this.Key.CompareTo(anotherKey.Key);
        }
    }
}
