using System.Collections.Generic;
using System.Linq;

namespace CSharpLibrary.SortLibrary
{
    public class SortList
    {
        public static void GetSortedHashList<T>(IEnumerable<T> xList, IEnumerable<T> yList, string keyPropertyName, out IEnumerable<T> xSortedList, out IEnumerable<T> ySortedList) where T : new()
        {
            var xKeyList = xList.Select(x => x.GetType().GetProperty(keyPropertyName).GetValue(x));
            var yKeyList = yList.Select(y => y.GetType().GetProperty(keyPropertyName).GetValue(y));
            var keyHashList = xKeyList.Concat(yKeyList).Distinct();

            var xDictionary = xList.ToDictionary(x => x.GetType().GetProperty(keyPropertyName).GetValue(x));
            var yDictionary = yList.ToDictionary(y => y.GetType().GetProperty(keyPropertyName).GetValue(y));

            foreach (var key in keyHashList)
            {
                if (!xDictionary.ContainsKey(key))
                {
                    var nullInstance = new T();
                    nullInstance.GetType().GetProperty(keyPropertyName).SetValue(nullInstance, key);
                    xDictionary.Add(key, nullInstance);
                }
                if (!yDictionary.ContainsKey(key))
                {
                    var nullInstance = new T();
                    nullInstance.GetType().GetProperty(keyPropertyName).SetValue(nullInstance, key);
                    yDictionary.Add(key, nullInstance);
                }
            }

            xSortedList = xDictionary.Values.OrderBy(x => x.GetType().GetProperty(keyPropertyName).GetValue(x));
            ySortedList = yDictionary.Values.OrderBy(y => y.GetType().GetProperty(keyPropertyName).GetValue(y));
        }
    }
}
