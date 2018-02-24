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

        private readonly IExchangeServices exchangeServices;
        private List<SiteName> sitesOfInterest;
        private List<CoinName> coinsOfInterest;
       // private DateTime currentTime;


        #region Properties
        public ExchangeSite Binance { get => sites[SiteName.Binance]; }
        public ExchangeSite Bitfinex => sites[SiteName.BitFinex];

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
        public void Init(IAlgorithm algorithm) => Init(algorithm.SitesOfInterest, algorithm.CoinsOfinterest);

        /// <summary>
        /// Generate X number of coins, starting from initial coin, with intervals of one second
        /// </summary>
        /// <param name="startingCoin"></param>
        /// <param name="numberOfCoinsToGenerate"></param>
        /// <returns />
        public void Init(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest)
        {
            this.sitesOfInterest = sitesOfInterest;
            this.coinsOfInterest = coinsOfInterest;

            Dictionary<SiteName, Dictionary<CoinName, List<CoinInfo>>> coinsHistory = exchangeServices.GetCoinsHistory(sitesOfInterest, coinsOfInterest);
            Dictionary<SiteName, Dictionary<CoinName, CoinInfo>> currentCoinsState = exchangeServices.GetCurrentCoinsState(sitesOfInterest, coinsOfInterest);

            foreach (SiteName siteName in sitesOfInterest)
            {
                ExchangeSite site = new ExchangeSite(siteName, this.exchangeServices,coinsOfInterest);
                foreach (CoinName coinName in coinsOfInterest)
                {
                    site.Coins[coinName].coinHistory = coinsHistory[siteName][coinName];
                    site.Coins[coinName].amountOfCoins = currentCoinsState[siteName][coinName].amountAtTheTime;
                }
                Sites.Add(siteName, site);
            }
        }
        #endregion

        public void CheckForUpdates()
        {
            Dictionary<SiteName, Dictionary<CoinName, CoinInfo>> currentCoins = exchangeServices.GetCurrentCoinsState(sitesOfInterest, coinsOfInterest);
            foreach (SiteName siteName in sitesOfInterest)
            {
               sites[siteName].UpdateCoins(currentCoins[siteName]);
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
