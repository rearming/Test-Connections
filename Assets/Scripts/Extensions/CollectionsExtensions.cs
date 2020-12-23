using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Extensions
{
	public static class CollectionsExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (var value in collection)
				action.Invoke(value);
		}

		public static T RandomElement<T>(this List<T> collection)
		{
			return collection[Random.Range(0, collection.Count)];
		}
	}
}