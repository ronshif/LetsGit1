using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCoin
{
    /// <summary>
    /// 1. simple algorithm, checks if we grew by 2 percentage in 30 seconds  - buy.
    /// </summary>
    public class RippleAlgorithm:IAlgorithm
    {
        public List<SiteName> SitesOfInterest { get; set; } = new List<SiteName>() { SiteName.Binance, SiteName.BitFinex };
        public List<CoinName> CoinsOfinterest { get; set; } = new List<CoinName>() { CoinName.Bitcoin, CoinName.Ripple };

        public BitAction GetRecommendedAction()
        {
           // return new BitAction(BitActionType.Buy, SiteName.BitFinex, double.MaxValue);
            
            
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

        public string GetRelevantDataForOutput()
        {
            throw new NotImplementedException();
        }


        public ISitesManager AllSitesManager { get; set; }

        private double GetLast30SecondsBinanceTendention(DateTime currTime)
        {
           /* var subList = binanceRipple.Where(coin => currTime.Subtract(coin.date).TotalSeconds < 30);
            var first = subList.First();
            var last = subList.Last();
            double d = (last.val / first.val - 1) * 100;
            return d;*/
            return 0;
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
