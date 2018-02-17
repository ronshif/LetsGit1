using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCoin
{
    public class RippleAlgorithm:IAlgorithm
    {
        List<CoinInfo> bitFinexRipple;
        List<CoinInfo> binanceRipple;

        private List<SiteName> sitesOfInterest = new List<SiteName>() { SiteName.Binance, SiteName.BitFinex };
        private List<CoinName> coinsOfInterest = new List<CoinName>() { CoinName.Bitcoin, CoinName.Ripple,CoinName.USD };

        public List<SiteName> SitesOfInterest { get => sitesOfInterest; set => sitesOfInterest = value; }
        public List<CoinName> CoinsOfinterest { get => coinsOfInterest; set => coinsOfInterest = value; }

        public BitAction GetRecommendedAction()
        {
            double last30SecondsBinance = GetLast30SecondsBinanceTendention(DateTime.Now);
            if (last30SecondsBinance > 1)
            {
                return new BitAction(BitActionType.Buy, SiteName.BitFinex, double.MaxValue);
            }
            else if (last30SecondsBinance < -1)
            {
                return new BitAction(BitActionType.Sell, SiteName.BitFinex, double.MaxValue);
            }
            else
            {
                return null;
            }
        }

        private double GetLast30SecondsBinanceTendention(DateTime currTime)
        {
            var subList = binanceRipple.Where(coin => currTime.Subtract(coin.date).TotalSeconds < 30);
            var first = subList.First();
            var last = subList.Last();
            double d = (last.val / first.val - 1) * 100;
            return d;
        }

  


        //  public void Init(List<ExchangeSite> initialData)
        // {
        /*  var bitFinexInitialRipplesData = initialData.Find(site => site.Site == SiteName.BitFinex).Ripples;
          var binanceInitialRipplesData = initialData.Find(site => site.Site == SiteName.Binance).Ripples;

          bitFinexRipple.AddRange(bitFinexInitialRipplesData);
          binanceRipple.AddRange(binanceInitialRipplesData);*/
        // }

        // public BitAction Update(List<CoinInfo> coins)
        // {
        //    HandleInput(coins);
        //          BitAction nextAction =  GetNextAction();
        //            return nextAction;
        //}

        /*private void HandleInput(List<CoinInfo> coins)
        {
            foreach (CoinInfo coin in coins)
            {
                if (coin.coinType == CoinName.Ripple && coin.site == SiteName.BitFinex)
                {
                    bitFinexRipple.Add(coin);
                }
                else if (coin.coinType == CoinName.Ripple && coin.site == SiteName.Binance)
                {
                    binanceRipple.Add(coin);
                }
            }
        }*/

    }
}
