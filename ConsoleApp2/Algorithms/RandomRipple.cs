using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCoin
{
    public class RandomAlgorithm:IAlgorithm
    {
        public string GetRelevantDataForOutput()
        {
           return  AllSitesManager.Bitfinex.GetCoinsState();
        }

        public ISitesManager AllSitesManager { get; set; }

        public List<SiteName> SitesOfInterest { get; set; } = new List<SiteName>() {SiteName.BitFinex };
        public List<CoinName> CoinsOfinterest { get; set; } = new List<CoinName>() {CoinName.Ripple };

        public BitAction GetRecommendedAction()
        {
            Random rnd = new Random();
            BitActionType actionType = BitActionType.Nothing;
            double val = rnd.NextDouble();
            if (val > 0.9)
            {
                actionType = BitActionType.Buy;
            }

            if (val < 0.1)
            {
                actionType = BitActionType.Sell;
            }
                
            return new BitAction(actionType, SiteName.BitFinex, double.MaxValue);
        }

    }
}
