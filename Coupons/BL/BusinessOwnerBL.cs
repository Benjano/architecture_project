using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coupons.DAL;
using System.Data;
using Coupons.Constants;
using Coupons.Models;





namespace Coupons.BL
{
    public class BusinessOwnerBL
    {
        public BusinessOwnerDAL mDal;

        public BusinessOwnerBL()
        {
            mDal = new BusinessOwnerDAL(); 
        }



        public BusinessOwner logBusinessOwner(String username, String password)
        {
            return mDal.logBusinessOwner(username, password);
        }

        public bool insertNewDeal(String name, String details, Business business, decimal price, DateTime experationDate, int startHour_h, int startHour_m, int endHour_h, int endHour_m)
        {
            if (startHour_h >= 0 & startHour_h < 24 & startHour_m >= 0 & startHour_m < 60 & endHour_h >= 0 & endHour_h < 24 & endHour_m >= 0 & endHour_m < 60)
                if (name.Length < 50 & name.Length > 0 & price > 0)
                    return mDal.insertNewDeal(name, details, business, price, experationDate, startHour_h + ":" + startHour_m, endHour_h + ":" + endHour_m);
            return false;
        }

        public bool insertNewBusiness(int ownerId, String name, String description, String city, String address)
        {
            if (name.Length < 50 & name.Length > 0 & city.Length < 50 & city.Length > 0)
                return mDal.insertNewBusiness(ownerId, name, description, city, address);
            return false;
        }

        public List<Business> getBusinessesByOwnerId(int ownerId)
        {
           return  mDal.getBusinessesByOwnerId(ownerId);
        }

        private void loadDealsToBusiness(Business business)
        {
            mDal.loadDealsToBusiness(business);
        }

        private void loadDealCoupons(Deal deal)
        {
            mDal.loadDealCoupons(deal);
        }

        public BusinessOwner getBusinessOwnerByNmae(string userName, string password)
        {
            return mDal.getBusinessOwnerByName(userName, password);
        }

        public bool UpdateBusiness(int businessId, String name, String description, int ownerId, String address, String city)
        {
            if (name.Length < 50 & name.Length > 0 & city.Length < 50 & city.Length > 0)
                return mDal.UpdateBusiness(businessId, name, description, ownerId, address, city);
            return false;
        }
        public List<Business> getBusinessesByName(string name)
        {
            return mDal.getBusinessesByName(name);
        }

        public List<Business> getBusinessById(int businessId)
        {
            return mDal.getBusinessById(businessId);
        }


        public BusinessOwner getBusinessOwnerById(int businessOwnerId)
        {
            return mDal.getBusinessOwnerById(businessOwnerId);
        }

        public BusinessOwner getBusinessOwnerByUserName(string businessOwnerUserName)
        {
            return mDal.getBusinessOwnerByUserName(businessOwnerUserName);
        }

        public List<Deal> getAllDealsByBussinesId(int businessId)
        {
            return mDal.getAllDealsByBussinesId(businessId);
        }

        public Deal getDealById(int dealId)
        {
            return mDal.getDealById(dealId);
        }

        public List<Coupon> getAllCouponByDealId(int dealId)
        {
            return mDal.getAllCouponByDealId(dealId);
        }
    }
}

