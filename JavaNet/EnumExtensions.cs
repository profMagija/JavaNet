using System;
using System.Collections.Generic;

namespace JavaNet
{
    public static class EnumExtensions
    {
        public static void TryForeach<T, TException>(this IEnumerable<T> enumerable, Action<T> act) 
            where TException : Exception
        {
            var list = new List<Exception>();

            foreach (var o in enumerable)
            {
                try
                {
                    act(o);
                }
                catch (TException e)
                {
                    if (e is AggregateException aggregateException)
                        list.AddRange(aggregateException.InnerExceptions);
                    else
                        list.Add(e);
                }
            }

            if (list.Count > 0)
                throw new AggregateException(list);
        }
    }
}