using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumericSequencer.Services
{
	public interface IEvenSequencer : ISequencer
	{ }

	public class EvenSequencer : IEvenSequencer
	{
		public IEnumerable<int> YieldSequence()
		{
			for (int i = 2; true; i += 2)
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