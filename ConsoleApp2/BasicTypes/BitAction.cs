namespace SmartCoin
{
    public class BitAction
    {
        private BitActionType action;
        private CoinName coinName;
        private SiteName site;
        private double amount;

        public BitActionType Action { get => action; set => action = value; }
        public SiteName Site { get => site; set => site = value; }
        public double Amount { get => amount; set => amount = value; }
        public CoinName CoinName { get => coinName; set => coinName = value; }

        public BitAction(BitActionType action, SiteName site, double amount)
        {
            this.Action = action;
            this.Site = site;
            this.Amount = amount;
        }
        public BitAction()
        { }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} Amount:{3} ", Action, Site, CoinName,Amount == double.MaxValue?"All":Amount.ToString());
        }
    }
}
