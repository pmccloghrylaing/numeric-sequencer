using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumericSequencer.Services;
using FluentAssertions;
using System.Linq;

namespace NumericSequencer.Tests.Services
{
	[TestClass]
	public class SequencersTests
	{
		[TestMethod]
		public void AllSequencer_GeneratesAll()
		{
			new AllSequencer().YieldSequence()
				.Take(20)
				.ShouldAllBeEquivalentTo(new[]
				{
					1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
				});
		}

		[TestMethod]
		public void OddSequencer_GeneratesOdd()
		{
			new OddSequencer().YieldSequence()
				.Take(20)
				.ShouldAllBeEquivalentTo(new[]
				{
					1, 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25, 27, 29, 31, 33, 35, 37, 39
				});
		}

		[TestMethod]
		public void EvenSequencer_GeneratesEven()
		{
			new EvenSequencer().YieldSequence()
				.Take(20)
				.ShouldAllBeEquivalentTo(new[]
				{
					2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40
				});
		}

		[TestMethod]
		public void FizzBuzzSequencer_GeneratesAll()
		{
			new FizzBuzzSequencer().YieldSequence()
				.Take(20)
				.ShouldAllBeEquivalentTo(new[]
				{
					1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
				});
		}

		[TestMethod]
		public void FibonacciSequencer_GeneratesFibonacci()
		{
			new FibonacciSequencer().YieldSequence()
				.Take(20)
				.ShouldAllBeEquivalentTo(new[]
				{
					1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765
				});
		}

		[TestMethod]
		public void MapItem_SequencersExceptFizzBuzz()
		{
			var sequencersWithDefaultMapping = new ISequencer[]
			{
				new AllSequencer(),
				new OddSequencer(),
				new EvenSequencer(),
				new FibonacciSequencer()
			};
			foreach (var sequencer in sequencersWithDefaultMapping)
			{
				Enumerable.Range(1, 20)
					.Select(sequencer.MapInteger)
					.ShouldAllBeEquivalentTo(new[]
					{
						"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"
					});
			}
		}

		[TestMethod]
		public void MapItem_FizzBuzz()
		{
			Enumerable.Range(1, 20)
				.Select(new FizzBuzzSequencer().MapInteger)
				.ShouldAllBeEquivalentTo(new[]
				{
					"1", "2", "C", "4", "E", "C", "7", "8", "C", "E", "11", "C", "13", "14", "Z", "16", "17", "C", "19", "E"
				});
		}
	}
}
