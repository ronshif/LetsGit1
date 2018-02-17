using System;
using System.Collections.Generic;

namespace SmartCoin
{

    public class GeneratedDBRequiredData
    {
        private List<SiteName> supportedSites;
        private Dictionary<CoinName, CoinInfo> startingCoinsValues;

        private int intervalInSeconds;
        private int amount;
        private DateTime firstDate;

        public int IntervalInSeconds { get => intervalInSeconds; }
        public int Amount { get => amount;}
        public DateTime FirstDate { get => firstDate; }
        public Dictionary<CoinName, CoinInfo> StartingCoinsValues { get => startingCoinsValues; set => startingCoinsValues = value; }
        public List<SiteName> SupportedSites { get => supportedSites; set => supportedSites = value; }
        public Dictionary<KeyValuePair<SiteName, CoinName>, double> InitialCoinAmounts { get => initialCoinAmounts; set => initialCoinAmounts = value; }

        private Dictionary<KeyValuePair<SiteName, CoinName>, double> initialCoinAmounts;

        public void SetAmountToGenreate(DateTime firstDate, int amount,int intervalInSeconds)
        {
            this.firstDate = firstDate;
            this.amount = amount;
            this.intervalInSeconds = intervalInSeconds;
        }

        public void SetAmountToGenreate(DateTime firstDate, DateTime lastDate, int intervalInSeconds)
        {
            DateTime currDate  = firstDate;

            this.firstDate = firstDate;
            this.intervalInSeconds = intervalInSeconds;

            double totalSeconds =lastDate.Subtract(firstDate).TotalSeconds;
            this.amount = (int)totalSeconds/IntervalInSeconds;
        }





    }

}


