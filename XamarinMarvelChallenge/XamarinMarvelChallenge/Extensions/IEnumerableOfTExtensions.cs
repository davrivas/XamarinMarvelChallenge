using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinMarvelChallenge.Extensions
{
    public static class IEnumerableOfTExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> thisIEnumerableOfT)
        {
            var newObservableCollectionOfT = new ObservableCollection<T>();

            foreach (var item in thisIEnumerableOfT)
                newObservableCollectionOfT.Add(item);

            return newObservableCollectionOfT;
        }
    }
}
