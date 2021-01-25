using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Optionull.Test
{
	public class OptionullUtilitiesShould
	{
		[Theory]
		[InlineData("Hello", "Hello!")]
		[InlineData(null, null)]
		public void CorrectlySelect(string? input, string? output)
		{
			input.Select(s => s + "!").ShouldBe(output);
		}

		[Theory]
		[InlineData("Hello", null)]
		[InlineData(null, null)]
		public void CorrectlySelectMany(string? input, string? output)
		{
			input.SelectMany(MakeNull).ShouldBe(output);
		}

		private static string? MakeNull(string s) => null;

		[Theory]
		[InlineData("Hello", 1)]
		[InlineData(null, 0)]
		public void CorrectlyConvertToList(string? input, int expectedCount)
		{
			var list = input.ToList();
			list.Count.ShouldBe(expectedCount);
		}

		[Theory]
		[InlineData("Hello", 1)]
		[InlineData(null, 0)]
		public void CorrectlyConvertToArray(string? input, int expectedLength)
		{
			var array = input.ToArray();
			array.Length.ShouldBe(expectedLength);
		}

		[Theory]
		[InlineData("Hello", "World", "Hello")]
		[InlineData(null, "World", "World")]
		public void CorrectlyExecuteIfNotNull(string? input, string initial, string expectedAssignment)
		{
			var result = initial;
			input.IfNotNull(s => result = s);
			result.ShouldBe(expectedAssignment);
		}

		[Fact]
		public async Task CorrectlyAwaitNullTask()
		{
			Task<string>? task = null;
			var s = await task.IfNotNull();
			s.ShouldBeNull();
		}
		
		[Fact]
		public async Task CorrectlyAwaitNonNullTask()
		{
			Task<string>? task = Task.FromResult("Hello World");
			var s = await task.IfNotNull();
			s.ShouldBe("Hello World");
		}
	}
}