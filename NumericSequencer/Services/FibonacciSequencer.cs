using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumericSequencer.Services
{
	public interface IFibonacciSequencer : ISequencer
	{ }

	public class FibonacciSequencer : IFibonacciSequencer
	{
		public IEnumerable<int> YieldSequence()
		{
			var queue = new Queue<int>();

			queue.Enqueue(1);
			yield return 1;

			while (true)
			{
				var next = queue.Sum();
				queue.Enqueue(next);

				if (queue.Count > 2)
				{
					queue.Dequeue();
				}

				yield return next;
			}
		}

		public string MapInteger(int i)
		{
			return i.ToString();
		}
	}
}