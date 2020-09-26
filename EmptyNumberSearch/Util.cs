using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyNumberSearch
{
    public static class Util
    {
        public static void shuffle<T>(ref List<T> list)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            List<T> newList = new List<T>();
            foreach(T item in list)
            {
                newList.Insert(rand.Next(0, newList.Count), item);
            }
            newList.Remove(list[0]);
            newList.Insert(rand.Next(0, newList.Count), list[0]);

            list=newList;
        }
    }
}
