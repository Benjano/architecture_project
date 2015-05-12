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

        public BusinessOwner logBusinessOwner (String username, String password){
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

        public bool insertNewDeal(String name, String details, Business business, decimal price, DateTime experationDate)
        {
            int result = mTableDeals.InsertDeal(name, details, business.ID, price, experationDate.ToShortDateString());
            loadDealsToBusiness(business);
            return result == 1;
        }

        public void loadBusinesses(BusinessOwner owner)
        {
            CouponsDataset.BusinessesDataTable businesses = mTableBusiness.SelectBusinessByOwner(owner.ID);
            foreach (DataRow row in businesses.Rows)
            {
                int id = (int)row[BusinessesColumns.ID];
                String name = row[BusinessesColumns.NAME].ToString();
                String description = row[BusinessesColumns.DESCRIPTION].ToString();
                String address = row[BusinessesColumns.ADDRESS].ToString();
                String city = row[BusinessesColumns.CITY].ToString();

                Business business = new Business(id, name,description,owner,address,city);
                owner.addBusiness(business);
             }
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
                Deal deal = new Deal(id, name, details, business, originalPrice, rate, experationDate, isApproved);
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
                ClientBL clientBL = new ClientBL();
                Client client = clientBL.getClientById(clientId);

                Coupon coupon = new Coupon(client, deal, originalPrice, boughtPrice, rate, isUsed, serialKey);
                deal.addCoupon(coupon);
            } 
        }

        public BusinessOwner getBusinessOwnerById(string userName, string password)
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
                 return (mTableBusiness.UpdateBusiness(name, description, ownerId, address, city, businessId)==1);
             }
             else
             {
                 return false;
             }
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
    }
}
