﻿using System.Collections.Generic;

namespace SmartCoin
{
    public interface ISitesManager
    {
        ExchangeSite Binance { get; }
        ExchangeSite Bitfinex { get; }

        void Init(IAlgorithm algorithm);
        void Init(List<SiteName> sitesOfInterest, List<CoinName> coinsOfInterest);
        void CheckForUpdates();
        void DoBitAction(BitAction recommendedAction);
        void PrintCurrentState();

    }
}