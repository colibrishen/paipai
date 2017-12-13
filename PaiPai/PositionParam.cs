using System;

namespace PaiPai
{
    public class PositionParam
    {
        public PositionParam()
        {
            Add100 = new ButLocal();
            Add200 = new ButLocal();
            Add300 = new ButLocal();
            AddOrder = new ButLocal();
            Less100 = new ButLocal();
            Less200 = new ButLocal();
            Less300 = new ButLocal();
            Bid = new ButLocal();
            BidRight = new ButLocal();

            BidTime = string.Empty;
            AddPriceTime = string.Empty;
            RighTime = string.Empty;
            Price = 0;
            Password = string.Empty;
        }

        public ButLocal Add100;
        public ButLocal Add200;
        public ButLocal Add300;
        public ButLocal AddOrder;
        public ButLocal Less100;
        public ButLocal Less200;
        public ButLocal Less300;
        public ButLocal Bid;
        public ButLocal BidRight;

        public int Price;
        public string BidTime;
        public string AddPriceTime;
        public string RighTime;
        public string Url;

        public string Password;
    }

    public class ButLocal
    {
        private int _x;
        private int _y;

        public ButLocal()
        {
            _x = 0;
            _y = 0;
        }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
