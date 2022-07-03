using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame.CExt.Extensions
{
    public static class CollectionExtensions
    {
        private static Random r = new Random();
        
        public static T Random<T>(this List<T> list)
        {
            return list[r.Next(0,list.Count)];
        }
        public static T Random<T>(this List<T> list, int start, int end)
        {
            return list[r.Next(start, end)];
        }
    }
}
