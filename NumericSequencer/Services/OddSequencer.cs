using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumericSequencer.Services
{
	public interface IOddSequencer : ISequencer
	{ }

	public class OddSequencer : IOddSequencer
	{
		public IEnumerable<int> YieldSequence()
		{
			for (int i = 1; true; i += 2)
			{
				yield return i;
			}
		}

		public string MapInteger(int i)
		{
			return i.ToString();
		}
	}
}