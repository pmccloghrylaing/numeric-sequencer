using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumericSequencer.Services
{
	public interface IAllSequencer : ISequencer
	{ }

	public class AllSequencer : IAllSequencer
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
			return i.ToString();
		}
	}
}