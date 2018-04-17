using System;
using System.Threading.Tasks;

namespace SmartCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            Runner run = new Runner();
            IAlgorithm ripple = new RandomAlgorithm();
            run.RunnerSimulation(ripple);
            //helooppp
            Console.WriteLine("helloooo");
        }
    }
}
