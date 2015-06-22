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

        private int mDealId;
        public int DealId
        {
            get { return mDealId; }
        }
        private int mClientId;
        public int ClientId
        {
            get { return mClientId; }
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
        public String SerialKey
        {
            get { return mSerialKey; }
        }

        public Coupon(int couponId, int clientId, int dealId,  decimal boughtPrice, int rate, bool IsUsed, String serialKey)
        {
            mId = couponId;
            mClientId = clientId;
            mDealId = dealId;
            mBoughtPrice = boughtPrice;
            mRate = rate;
            mIsUsed = IsUsed;
            mSerialKey = serialKey;
        }

    }
}
