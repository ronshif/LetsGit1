using System;
using System.Threading.Tasks;

namespace SmartCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            //b3
            //Vs_ronshif

            Runner run = new Runner();

            //change2
            IAlgorithm ripple = new RandomAlgorithm();

            //change3
            run.RunnerSimulation(ripple);

            //change4
            //helooppp
            Console.WriteLine("helloooo");
        }
    }
}
