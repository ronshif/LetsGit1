using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCoin
{
    public class FullCoinData
    {
        public CoinName coinName;
        public List<CoinInfo> coinHistory;
        public double amountOfCoins;

        private double GetUSDVal()
        {
            var latesState = coinHistory[coinHistory.Count - 1];
            return amountOfCoins * latesState.val;
        }

        public void AddCoin(CoinInfo coin)
        {
            coinHistory.Add(coin);
        }

        public CoinInfo GetCoinAtTime(DateTime dateTime)
        {
            for (int i = 1; i < coinHistory.Count; i++)
            {
                if (coinHistory[i].date > dateTime)
                {
                    return coinHistory[i - 1];
                }
            }

            return null;

        }

        public List<CoinInfo> GetCoinHistory(DateTime from,DateTime to)
        {
            return coinHistory.Where(coinInfo=> coinInfo.date.CompareTo(from)>0 && coinInfo.date.CompareTo(to)<=1).ToList<CoinInfo>();
        }
        public List<CoinInfo> GetCoinHistory(DateTime to)
        {
            return GetCoinHistory(DateTime.MinValue,to);
        }
    }
}
