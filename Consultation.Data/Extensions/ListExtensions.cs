using System.Collections.Generic;
using System.Linq;

namespace Consultation.Data
{
    // Class providing IEnumerable extension methods
    public static class ListExtension
    {
        // Check if list a contains list b
        public static bool ContainsList<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            // if list a is smaller than list b then list a cannot contain list b
            if (a.ToList().Count < b.ToList().Count)
            {
                return false;
            }
            // iterate over each element in list b
            foreach (var e in b)
            {
                // if list a does not contain the element then return false
                if (!a.Contains(e)) return false;
            }
            // list a must contain list b so return true
            return true;
        }


        // return a string representation of the IEnumerable data
        public static string ToPrettyString<T>(this IEnumerable<T> data)
        {
            // provide a suitable loop to construct a string from the IEnumerable data
            // in format [ e1 e2 e3 ... ]
            var r = "[";
            foreach (var (e, i) in data.WithIndex())
            {
                r += $"{e}";
                if (i < (data.Count() - 1)) r += ", ";
            }

            r = r + "]";

            // return the newly constructed string
            return r;
        }


        // return Enumerable of Tuples containing item and index
        // useful for use in foreach loops when list item index 
        // required. See use in ToPrettyString above
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> data)
        {
            return data.Select((item, index) => (item, index));
        }
    }
}