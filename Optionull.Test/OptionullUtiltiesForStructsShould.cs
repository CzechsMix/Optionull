using System.Threading.Tasks;
using Xunit;
using Shouldly;

namespace Optionull.Test
{
	public class OptionullUtiltiesForStructsShould
	{
		[Theory]
		[InlineData(1, 2)]
		[InlineData(null, null)]
		public void CorrectlySelect(int? input, int? output)
		{
			input.Select(i => i + 1).ShouldBe(output);
		}

		[Theory]
		[InlineData(1, null)]
		[InlineData(null, null)]
		public void CorrectlySelectMany(int? input, int? output)
		{
			input.SelectMany(MakeNull).ShouldBe(output);
		}

		private static int? MakeNull(int i) => null;
		
		[Theory]
		[InlineData(1, 1)]
		[InlineData(null, 0)]
		public void CorrectlyConvertToList(int? input, int expectedCount)
		{
			var list = input.ToList();
			list.Count.ShouldBe(expectedCount);
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(null, 0)]
		public void CorrectlyConvertToArray(int? input, int expectedLength)
		{
			var array = input.ToArray();
			array.Length.ShouldBe(expectedLength);
		}

		[Theory]
		[InlineData(1, 2, 1)]
		[InlineData(null, 2, 2)]
		public void CorrectlyExecuteIfNotNull(int? input, int initial, int expectedAssignment)
		{
			var result = initial;
			input.IfNotNull(i => result = i);
			result.ShouldBe(expectedAssignment);
		}
		
		[Fact]
		public async Task CorrectlyAwaitNullTask()
		{
			Task<int>? task = null;
			var i = await task.IfNotNull();
			i.ShouldBeNull();
		}
		
		[Fact]
		public async Task CorrectlyAwaitNonNullTask()
		{
			Task<int>? task = Task.FromResult(1);
			var i = await task.IfNotNull();
			i.ShouldBe(1);
		}
	}
}