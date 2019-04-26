using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.Extended;

namespace XamarinMarvelChallenge.Extensions
{
    public static class IEnumerableOfTExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> thisIEnumerableOfT)
        {
            var newObservableCollectionOfT = new ObservableCollection<T>(thisIEnumerableOfT);

            return newObservableCollectionOfT;
        }

        public static InfiniteScrollCollection<T> ToInfiniteScrollCollection<T>(this IEnumerable<T> thisIEnumerableOfT)
        {
            var newInfiniteScrollCollectionOfT = new InfiniteScrollCollection<T>(thisIEnumerableOfT);

            return newInfiniteScrollCollectionOfT;
        }
    }
}
