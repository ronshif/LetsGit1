using System;
using System.Collections.Generic;

namespace SmartCoin
{
    /// <summary>
    /// Simulates responses of an exchange site
    /// </summary>
    public partial class ExchangesServicesSimulator : IExchangeServices
    {
        private SitesManager demoSitesManager;
        private DateTime currentTimeSimulation; //Simulates the current time.
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public int Intervals { get; set; }


        public SitesManager DemoSitesManager { get => demoSitesManager; set => demoSitesManager = value; }
        public DateTime CurrentTime { get => currentTimeSimulation; set => currentTimeSimulation = value; }

        public ExchangesServicesSimulator()
        {
            GenerateDB();
        }

        #region Initialization
        public void GenerateDB()
        {
            DemoDataGenerator generator = new DemoDataGenerator();
            GeneratedDBRequiredData generatedDBRequirements = GetDefaultDBRequirements();

            var generatedDB = generator.GenerateDB(generatedDBRequirements);

            foreach (var x in generatedDBRequirements.InitialCoinAmounts)
            {
                var siteName = x.Key.Key;
                var coinName = x.Key.Value;
                var amount = x.Value;
                generatedDB[siteName].Coins[coinName].amountOfCoins = amount;
            }

            demoSitesManager = new SitesManager() { Sites = generatedDB };
            CurrentTime = generatedDBRequirements.FirstDate;
            FirstDate = generatedDBRequirements.FirstDate;
            LastDate = generatedDBRequirements.LastDate;
            Intervals = generatedDBRequirements.IntervalInSeconds;
            
        }

        private GeneratedDBRequiredData GetDefaultDBRequirements()
        {
            GeneratedDBRequiredData dbRequirements = new GeneratedDBRequiredData();

            dbRequirements.SupportedSites = new List<SiteName>() { SiteName.Binance, SiteName.BitFinex };
            dbRequirements.StartingCoinsValues = new Dictionary<CoinName, CoinInfo>()
            {
                {CoinName.Bitcoin, new CoinInfo(){coinType= CoinName.Bitcoin} },
                {CoinName.Ripple, new CoinInfo(){coinType= CoinName.Ripple} },
            };
            dbRequirements.SetAmountToGenreate(DateTime.Now.Subtract(new TimeSpan(3, 0, 0)), DateTime.Now.AddHours(24), 5);
            dbRequirements.InitialCoinAmounts = new Dictionary<KeyValuePair<SiteName, CoinName>, double>()
            {
                {new KeyValuePair<SiteName, CoinName>(SiteName.BitFinex ,CoinName.Ripple),100 },
                {new KeyValuePair<SiteName, CoinName>(SiteName.BitFinex ,CoinName.Bitcoin),0.5 }
            };
            
            return dbRequirements;
        }
        #endregion

        #region Actions
        public bool DoCoinAction(SiteName siteName, CoinName coinName, BitActionType action, double amountOfCoins)
        {
            return true;
        }

        public CoinInfo GetCoin(SiteName site, CoinName coinType)
        {
            var site1 = demoSitesManager.GetExchangeSite(site);
            return site1.GetCoinFullData(coinType).GetCoinAtTime(currentTimeSimulation);
        }

        public Dictionary<SiteName, Dictionary<CoinName, List<CoinInfo>>> GetCoinsHistory(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest)
        {
            Dictionary<SiteName, Dictionary<CoinName, List<CoinInfo>>> dic = new Dictionary<SiteName, Dictionary<CoinName, List<CoinInfo>>>();

            foreach (var site in sitesOfInterest)
            {
                var site1 = demoSitesManager.GetExchangeSite(site);
                var x = new Dictionary<CoinName, List<CoinInfo>>();

                foreach (var coin in coinsOfInterest)
                {
                    var list = site1.GetCoinFullData(coin).GetCoinHistory(currentTimeSimulation);
                    x[coin] = list;
                }
                dic[site] = x;
            }
            return dic;
        }

        public Dictionary<SiteName, Dictionary<CoinName, CoinInfo>> GetCurrentCoinsState(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest)
        {
            Dictionary<SiteName, Dictionary<CoinName, CoinInfo>> allCoins = new Dictionary<SiteName, Dictionary<CoinName, CoinInfo>>();
            foreach (SiteName siteName in sitesOfInterest)
            {
                var site = demoSitesManager.GetExchangeSite(siteName);
                Dictionary<CoinName, CoinInfo> coinsOfOneSite = new Dictionary<CoinName, CoinInfo>();
                foreach (CoinName coinName in coinsOfInterest)
                {
                    CoinInfo coinInfo = site.Coins[coinName].GetCoinAtTime(currentTimeSimulation);
                    coinsOfOneSite[coinName] = coinInfo;
                }
                allCoins[siteName] = coinsOfOneSite;
            }
            return allCoins;
        }

        

        #endregion
    }
}


