using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCoin
{
    /// <summary>
    /// The main object that an algorithm will use, it should contain all the needed information, and perform the required operation against the markets.
    /// </summary>
    public class SitesManager : ISitesManager
    {
        private Dictionary<SiteName, ExchangeSite> sites = new Dictionary<SiteName, ExchangeSite>();

        private IExchangeServices exchangeServices;
        private List<SiteName> sitesOfInterest;
        private List<CoinName> coinsOfInterest;
        private DateTime currentTime;


        #region Properties
        public ExchangeSite Binance { get { return sites[SiteName.Binance]; } }
        public ExchangeSite Bitfinex { get { return sites[SiteName.BitFinex]; } }

        public Dictionary<SiteName, ExchangeSite> Sites { get => sites; set => sites = value; }

        public ExchangeSite GetExchangeSite(SiteName siteName)
        {
            return sites[siteName];
        }
        #endregion

        #region Ctrs and Initializators

        public SitesManager()
        {

        }
        public SitesManager(IExchangeServices exchangeServices)
        {
            this.exchangeServices = exchangeServices;
        }

        /// <summary>
        /// Generate X number of coins, starting from initial coin, with intervals of one second
        /// </summary>
        /// <param name="startingCoin"></param>
        /// <param name="numberOfCoinsToGenerate"></param>
        /// <returns></returns>
        public void Init(IAlgorithm algorithm)
        {
            Init(algorithm.SitesOfInterest, algorithm.CoinsOfinterest);
        }
        /// <summary>
        /// Generate X number of coins, starting from initial coin, with intervals of one second
        /// </summary>
        /// <param name="startingCoin"></param>
        /// <param name="numberOfCoinsToGenerate"></param>
        /// <returns></returns>
        public void Init(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest)
        {
            Dictionary<SiteName, Dictionary<CoinName, List<CoinInfo>>> coinsOfInterest1 = exchangeServices.GetCoinsHistory(sitesOfInterest, coinsOfInterest);
            var x = exchangeServices.GetCurrentCoinsState(sitesOfInterest, coinsOfInterest);
            foreach (SiteName siteName in sitesOfInterest)
            {
                ExchangeSite site = new ExchangeSite(siteName, this.exchangeServices);
                site.Coins = new Dictionary<CoinName, FullCoinData>();

                Dictionary<CoinName, List<CoinInfo>> dic1 = coinsOfInterest1[siteName];
                foreach (CoinName coinName in coinsOfInterest)
                {
                    FullCoinData fullCoinData = new FullCoinData();
                    fullCoinData.coinHistory = dic1[coinName];
                    fullCoinData.amountOfCoins = x[siteName][coinName].amountAtTheTime;
                    site.Coins[coinName] = fullCoinData;
                }
                site.Coins = new Dictionary<CoinName, FullCoinData>();
            }
        }
        #endregion



        public void CheckForUpdates()
        {
            Dictionary<SiteName, Dictionary<CoinName, CoinInfo>> currentCoins = exchangeServices.GetCurrentCoinsState(sitesOfInterest, coinsOfInterest);
            foreach (SiteName site in currentCoins.Keys)
            {
                sites[site].UpdateCoins(currentCoins[site]);
            }
        }

        public void PrintCurrentState()
        {
            Console.WriteLine("woohooo");
        }

        public void DoBitAction(BitAction recommendedAction)
        {
            if (recommendedAction.Action == BitActionType.Nothing)
            {
                return;
            }

            var site = this.sites[recommendedAction.Site];

            site.ExecuteAction(recommendedAction);

        }





    }
}
