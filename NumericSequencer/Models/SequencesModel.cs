using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumericSequencer.Models
{
	public class SequencesModel
	{
		public string[] AllSequence { get; set; }
		public string[] OddSequence { get; set; }
		public string[] EvenSequence { get; set; }
		public string[] FizzBuzzSequence { get; set; }
		public string[] FibonacciSequence { get; set; }
	}
}