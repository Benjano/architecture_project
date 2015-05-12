using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coupons.Enums;

namespace Coupons.Models
{
    public class Business
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
            set { mName = value; }
        }

        private String mDescription;
        public String Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }

        private  User mOwner;
        public  User Owner
        {
            get { return mOwner; }
        }

        private String mAddress;
        private String Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        
        private String mCity;
        public String City
        {
            get { return mCity; }
            set { mCity = value; }
        }

        private List<Deal> mDeals;
        public List<Deal> Deals
        {
            get { return mDeals; }
        }

        private List<Category> mCategories;
        public List<Category> Categories
        {
            get { return mCategories; }
        }

        
        public Business(int id, String name, String description, User owner, String address, String city)
        {
            mId = id;
            mName = name;
            mDescription = description;
            mOwner = owner;
            mAddress = address;
            mCity = city;
            mDeals = new List<Deal>();
            mCategories = new List<Category>();
        }


        public bool addDeal(Deal deal)
        {
            bool result = false;
            if (!mDeals.Contains(deal))
            {
                mDeals.Add(deal);
                result = true;
            }

            return result;
        }

        public bool addCategory(Category category)
        {
            bool result = false;
            if (!mCategories.Contains(category))
            {
                mCategories.Add(category);
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
