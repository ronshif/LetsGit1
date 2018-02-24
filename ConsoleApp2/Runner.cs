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
            ISitesManager sitesManager = new SitesManager(exchangesServicesSimulator);
            sitesManager.Init(algorithm);

           
            DateTime currentTime = exchangesServicesSimulator.FirstDate;

            while (currentTime< exchangesServicesSimulator.LastDate)
            {
                exchangesServicesSimulator.CurrentTime = currentTime;
                EvaluateAlgorithmLogic(sitesManager, algorithm);
                currentTime = currentTime.AddSeconds(exchangesServicesSimulator.Intervals);
            }
        }

        private void EvaluateAlgorithmLogic(ISitesManager sitesManager, IAlgorithm algorithm)
        {
            sitesManager.CheckForUpdates();
            BitAction recommendedAction = algorithm.GetRecommendedAction();
            sitesManager.DoBitAction(recommendedAction);
            sitesManager.PrintCurrentState();
        }

        public void RunnerReal(IAlgorithm algorithm, int steps)
        {
            IExchangeServices services = new ExchangesServices();
            ISitesManager sitesManager = new SitesManager(services);

            sitesManager.Init(algorithm); 

            bool keepRunning = true;
            while (keepRunning)
            {
                EvaluateAlgorithmLogic(sitesManager, algorithm);
            }
        }
    }
}
