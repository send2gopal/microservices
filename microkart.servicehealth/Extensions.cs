using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microkart.shared
{
    public static class Extensions
    {
        private static IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource> source,
    Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>(comparer);
            foreach (TSource element in source)
            {
                if (knownKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
