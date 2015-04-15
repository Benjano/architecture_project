using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coupons.Models
{
    public class Coupon
    {
        private int mId;
        public int ID
        {
            get { return mId; }
        }

        private Deal mDeal;
        public Deal Deal
        {
            get { return mDeal; }
        }
        private Client mClient;
        public Client Client
        {
            get { return mClient; }
        }

        private decimal mOriginalPrice;
        public decimal OriginalPrice
        {
            get { return mOriginalPrice; }
        }

        private decimal mBoughtPrice;
        public decimal BoughtPrice
        {
            get { return mBoughtPrice; }
        }

        private int mRate;
        public int Rate
        {
            get { return mRate; }
            set { mRate = value; }
        }

        private bool mIsUsed;
        public bool IsUsed
        {
            get { return mIsUsed; }
            set { mIsUsed = value; }
        }
        private String mSerialKey;
        private int p;
        private string isUsed;
        public String SerialKey
        {
            get { return mSerialKey; }
        }

        public Coupon(Client client, Deal deal, decimal originalPrice, decimal boughtPrice, int rate, bool isUsed, String serialKey)
        {
            mClient = client;
            mDeal = deal;
            mOriginalPrice = originalPrice;
            mBoughtPrice = boughtPrice;
            mRate = rate;
            mIsUsed = isUsed;
            mSerialKey = serialKey;
        }

    }
}
