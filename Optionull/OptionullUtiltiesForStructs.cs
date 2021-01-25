using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Optionull
{
	public static class OptionullUtiltiesForStructs
	{
		public static TOut? Select<TIn, TOut>(this TIn? input, Func<TIn, TOut> selector)
			where TOut : struct
			where TIn : struct
			=> input.HasValue ? selector(input.Value) : null;
		public static TOut? SelectMany<TIn, TOut>(this TIn? input, Func<TIn, TOut?> selector)
			where TIn : struct
			where TOut : struct
			=> input.HasValue ? selector(input.Value) : null;

		public static List<T> ToList<T>(this T? input)
			where T : struct
			=> input.HasValue ? new List<T>(1) { input.Value } : new List<T>(0);

		public static T[] ToArray<T>(this T? input)
			where T : struct
			=> input.HasValue ? new[] { input.Value } : Array.Empty<T>();

		public static T? IfNotNull<T>(this T? input, Action<T> traversal)
			where T : struct
		{
			if (input.HasValue)
				traversal(input.Value);
			return input;
		}

		public static async Task<T?> IfNotNull<T>(this Task<T>? task)
			where T : struct
		{
			if (task is null)
				return null;
			else
				return await task;
		}
	}
}