using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCoin
{

    /// <summary>
    /// Performs actions agaoinst an exchange site
    /// </summary>
    public class ExchangesServices : IExchangeServices
    {
        public void DoCoinAction(CoinName coinName, BitActionType buy, double amountOfCoins)
        {
           //call method on api
        }

        public Dictionary<SiteName, Dictionary<CoinName, double>> GetCurrentAmounts(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest)
        {
            throw new NotImplementedException();
        }

        public double GetCurrentAmounts(SiteName siteName, CoinName coinName)
        {
            throw new NotImplementedException();
        }

        public bool DoCoinAction(SiteName site, CoinName coinName, BitActionType buy, double amountOfCoins)
        {
            throw new NotImplementedException();
        }

        public CoinInfo GetCoin(SiteName site, CoinName coinType)
        {
            //call method on api
            return null;
        }

        public Dictionary<SiteName, Dictionary<CoinName, FullCoinData>> GetCoinsHistory(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest)
        {
            //call method on api
            return null;
        }

        public Dictionary<SiteName, Dictionary<CoinName, CoinInfo>> GetCurrentCoinsState(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest)
        {
            //call method on api
            return null;
        }

        Dictionary<SiteName, Dictionary<CoinName, List<CoinInfo>>> IExchangeServices.GetCoinsHistory(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest)
        {
            //call method on api
            return null;
        }
    }

}
