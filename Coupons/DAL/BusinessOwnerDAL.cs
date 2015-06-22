using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coupons.Models;
using Coupons.Constants;
using Coupons.BL;
using System.Data;

namespace Coupons.DAL
{
    public class BusinessOwnerDAL
    {
        private CouponsDatasetTableAdapters.UsersTableAdapter mTableUsers = new CouponsDatasetTableAdapters.UsersTableAdapter();
        private CouponsDatasetTableAdapters.BusinessesTableAdapter mTableBusiness = new CouponsDatasetTableAdapters.BusinessesTableAdapter();
        private CouponsDatasetTableAdapters.DealsTableAdapter mTableDeals = new CouponsDatasetTableAdapters.DealsTableAdapter();
        private CouponsDatasetTableAdapters.CouponsTableAdapter mTableCoupons = new CouponsDatasetTableAdapters.CouponsTableAdapter();

        public BusinessOwner logBusinessOwner(String username, String password)
        {
            CouponsDataset.UsersDataTable user = mTableUsers.SelectBusinessOwner(username, password);
            if (user.Rows.Count == 1)
            {
                DataRow row = user.Rows[0];
                int id = (int)row[UserColumns.ID];
                String mail = row[UserColumns.MAIL].ToString();
                String phone = row[UserColumns.PHONE].ToString();
                BusinessOwner businessOwner = new BusinessOwner(id, username, mail, phone);
                return businessOwner;
            }
            else
            {
                return null;
            }
        }



        public bool insertNewDeal(String name, String details, Business business, decimal price, DateTime experationDate, String startHour, String endHour)
        {
            int result = mTableDeals.InsertDeal(name, details, business.ID, price, experationDate, startHour, endHour);
            return result == 1;
        }

        public bool insertNewBusiness(int ownerId, String name, String description, String city, String address)
        {
            return mTableBusiness.InsertBusiness(name, description, ownerId, address, city) == 1;
        }

        public List<Business> getBusinessesByOwnerId(int ownerId)
        {
            List<Business> result = new List<Business>();
            CouponsDataset.BusinessesDataTable businesses = mTableBusiness.SelectBusinessByOwner(ownerId);
            foreach (DataRow row in businesses.Rows)
            {
                int id = (int)row[BusinessesColumns.ID];
                String name = row[BusinessesColumns.NAME].ToString();
                String description = row[BusinessesColumns.DESCRIPTION].ToString();
                String address = row[BusinessesColumns.ADDRESS].ToString();
                String city = row[BusinessesColumns.CITY].ToString();

                Business business = new Business(id, name, description, ownerId, address, city);
                result.Add(business);
            }
            return result;
        }

        public void loadDealsToBusiness(Business business)
        {
            // Select the deals of each business belonging to owner
            CouponsDataset.DealsDataTable deals = mTableDeals.SelectDealByBusinessId(business.ID);
            foreach (DataRow row in deals.Rows)
            {
                int id = (int)row[DealsColumns.ID];
                String name = row[DealsColumns.NAME].ToString();
                String details = row[DealsColumns.DETAILS].ToString(); ;
                decimal originalPrice = (decimal)row[DealsColumns.ORIGINAL_PRICE];
                float rate = (float)(double)row[DealsColumns.RATE];
                DateTime experationDate;
                DateTime.TryParse(row[DealsColumns.EXPERATION_DATE].ToString(), out experationDate);
                bool isApproved = (row[DealsColumns.IS_APPROVED].ToString().Equals("True"));
                String startHour = row[DealsColumns.START_HOUR].ToString();
                String endHour = row[DealsColumns.END_HOUR].ToString();
                Deal deal = new Deal(id, name, details, business.ID, originalPrice, rate, experationDate, isApproved, startHour, endHour);
                business.addDeal(deal);
            }
        }

