using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Optionull
{
	public static class OptionullUtilities
	{
		public static TOut? Select<TIn, TOut>(this TIn? input, Func<TIn, TOut> selector)
			where TIn : class
			where TOut : class
			=> input is null ? null : selector(input);

		public static TOut? SelectMany<TIn, TOut>(this TIn? input, Func<TIn, TOut?> selector)
			where TIn : class
			where TOut : class
			=> input is null ? null : selector(input);

		public static T? IfNotNull<T>(this T? input, Action<T> traversal)
			where T : class
		{
			if (input is not null)
				traversal(input);
			return input;
		}
		
		public static List<T> ToList<T>(this T? input)
			where T : class
			=> input is null ? new List<T>(0) : new List<T>(1) { input };

		public static T[] ToArray<T>(this T? input)
			where T : class
			=> input is null ? Array.Empty<T>() : new[] { input };

		public static async Task<T?> IfNotNull<T>(this Task<T>? task)
			where T : class
		{
			if (task is null)
				return null;
			else
				return await task;
		}
	}
}