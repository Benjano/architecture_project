using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.Models
{
   

    public class Deal
    {
        private int mId;
        public int ID
        {
            get { return mId; }
        }
        private String mName;
        public String Name
        {
            get { return mName; }
        }

        private String mDetails;
        public String Details
        {
            get { return mDetails; }
        }

        private Business mBusiness;
        public Business Business
        {
            get { return mBusiness; }
        }

        private decimal mPrice;
        public decimal Price
        {
            get { return mPrice; }
        }

        private double mRate;
        public double Rate
        {
            get { return mRate; }
            set { mRate= value; }
        }

        private DateTime mExperationDate;
        public DateTime ExperationDate
        {
            get { return mExperationDate; }
            set { mExperationDate = value; }
        }
        private bool mIsApproved;
        public bool IsApproved
        {
            get { return mIsApproved; }
            set { mIsApproved = value; }
        }

        private List<Coupon> mCoupons;
        public List<Coupon> Coupons
        {
            get { return mCoupons; }
        }

        public Deal(int id, String name, String details, Business business, decimal price, double rate, DateTime experationDate, bool isApproved)
        {
            mId = id;
            mName = name;
            mDetails = details;
            mBusiness = business;
            mPrice = price;
            mRate = rate;
            mExperationDate = experationDate;
            mIsApproved = isApproved;
            mCoupons = new List<Coupon>();
        }

        public bool addCoupon(Coupon coupon)
        {
            bool result = false;

            if (this == coupon.Deal & !mCoupons.Contains(coupon))
            {
                mCoupons.Add(coupon);
                result = true;
            }        
            return result; 
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
