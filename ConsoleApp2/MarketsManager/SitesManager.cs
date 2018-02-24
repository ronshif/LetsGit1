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
        public Dictionary<SiteName, ExchangeSite> Sites { get; set; } = new Dictionary<SiteName, ExchangeSite>();

        private readonly IExchangeServices exchangeServices;
        private List<SiteName> sitesOfInterest;
        private List<CoinName> coinsOfInterest;

        public ExchangeSite Binance => Sites[SiteName.Binance];
        public ExchangeSite Bitfinex => Sites[SiteName.BitFinex];

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
                    site.Coins[coinName].CoinHistory = coinsHistory[siteName][coinName];

                    site.Coins[coinName].AmountOfCoins = exchangeServices.GetCurrentAmounts(siteName, coinName);
                }
                Sites.Add(siteName, site);
            }
        }

        public ExchangeSite GetExchangeSite(SiteName siteName)
        {
            return Sites[siteName];
        }

        public void CheckForUpdates()
        {
            Dictionary<SiteName, Dictionary<CoinName, CoinInfo>> currentCoins = exchangeServices.GetCurrentCoinsState(sitesOfInterest, coinsOfInterest);
            foreach (SiteName siteName in sitesOfInterest)
            {
               Sites[siteName].UpdateCoins(currentCoins[siteName]);
            }
        }

        public void DoBitAction(BitAction recommendedAction)
        {
            if (recommendedAction.Action == BitActionType.Nothing)
            {
                return;
            }

            var site = this.Sites[recommendedAction.Site];

            site.ExecuteAction(recommendedAction);
        }

        public void PrintAllData()
        {
            foreach (var pair in Sites)
            {
                pair.Value.PrintAllData();
            }
        }
    }
}
