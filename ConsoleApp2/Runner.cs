using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCoin
{
    public class Runner
    {
        public void RunnerSimulation(IAlgorithm algorithm)
        {
            ExchangesServicesSimulator exchangesServicesSimulator = new ExchangesServicesSimulator();
//            exchangesServicesSimulator.PrintAllData(SiteName.BitFinex,CoinName.Ripple);
            ISitesManager sitesManager = new SitesManager(exchangesServicesSimulator);
            sitesManager.Init(algorithm.SitesOfInterest,algorithm.CoinsOfinterest);
            algorithm.AllSitesManager = sitesManager;

            DateTime simulatedCurrentTime = exchangesServicesSimulator.FirstDate;
            int step = 0;
            int maxSteps = Int32.MaxValue;

            while (simulatedCurrentTime < exchangesServicesSimulator.LastDate && step++< maxSteps)
            {
                exchangesServicesSimulator.Time = simulatedCurrentTime;
                Console.WriteLine(simulatedCurrentTime + algorithm.GetRelevantDataForOutput());
                var evaluatedAction = EvaluateAlgorithmLogic(sitesManager, algorithm);
                if (evaluatedAction.Action != BitActionType.Nothing)
                {
                    Console.WriteLine(evaluatedAction);
                }

                simulatedCurrentTime = simulatedCurrentTime.AddSeconds(exchangesServicesSimulator.Intervals);
            }
        }

        private BitAction EvaluateAlgorithmLogic(ISitesManager sitesManager, IAlgorithm algorithm)
        {
            sitesManager.CheckForUpdates();
            BitAction recommendedAction = algorithm.GetRecommendedAction();
            sitesManager.DoBitAction(recommendedAction);

            return recommendedAction;
        }

        public void RunnerReal(IAlgorithm algorithm, int steps)
        {
            IExchangeServices services = new ExchangesServices();
            ISitesManager sitesManager = new SitesManager(services);

            sitesManager.Init(algorithm.SitesOfInterest, algorithm.CoinsOfinterest); 

            while (true)
            {
                EvaluateAlgorithmLogic(sitesManager, algorithm);
            }
        }
    }
}
