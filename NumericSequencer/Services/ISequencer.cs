using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumericSequencer.Services
{
	public interface ISequencer
	{
		IEnumerable<int> YieldSequence();
		string MapInteger(int i);
	}
}
