﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SmartCoin
{
    public class DemoDataGenerator
    {
        /// <summary>
        ///Generates a full DB according to the pramaters 
        /// </summary>
        public  Dictionary<SiteName, ExchangeSite> GenerateDB(GeneratedDBRequiredData data)
        {
            Dictionary<SiteName, ExchangeSite> sites = new Dictionary<SiteName, ExchangeSite>();
            foreach (var siteName in data.SupportedSites)
            {
                Dictionary<CoinName, FullCoinData> fullCoinsData = new Dictionary<CoinName, FullCoinData>();
                
                foreach (var startingCoin in data.StartingCoinsValues)
                {
                    //here
                    FullCoinData coinData = new FullCoinData();
                    List<CoinInfo> generatedCoins = GenerateCoins(startingCoin.Value, data.Amount,data.IntervalInSeconds,data.FirstDate);
                    coinData.CoinHistory = generatedCoins;
                    coinData.CoinName = startingCoin.Key;
                    fullCoinsData.Add(coinData.CoinName, coinData);
                }
                fullCoinsData.Add(CoinName.USD,new FullCoinData());

                var coinsOfInterest = data.StartingCoinsValues.Select(x => x.Value.coinType).ToList();
                ExchangeSite site = new ExchangeSite(siteName, null, coinsOfInterest);
                site.Coins = fullCoinsData;
                sites.Add(siteName, site);
            }

            return sites;
        }

        private  List<CoinInfo> GenerateCoins(CoinInfo startingCoin, int amount, int intervalInSeconds, DateTime firstDate)
        {
            Random rnd = new Random(1);
            Random rnd2 = new Random(2);
            double lastVal = startingCoin.val;

            List<CoinInfo> list = new List<CoinInfo>();
            list.Add(startingCoin);
            
            for (int i = 0; i < amount; i++)
            {
                double change = rnd.NextDouble();
                int direction = rnd2.NextDouble() > 0.5 ? 1 : -1;
                change = ((change * direction) + 100) / 100;
                double newVal = lastVal * change;

                var newCoin = new CoinInfo(startingCoin.coinType, firstDate.AddSeconds(intervalInSeconds*i), newVal);
                list.Add(newCoin);
                lastVal = newVal;
            }

            return list;
            //here
        }

    }
}

