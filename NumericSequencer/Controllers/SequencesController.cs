using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NumericSequencer.Models;
using NumericSequencer.Services;

namespace NumericSequencer.Controllers
{
	[RoutePrefix("api/sequences")]
	public class SequencesController : ApiController
	{
		readonly IAllSequencer allSequencer;
		readonly IOddSequencer oddSequencer;
		readonly IEvenSequencer evenSequencer;
		readonly IFizzBuzzSequencer fizzBuzzSequencer;
		readonly IFibonacciSequencer fibonacciSequencer;

		public SequencesController(
			IAllSequencer allSequencer,
			IOddSequencer oddSequencer,
			IEvenSequencer evenSequencer,
			IFizzBuzzSequencer fizzBuzzSequencer,
			IFibonacciSequencer fibonacciSequencer)
		{
			this.allSequencer = allSequencer;
			this.oddSequencer = oddSequencer;
			this.evenSequencer = evenSequencer;
			this.fizzBuzzSequencer = fizzBuzzSequencer;
			this.fibonacciSequencer = fibonacciSequencer;
		}

		[Route("{number}")]
		public IHttpActionResult GetResults(int number)
		{
			var model = new SequencesModel
			{
				AllSequence = GetSequence(allSequencer, number),
				OddSequence = GetSequence(oddSequencer, number),
				EvenSequence = GetSequence(evenSequencer, number),
				FizzBuzzSequence = GetSequence(fizzBuzzSequencer, number),
				FibonacciSequence = GetSequence(fibonacciSequencer, number),
			};

			return Ok(model);
		}

		internal static string[] GetSequence(ISequencer sequencer, int number)
		{
			return sequencer.YieldSequence()
				.TakeWhile(x => x <= number)
				.Select(sequencer.MapInteger)
				.ToArray();
		}
	}
}
