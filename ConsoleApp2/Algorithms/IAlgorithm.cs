using System.Collections.Generic;

namespace SmartCoin
{
    public interface IAlgorithm
    {

        List<SiteName> SitesOfInterest { get; set; }
        List<CoinName> CoinsOfinterest { get; }

        BitAction GetRecommendedAction();

    }
}