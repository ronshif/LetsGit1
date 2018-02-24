using System;
using System.Collections.Generic;

namespace SmartCoin
{
    /// <summary>
    /// Holds info of one exchangeSite, can perform operations agains the actual site and hold the info about the related data in that site, including our own posessions there.
    /// </summary>
    public class ExchangeSite
    {
        private SiteName site;
        private Dictionary<CoinName,FullCoinData> coins;

        public SiteName Site { get => site; set => site = value; }
        public Dictionary<CoinName, FullCoinData> Coins { get => coins; set => coins = value; }

        IExchangeServices exchangeServices;

        public FullCoinData GetCoinFullData(CoinName coinName)
        {
            return Coins[coinName];
        }


        public ExchangeSite(SiteName site, IExchangeServices exchangeServices,List<CoinName> coinsOfInterest)
        {
            this.Site = site;
            this.exchangeServices = exchangeServices;
            Coins = new Dictionary<CoinName, FullCoinData>();
            foreach(var coinName in coinsOfInterest)
            {
                Coins[coinName] = new FullCoinData() { coinName = coinName };
            }
            Coins[CoinName.USD] = new FullCoinData() { coinName = CoinName.USD };
        }

        internal void UpdateCoins(Dictionary<CoinName, CoinInfo> latestCoinsState)
        {
          foreach (CoinName coinName in latestCoinsState.Keys)
            {
                coins[coinName].coinHistory.Add(latestCoinsState[coinName]);
            }
        }

        internal void ExecuteAction(BitAction recommendedAction)
        {
            FullCoinData coinData =  this.coins[recommendedAction.CoinName];
            FullCoinData usdCoinData = this.coins[CoinName.USD];

            if (recommendedAction.Action == BitActionType.Buy )
            {
                if (usdCoinData.amountOfCoins <= 0)
                {
                    Console.WriteLine("can't buy coins - no money");
                    return;
                }
                exchangeServices.DoCoinAction(this.Site, recommendedAction.CoinName,BitActionType.Buy, usdCoinData.amountOfCoins);
            }
            if (recommendedAction.Action == BitActionType.Sell)
            {
                if (coinData.amountOfCoins <= 0)
                {
                    Console.WriteLine("can't sell coins - no coins");
                    return;
                }
                exchangeServices.DoCoinAction(this.site,recommendedAction.CoinName,BitActionType.Sell, coinData.amountOfCoins);
            }
        }

        #region Specific coins getters  - helper methods for ease of use
        public List<CoinInfo> Ripples
        {
            get { return Coins[CoinName.Ripple].coinHistory; }
        }

        #endregion
    }


}
