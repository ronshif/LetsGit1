﻿using System;
using System.Threading.Tasks;

namespace SmartCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            //Vs_ronshif
            Runner run = new Runner();

            //change2
            IAlgorithm ripple = new RandomAlgorithm();

            //change3
            run.RunnerSimulation(ripple);
            //helooppp
            Console.WriteLine("helloooo");
        }
    }
}
