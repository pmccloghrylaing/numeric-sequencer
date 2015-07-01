using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumericSequencer.Services
{
	public interface IFizzBuzzSequencer : ISequencer
	{ }

	public class FizzBuzzSequencer : IFizzBuzzSequencer
	{
		public IEnumerable<int> YieldSequence()
		{
			for (int i = 1; true; i++)
			{
				yield return i;
			}
		}

		public string MapInteger(int i)
		{
			if (i % 15 == 0)
			{
				return "Z";
			}
			if (i % 3 == 0)
			{
				return "C";
			}
			if (i % 5 == 0)
			{
				return "E";
			}
			return i.ToString();
		}
	}
}