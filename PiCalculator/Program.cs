using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace PiCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("How many darts should each thread throw? ");
            int toThrow = Convert.ToInt32(Console.ReadLine());
            Console.Write("How many threads should be created? ");
            int numThreads = Convert.ToInt32(Console.ReadLine());

            List<Thread> threadList = new List<Thread>(numThreads);
            List<FindPiThread> funcList = new List<FindPiThread>(numThreads);

            for(int i = 0; i < numThreads; i++)
            {
                FindPiThread p = new FindPiThread(toThrow);
                funcList.Add(p);
                Thread t = new Thread(new ThreadStart(p.throwDarts));
                threadList.Add(t);
                threadList[i].Start();
                Thread.Sleep(16);
            }

            foreach(Thread t in threadList)
            {
                t.Join();
            }

            int totalDartsInside = 0;
            foreach(FindPiThread p in funcList)
            {
                totalDartsInside += p.dartCount;
            }

            double approxPi = (4.0 * totalDartsInside / (toThrow * numThreads));
            Console.WriteLine("Approximation of pi: {0}", approxPi);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }

    class FindPiThread
    {
        private int dartsToThrow;
        public int dartCount;
        private Random random;

        public FindPiThread(int toThrow)
        {
            random = new Random();
            dartsToThrow = toThrow;
            dartCount = 0;
        }

        public void throwDarts()
        {
            double x;
            double y;
            for(int i = 0; i < dartsToThrow; i++)
            {
                x = random.NextDouble();
                y = random.NextDouble();
                if(x*x + y*y <= 1)
                {
                    dartCount++;
                }
            }
        }
    }
}
