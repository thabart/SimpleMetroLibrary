using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PersonnalLibrary.Components.Extensions
{
    public static class CollectionExtension
    {
        public static void AddRange<T>(
            this ObservableCollection<T> collection,
            IList<T> values)
        {
            foreach (var value in values)
            {
                collection.Add(value);
            }
        }
    }
}
