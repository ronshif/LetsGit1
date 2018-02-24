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
            exchangesServicesSimulator.PrintAllData();
            return;
            ISitesManager sitesManager = new SitesManager(exchangesServicesSimulator);
            sitesManager.Init(algorithm.SitesOfInterest,algorithm.CoinsOfinterest);
            algorithm.AllSitesManager = sitesManager;

            DateTime currentTime = exchangesServicesSimulator.FirstDate;
            int step = 0;
            int maxSteps = 20;
            while (currentTime< exchangesServicesSimulator.LastDate && step++< maxSteps)
            {
                exchangesServicesSimulator.CurrentTime = currentTime;
                Console.WriteLine(currentTime + algorithm.GetRelevantDataForOutput());
                EvaluateAlgorithmLogic(sitesManager, algorithm);
                currentTime = currentTime.AddSeconds(exchangesServicesSimulator.Intervals);
            }
        }

        private void EvaluateAlgorithmLogic(ISitesManager sitesManager, IAlgorithm algorithm)
        {
            sitesManager.CheckForUpdates();
            BitAction recommendedAction = algorithm.GetRecommendedAction();
            sitesManager.DoBitAction(recommendedAction);
            if (recommendedAction.Action != BitActionType.Nothing)
            {
                Console.WriteLine(algorithm.GetRecommendedAction());
            }
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
