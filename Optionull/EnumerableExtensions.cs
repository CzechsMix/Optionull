using System.Collections.Generic;

namespace Optionull
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> items)
			where T : class
		{
			foreach (var item in items)
				if (item is not null)
					yield return item;
		}
	}
}