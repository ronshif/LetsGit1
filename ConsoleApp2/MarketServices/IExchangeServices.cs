using System.Collections.Generic;

namespace SmartCoin
{
    public interface IExchangeServices
    {
        CoinInfo GetCoin(SiteName site, CoinName coinType);
        Dictionary<SiteName, Dictionary<CoinName, List<CoinInfo>>> GetCoinsHistory(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest);
        Dictionary<SiteName, Dictionary<CoinName, CoinInfo>> GetCurrentCoinsState(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest);

        bool DoCoinAction(SiteName site, CoinName coinName, BitActionType buy, double amountOfCoins);
    }

}
