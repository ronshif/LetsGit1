using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCoin
{
    public class FullCoinData
    {
        public CoinName CoinName { get; set; }
        public List<CoinInfo> CoinHistory { get; set; } = new List<CoinInfo>();
        public double AmountOfCoins { get; set; }

        public void AddCoin(CoinInfo coin)
        {
            CoinHistory.Add(coin);
        }

        public CoinInfo GetCoinAtTime(DateTime dateTime)
        {
            for (int i = 1; i < CoinHistory.Count; i++)
            {
                if (CoinHistory[i].date > dateTime)
                {
                    return CoinHistory[i - 1];
                }
            }

            return null;

        }

        public List<CoinInfo> GetCoinHistory(DateTime from,DateTime to)
        {
            return CoinHistory.Where(coinInfo=> coinInfo.date.CompareTo(from)>0 && coinInfo.date.CompareTo(to)<=1).ToList<CoinInfo>();
        }
        public List<CoinInfo> GetCoinHistory(DateTime to)
        {
            return GetCoinHistory(DateTime.MinValue,to);
        }

        public double GetLastCoinValue()
        {
            if (CoinHistory.Count == 0)
            {
                return 0;
            }

            return this.CoinHistory.Last().val;
        }

        public void PrintAllData()
        {
            foreach (CoinInfo coinInfo in CoinHistory)
            {
                Console.WriteLine(coinInfo);
            }
        }
    }
}
