using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace SmartCoin
{
    /// <summary>
    /// Holds info of one exchangeSite, can perform operations agains the actual site and hold the info about the related data in that site, including our own posessions there.
    /// </summary>
    public class ExchangeSite
    {
        public SiteName Name { get; set; }
        public Dictionary<CoinName, FullCoinData> Coins { get; set; } = new Dictionary<CoinName, FullCoinData>();

        private readonly IExchangeServices exchangeServices;


        public FullCoinData GetCoinFullData(CoinName coinName)
        {
            return Coins[coinName];
        }


        public ExchangeSite(SiteName siteName, IExchangeServices exchangeServices,List<CoinName> coinsOfInterest)
        {
            this.Name = siteName;
            this.exchangeServices = exchangeServices;
           
            foreach(var coinName in coinsOfInterest)
            {
                Coins[coinName] = new FullCoinData() { CoinName = coinName };
            }

            Coins[CoinName.USD] = new FullCoinData() { CoinName = CoinName.USD };
        }

        internal void UpdateCoins(Dictionary<CoinName, CoinInfo> latestCoinsState)
        {
            foreach (var coinName in latestCoinsState.Keys)
            {
                Coins[coinName].CoinHistory.Add(latestCoinsState[coinName]);
            }
        }

        internal void ExecuteAction(BitAction recommendedAction)
        {
            FullCoinData coinData =  this.Coins[recommendedAction.CoinName];
            FullCoinData usdCoinData = this.Coins[CoinName.USD];

            if (recommendedAction.Action == BitActionType.Buy )
            {
                if (usdCoinData.AmountOfCoins <= 0)
                {
                    Console.WriteLine("can't buy coins - no money");
                    return;
                }
                exchangeServices.DoCoinAction(this.Name, recommendedAction.CoinName,BitActionType.Buy, usdCoinData.AmountOfCoins);
            }
            if (recommendedAction.Action == BitActionType.Sell)
            {
                if (coinData.AmountOfCoins <= 0)
                {
                    Console.WriteLine("can't sell coins - no coins");
                    return;
                }
                exchangeServices.DoCoinAction(this.Name,recommendedAction.CoinName,BitActionType.Sell, coinData.AmountOfCoins);
            }
        }

        public string GetCoinsState()
        {
            StringBuilder str = new StringBuilder();
            foreach (var val in this.Coins.Where(pair => pair.Value.CoinName != CoinName.USD))
            {
                FullCoinData coinDate = val.Value;
                var lastValue = coinDate.GetLastCoinValue();
                var amount = coinDate.AmountOfCoins;
                var usdVal = amount * lastValue;
                str.AppendFormat("{0} value: {1} amount: {2}, usdval: {3}", coinDate.CoinName, lastValue, amount,
                    usdVal);
                str.AppendLine();
            }

            return str.ToString();
            
        }

        #region Specific coins getters  - helper methods for ease of use
        public List<CoinInfo> Ripples
        {
            get { return Coins[CoinName.Ripple].CoinHistory; }
        }

        #endregion

        public void PrintAllData()
        {
            foreach (var val in Coins)
            {
                val.Value.PrintAllData();
            }
        }
    }


}