        public void loadDealCoupons(Deal deal)
        {
            CouponsDataset.CouponsDataTable coupons = mTableCoupons.SelectCouponByDealID(deal.ID);
            foreach (DataRow row in coupons.Rows)
            {
                int id = (int)row[CouponsColumns.ID];
                decimal originalPrice = (decimal)row[CouponsColumns.ORIGINAL_PRICE];
                decimal boughtPrice = (decimal)row[CouponsColumns.BOUGHT_PRICE];
                int rate = (int)row[CouponsColumns.RATE];
                bool isUsed = (row[CouponsColumns.IS_USED].ToString().Equals("True"));
                String serialKey = row[CouponsColumns.SERIAL_KEY].ToString();
                int clientId = (int)row[CouponsColumns.CLIENT_ID];


                Coupon coupon = new Coupon(id, clientId, deal.ID, originalPrice, boughtPrice, rate, isUsed, serialKey);
                deal.addCoupon(coupon);
            }
        }

        public BusinessOwner getBusinessOwnerByName(string userName, string password)
        {
            CouponsDataset.UsersDataTable user = mTableUsers.SelectBusinessOwner(userName, password);

            if (user.Rows.Count == 1)
            {
                DataRow row = user.Rows[0];
                int id = (int)row[UserColumns.ID];
                String username = row[UserColumns.USERNAME].ToString();
                String mail = row[UserColumns.MAIL].ToString();
                String phone = row[UserColumns.PHONE].ToString();

                BusinessOwner businessOwner = new BusinessOwner(id, username, mail, phone);
                return businessOwner;
            }
            return null;
        }

        public bool UpdateBusiness(int businessId, String name, String description, int ownerId, String address, String city)
        {
            CouponsDataset.BusinessesDataTable business = mTableBusiness.SelectBusinessById(businessId);
            if (business.Rows.Count == 1)
            {
                return (mTableBusiness.UpdateBusiness(name, description, ownerId, address, city, businessId) == 1);
            }
            else
            {
                return false;
            }
        }

        public List<Business> getBusinessesByName(string name)
        {
            List<Business> result = new List<Business>();
            CouponsDataset.BusinessesDataTable businesses = mTableBusiness.selectBusinessByName(name);
            foreach (DataRow row in businesses.Rows)
            {
                int id = (int)row[BusinessesColumns.ID];
                String description = row[BusinessesColumns.DESCRIPTION].ToString();
                int ownerId = (int)row[BusinessesColumns.OWNER_ID];
                String address = row[BusinessesColumns.ADDRESS].ToString();
                String city = row[BusinessesColumns.CITY].ToString();

                Business business = new Business(id, name, description, ownerId, address, city);
                result.Add(business);
            }
            return result;
        }

        public List<Business> getBusinessById(int businessId)
        {
            List<Business> result = new List<Business>();
            CouponsDataset.BusinessesDataTable businesses = mTableBusiness.SelectBusinessById(businessId);
            if (businesses.Rows.Count == 1)
            {
                DataRow row = businesses.Rows[0];
                int id = (int)row[BusinessesColumns.ID];
                String name = row[BusinessesColumns.NAME].ToString();
                String description = row[BusinessesColumns.DESCRIPTION].ToString();
                int ownerId = (int)row[BusinessesColumns.OWNER_ID];
                String address = row[BusinessesColumns.ADDRESS].ToString();
                String city = row[BusinessesColumns.CITY].ToString();

                Business business = new Business(id, name, description, ownerId, address, city);
                result.Add(business);
            }
            return result;
        }

        public BusinessOwner getBusinessOwnerById(int businessOwnerId)
        {
            CouponsDataset.UsersDataTable user = mTableUsers.selectBusinessOwnerById(businessOwnerId);

            if (user.Rows.Count == 1)
            {
                DataRow row = user.Rows[0];
                int id = (int)row[UserColumns.ID];
                String username = row[UserColumns.USERNAME].ToString();
                String mail = row[UserColumns.MAIL].ToString();
                String phone = row[UserColumns.PHONE].ToString();

                BusinessOwner businessOwner = new BusinessOwner(id, username, mail, phone);
                return businessOwner;
            }
            return null;  
        }

        public BusinessOwner getBusinessOwnerByUserName(string businessOwnerUserName)
        {
            CouponsDataset.UsersDataTable user = mTableUsers.selectBusinessOwnerByUserName(businessOwnerUserName);

            if (user.Rows.Count == 1)
            {
                DataRow row = user.Rows[0];
                int id = (int)row[UserColumns.ID];
                String mail = row[UserColumns.MAIL].ToString();
                String phone = row[UserColumns.PHONE].ToString();

                BusinessOwner businessOwner = new BusinessOwner(id, businessOwnerUserName, mail, phone);
                return businessOwner;
            }
            return null;  
        }


