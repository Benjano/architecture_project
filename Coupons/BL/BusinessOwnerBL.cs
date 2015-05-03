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

        public bool insertNewDeal(String name, String details, Business business, decimal price, DateTime experationDate)
        {
            return mDal.insertNewDeal(name, details, business, price, experationDate);
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

        public BusinessOwner getBusinessOwnerById(string userName, string password)
        {
            return mDal.getBusinessOwnerById(userName, password);
        }

        public bool UpdateBusiness(int businessId, String name, String description, int ownerId, String address, String city)
        {
            return mDal.UpdateBusiness(businessId, name, description, ownerId, address, city);
        }
    }
}

