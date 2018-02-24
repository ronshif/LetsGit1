using System;

namespace SmartCoin
{
    public class CoinInfo
    {
        public CoinName coinType;
        public DateTime date;
        public double val;
        public int hourTendency;
        public int minuteTendency;
        public int secondTendency;
        //public SiteName site;
        public double amountAtTheTime;


        public CoinInfo(CoinName coinType, DateTime date, double val)
        {
            this.coinType = coinType;
            this.date = date;
            this.val = val;
          //  this.site = site;
        }
        public CoinInfo()
        {
           
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}  ", coinType, date, val);
        }
    }
}