        public BusinessOwner GetBusinessOwnerById(int ownerId)
        {
            CouponsDataset.UsersDataTable user = mTableUsers.selectUserById(ownerId);

            if (user.Rows.Count == 1)
            {
                DataRow row = user.Rows[0];
                int id = (int)row[UserColumns.ID];
                String username = row[UserColumns.USERNAME].ToString();
                String mail = row[UserColumns.MAIL].ToString();
                String phone = row[UserColumns.PHONE].ToString();

                BusinessOwner businessOwner = new BusinessOwner(id, username, mail, phone);
                return businessOwner;
            }
            return null;
        }

        public List<Deal> getAllDealsByBussinesId(int businessId)
        {
            CouponsDataset.DealsDataTable deals = mTableDeals.SelectDealByBusinessId(businessId);
            List<Deal> result = new List<Deal>();
            foreach (DataRow row in deals.Rows)
            {
                int id = (int)row[DealsColumns.ID];
                String name = row[DealsColumns.NAME].ToString();
                String details = row[DealsColumns.DETAILS].ToString(); ;
                decimal originalPrice = (decimal)row[DealsColumns.ORIGINAL_PRICE];
                float rate = (float)(double)row[DealsColumns.RATE];
                DateTime experationDate;
                DateTime.TryParse(row[DealsColumns.EXPERATION_DATE].ToString(), out experationDate);
                bool isApproved = (row[DealsColumns.IS_APPROVED].ToString().Equals("True"));
                String startHour = row[DealsColumns.START_HOUR].ToString();
                String endHour = row[DealsColumns.END_HOUR].ToString();
                Deal deal = new Deal(id, name, details, businessId, originalPrice, rate, experationDate, isApproved, startHour, endHour);
                result.Add(deal);
            }
            return result;
        }


        public Deal getDealById(int dealId)
        {
            CouponsDataset.DealsDataTable deals = mTableDeals.SelectDealById(dealId);
            if (deals.Rows.Count == 1)
            {
                DataRow row = deals[0];
                int id = (int)row[DealsColumns.ID];
                String name = row[DealsColumns.NAME].ToString();
                String details = row[DealsColumns.DETAILS].ToString(); ;
                decimal originalPrice = (decimal)row[DealsColumns.ORIGINAL_PRICE];
                float rate = (float)(double)row[DealsColumns.RATE];
                DateTime experationDate;
                DateTime.TryParse(row[DealsColumns.EXPERATION_DATE].ToString(), out experationDate);
                bool isApproved = (row[DealsColumns.IS_APPROVED].ToString().Equals("True"));
                int businessId = (int)row[DealsColumns.BUSINESS_ID];
                String startHour = row[DealsColumns.START_HOUR].ToString();
                String endHour = row[DealsColumns.END_HOUR].ToString();


                Deal deal = new Deal(id, name, details, businessId, originalPrice, rate, experationDate, isApproved, startHour, endHour);
                return deal;
            }
            return null;
        }


        public List<Coupon> getAllCouponByDealId(int dealId)
        {
            CouponsDataset.CouponsDataTable coupons = mTableCoupons.SelectCouponByDealID(dealId);
            List<Coupon> result = new List<Coupon>();
            foreach (DataRow row in coupons.Rows)
            {
                int rate = Convert.ToInt32(row[CouponsColumns.RATE]);
                decimal originalPrice = (decimal)row[CouponsColumns.ORIGINAL_PRICE];
                decimal boughtPrice = (decimal)row[CouponsColumns.BOUGHT_PRICE];
                bool isUsed = (row[CouponsColumns.IS_USED].ToString().Equals("True"));
                String serialKey = row[CouponsColumns.SERIAL_KEY].ToString();
                int clientId = (int)row[CouponsColumns.CLIENT_ID];
                int id = (int)row[CouponsColumns.ID];
                Coupon coupon = new Coupon(id, clientId, dealId, originalPrice, boughtPrice, rate, isUsed, serialKey);
                result.Add(coupon);
            }
            return result;
        }

    }
}
