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
    public class BusinessOwnerController
    {
        private BusinessOwnerDAL mDal;
        private DataParser mParser;

        public BusinessOwnerController()
        {
            mDal = new BusinessOwnerDAL();
            mParser = new DataParser();
        }

        public bool InsertNewDeal(String name, String details, Business business, decimal price, DateTime experationDate, int startHour_h, int startHour_m, int endHour_h, int endHour_m)
        {
            bool result = false;
            if (ValidateDealDate(startHour_h, startHour_m, endHour_h, endHour_m))
            {
                result = mDal.InsertNewDeal(name, details, business, price, experationDate, startHour_h + ":" + startHour_m, endHour_h + ":" + endHour_m);
            }
            return result;
        }
        private bool ValidateDealDate(int startHour_h, int startHour_m, int endHour_h, int endHour_m)
        {
            return startHour_h >= 0 & startHour_h < 24 & startHour_m >= 0 &
                startHour_m < 60 & endHour_h >= 0 & endHour_h < 24 &
                endHour_m >= 0 & endHour_m < 60;
        }

        public bool InsertNewBusiness(int ownerId, String name, String description, String city, String address)
        {
            bool result = false;
            if (name.Length < 50 & name.Length > 0 & city.Length < 50 & city.Length > 0)
            {
                result = mDal.InsertNewBusiness(ownerId, name, description, city, address);
            }
            return result;
        }

        public List<Business> getBusinessesByOwnerId(int ownerId)
        {
            DataTable businessTable = mDal.GetBusinessesByOwnerId(ownerId);
            List<Business> businesses = mParser.ParseBusinesses(businessTable);
            foreach (Business buisness in businesses)
                LoadDealsToBusiness(buisness);
            return businesses;
        }

        private void LoadDealsToBusiness(Business business)
        {
            DataTable dealsTable = mDal.GetBusinessDealsByBusinessId(business.ID);
            List<Deal> deals = mParser.ParseDeals(dealsTable);
            foreach (Deal deal in deals){
                business.addDeal(deal); 
            }
        }

        private void LoadDealCoupons(Deal deal)
        {
            DataTable couponsTable = mDal.GetDealCoupons(deal.ID);
            List<Coupon> coupons = mParser.ParseCoupons(couponsTable);
            foreach (Coupon coupon in coupons)
            {
                deal.addCoupon(coupon);
            }
        }

        public bool UpdateBusiness(int businessId, String name, String description, int ownerId, String address, String city)
        {
            if (name.Length < 50 & name.Length > 0 & city.Length < 50 & city.Length > 0)
                return mDal.UpdateBusiness(businessId, name, description, ownerId, address, city);
            return false;
        }
        
        public List<Business> GetBusinessesByName(string name)
        {
            DataTable table = mDal.GetBusinessesByName(name);
            List<Business> result = mParser.ParseBusinesses(table);
            foreach (Business buisness in result)
                LoadDealsToBusiness(buisness);
            return result;
        }

        public List<Business> GetBusinessById(int businessId)
        {
            DataTable table = mDal.GetBusinessById(businessId);
            List<Business> result = mParser.ParseBusinesses(table);
            foreach (Business buisness in result)
                LoadDealsToBusiness(buisness);
            return result;
        }

        public BusinessOwner GetBusinessOwnerById(int businessOwnerId)
        {
            DataTable table = mDal.GetBusinessOwnerById(businessOwnerId);
            if (table.Rows.Count == 1)
            {
                BusinessOwner result = mParser.ParseBusinessOwner(table.Rows[0]);
                return result;
            }
            else
            {
                return null;
            }
        }

        public List<BusinessOwner> getBusinessOwnerByUserName(string name)
        {
            DataTable table = mDal.GetBusinessOwnerByName(name);
            List<BusinessOwner> result = mParser.ParseBusinessOwners(table);
            return result;
        }

        public List<Deal> GetAllDealsByBussinesId(int businessId)
        {
            DataTable table = mDal.GetAllDealsByBussinesId(businessId);
            List<Deal> result = mParser.ParseDeals(table);
            return result;
        }

        public Deal getDealById(int dealId)
        {
            DataTable table = mDal.GetDealById(dealId);
            if (table.Rows.Count == 1)
            {
                Deal result = mParser.ParseDeal(table.Rows[0]);
                return result;
            }
            else
            {
                return null;
            }
        }

        public List<Coupon> GetAllCouponByDealId(int dealId)
        {
            DataTable table = mDal.GetAllCouponByDealId(dealId);
            List<Coupon> result = mParser.ParseCoupons(table);
            return result;
        }

        public bool updateCouponUsed(Coupon mSelectedCoupon)
        {
            return mDal.updateCouponUsed(mSelectedCoupon.ID);
        }
    }
}

