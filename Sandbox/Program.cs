using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sandbox
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var nbr = 1000000;
			var values = new List<double> ();
			var r = new Random ();
			for (int i = 0; i < nbr; i++) 
			{
				values.Add (r.Next (10));
			}

			Stopwatch sw = new Stopwatch();

			sw.Start();

			MovingMean (values, 100000);

			sw.Stop();

			Console.WriteLine("for {0} elements Elapsed={1}",nbr,sw.Elapsed);
			sw.Reset ();
			sw.Start();
			EfficientMovingMean(values, 100000);
			sw.Stop();

			Console.WriteLine("for {0} elements Elapsed={1}",nbr,sw.Elapsed);

				
		}

		public static List<double> MovingMean(List<double> values, int s)
		{
			var ret = new List<double> ();
			for (int idx = 0; idx < values.Count; idx++) 
			{
				var span = s;
				if (idx + s >= values.Count) 
				{
					break;
				}
				var valToAdd = 0.0d;
				for (int i = idx; i < idx + span; i++) 
				{
					valToAdd += values [i];
				}
				ret.Add (valToAdd / span);
			}
			return ret;
		}

		public static List<double> EfficientMovingMean(List<double> values, int s)
		{
			var ret = new List<double> ();

				double span = s;
				if (s >= values.Count) 
				{
				return ret;
				}
				var valToAdd = 0.0d;
				for (int i = 0; i <  span; i++) 
				{
					valToAdd += values [i];
				}
			    var mean = valToAdd / span;

			ret.Add (mean);


			for (int idx = 1; idx < values.Count; idx++) 
			{
				var previousIdx = idx - 1;
				var nextIdx = s+idx-1;

				if (nextIdx >= values.Count) 
				{
					break;

				}

				mean = mean - (values [previousIdx] / span) + (values [nextIdx]/span);
				ret.Add (mean);
			}
			return ret;
		}
	}
}
