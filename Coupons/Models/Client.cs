using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coupons.Enums;

namespace Coupons.Models
{
     public class Client : User
    {

        private DateTime mBirthDate;
        public DateTime BirthDate
        {
            get { return mBirthDate; }
        }
        
        private Gender mGender;
        public Gender Gender
        {
            get { return mGender; }
        }

        private String mLocation;
        public String Location
        {
            get { return mLocation; }
            set { mLocation = value; }
        }

        private List<Category> mCategories;
        public List<Category> Categories
        {
            get { return mCategories; }
        }

        private List<Brand> mBrands;
        public List<Brand> Brands
        {
            get { return mBrands; }
        }

        private List<Coupon> mCoupons;
        public List<Coupon> Coupons
        {
            get { return mCoupons; }
        }

        private List<Group> mGroups;
        public List<Group> Groups
        {
            get { return mGroups; }
        }

        private List<SocialNetwork> mSocialNetworks;
        public List<SocialNetwork> SocialNetworks
        {
            get { return mSocialNetworks; }
        }

        private List<Client> mFriends;
        public List<Client> Friends
        {
            get { return mFriends; }
        }


        public Client(int id, String username, String mail, String phone, DateTime birthDate, Gender gender, String location):base(id, username, mail, phone)
        {
            mBirthDate = birthDate;
            mGender = gender;
            mLocation = location;

            mCategories = new List<Category>();
            mBrands = new List<Brand>();
            mCoupons = new List<Coupon>();
            mGroups = new List<Group>();
            mSocialNetworks = new List<SocialNetwork>();
            mFriends = new List<Client>();
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

        public bool addBrand(Brand brand)
        {
            bool result = false;
            if (!mBrands.Contains(brand))
            {
                mBrands.Add(brand);
                result = true;
            }

            return result;
        }

        public bool addCoupon(Coupon coupon)
        {
            bool result = false;
            if (!mCoupons.Contains(coupon))
            {
                mCoupons.Add(coupon);
                result = true;
            }

            return result;
        }

        public bool addGroup(Group group)
        {
            bool result = false;
            if (!mGroups.Contains(group))
            {
                mGroups.Add(group);
                result = true;
            }

            return result;
        }

        public bool addSocialNetwork(SocialNetwork socialNetwork)
        {
            bool result = false;
            if (!mSocialNetworks.Contains(socialNetwork))
            {
                mSocialNetworks.Add(socialNetwork);
                result = true;
            }

            return result;
        }

        public bool addFriend(Client friend)
        {
            bool result = false;
            if (friend != this && !mFriends.Contains(friend))
            {
                mFriends.Add(friend);
                result = true;
            }

            return result;
        }

        public Coupon buyCoupon(Deal deal, int price)
        {
            String serialKey = ""; // TODO : add coupon to db and generate serial key
            Coupon coupon = new Coupon(this, deal, deal.Price, price, -1, false, serialKey);
            deal.addCoupon(coupon);

            return coupon;
        }

    }
}
