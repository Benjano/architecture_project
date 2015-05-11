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

        public bool insertNewDeal(String name, String details, Business business, decimal price, DateTime experationDate, String startHour, String endHour)
        {
            return mDal.insertNewDeal(name, details, business, price, experationDate, startHour, endHour);
        }

        public bool insertNewBusiness(int ownerId, String name, String description, String city, String address)
        {
            return mDal.insertNewBusiness(ownerId, name, description, city, address);
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
            return mDal.UpdateBusiness(businessId, name, description, ownerId, address, city);
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
    }
}

