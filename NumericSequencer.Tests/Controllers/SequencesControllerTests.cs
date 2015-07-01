using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NumericSequencer.Controllers;
using NumericSequencer.Services;
using FluentAssertions;

namespace NumericSequencer.Tests.Controllers
{
	[TestClass]
	public class SequencesControllerTests
	{
		ISequencer sequencer;
		int[] returnSequence;
		Func<int, string> mapItem;

		[TestInitialize]
		public void Init()
		{
			var sequencerMock = new Mock<ISequencer>();

			sequencerMock.Setup(s => s.YieldSequence()).Returns(() => returnSequence);

			mapItem = i  => i.ToString();
			sequencerMock.Setup(s => s.MapInteger(It.IsAny<int>())).Returns((int i) => mapItem(i));

			sequencer = sequencerMock.Object;
		}

		[TestMethod]
		public void GetSequence_Inclusive()
		{
			 returnSequence = new[] { 1, 2, 4, 5, 7, 8, 10 };
			SequencesController.GetSequence(sequencer, 5)
				.ShouldAllBeEquivalentTo(new[]
				{
					"1", "2", "4", "5"
				});
		}

		[TestMethod]
		public void GetSequence_Exclusive()
		{
			 returnSequence = new[] { 1, 2, 4, 5, 7, 8, 10 };
			SequencesController.GetSequence(sequencer, 3)
				.ShouldAllBeEquivalentTo(new[]
				{
					"1", "2"
				});
		}

		[TestMethod]
		public void GetSequence_MapToChar()
		{
			mapItem = i => ((char)('a' + i)).ToString();

			returnSequence = new[] { 1, 2, 4, 5, 7, 8, 10 };
			SequencesController.GetSequence(sequencer, 5)
				.ShouldAllBeEquivalentTo(new[]
				{
					"b", "c", "e", "f"
				});
		}

		[TestMethod]
		public void GetSequence_MapToLargerNumber()
		{
			mapItem = i => Math.Pow(2, i).ToString();

			returnSequence = new[] { 1, 2, 4, 5, 7, 8, 10 };
			SequencesController.GetSequence(sequencer, 5)
				.ShouldAllBeEquivalentTo(new[]
				{
					"2", "4", "16", "32"
				});
		}
	}
}
