using Coupons.Constants;
using Coupons.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.BL
{
    class DataParser
    {


        /* Parsing User Table */
        public List<BusinessOwner> ParseBusinessOwners(DataTable table)
        {
            List<BusinessOwner> result = new List<BusinessOwner>();
            BusinessOwner businessOwner;
            foreach (DataRow row in table.Rows)
            {
                businessOwner = ParseBusinessOwner(row);
                if (businessOwner != null)
                {
                    result.Add(businessOwner);
                }
            }
            return result;
        }

        public BusinessOwner ParseBusinessOwner(DataRow row)
        {
            try
            {
                int id = (int)row[UserColumns.ID];
                String username = row[UserColumns.USERNAME].ToString();
                String mail = row[UserColumns.MAIL].ToString();
                String phone = row[UserColumns.PHONE].ToString();

                BusinessOwner businessOwner = new BusinessOwner(id, username, mail, phone);
                return businessOwner;
            }
            catch
            {
                return null;
            }
        }



        /* Parsing Client Table */
        

        /* Parsing Group Table */
        public Group ParseGroup(DataRow row)
        {
            try
            {
                int id = (int)row[GroupColumns.ID];
                string name = (string)row[GroupColumns.NAME];
                Group group = new Group(id, name);
                return group;
            }
            catch
            {
                return null;
            }
        }


        /* Parsing Coupon Table */
        public List<Coupon> ParseCoupons(DataTable table)
        {
            List<Coupon> result = new List<Coupon>();
            Coupon coupon;
            foreach (DataRow row in table.Rows)
            {
                coupon = ParseCoupon(row);
                if (coupon != null)
                {
                    result.Add(coupon);
                }
            }
            return result;
        }

        public Coupon ParseCoupon(DataRow row)
        {
            try
            {
                int id = (int)row[CouponsColumns.ID];
                int clientId = (int)row[CouponsColumns.CLIENT_ID];
                int dealId = (int)row[CouponsColumns.DEAL_ID];
                int rate = Convert.ToInt32(row[CouponsColumns.RATE]);
                decimal originalPrice = (decimal)row[CouponsColumns.ORIGINAL_PRICE];
                decimal boughtPrice = (decimal)row[CouponsColumns.BOUGHT_PRICE];
                bool isUsed = (row[CouponsColumns.IS_USED].ToString().Equals("True      "));
                String serialKey = row[CouponsColumns.SERIAL_KEY].ToString();

                Coupon coupon = new Coupon(id, clientId, dealId, originalPrice, boughtPrice, rate, isUsed, serialKey);
                return coupon;
            }
            catch
            {
                return null;
            }

        }

        /* Parsing Deal Table */
        public List<Deal> ParseDeals(DataTable table)
        {
            List<Deal> result = new List<Deal>();
            Deal deal;
            foreach (DataRow row in table.Rows)
            {
                deal = ParseDeal(row);
                if (deal != null)
                {
                    result.Add(deal);
                }
            }
            return result;
        }

        public Deal ParseDeal(DataRow row)
        {
            try
            {
                int id = (int)row[DealsColumns.ID];
                String name = row[DealsColumns.NAME].ToString();
                String details = row[DealsColumns.DETAILS].ToString(); ;
                decimal originalPrice = (decimal)row[DealsColumns.ORIGINAL_PRICE];
                float rate = (float)(double)row[DealsColumns.RATE];
                DateTime experationDate;
                DateTime.TryParse(row[DealsColumns.EXPERATION_DATE].ToString(), out experationDate);
                bool isApproved = (row[DealsColumns.IS_APPROVED].ToString().Equals("True      "));
                int businessId = (int)row[DealsColumns.BUSINESS_ID];
                String startHour = row[DealsColumns.START_HOUR].ToString();
                String endHour = row[DealsColumns.END_HOUR].ToString();

                Deal deal = new Deal(id, name, details, businessId, originalPrice, rate, experationDate, isApproved, startHour, endHour);
                return deal;
            }
            catch
            {
                return null;
            }
        }


        /* Parsing Business Table */
        public List<Business> ParseBusinesses(DataTable table)
        {
            List<Business> result = new List<Business>();
            Business business;
            foreach (DataRow row in table.Rows)
            {
                business = ParseBusiness(row);
                if (business != null)
                {
                    result.Add(business);
                }
            }
            return result;
        }

        public Business ParseBusiness(DataRow row)
        {
            try
            {
                int id = (int)row[BusinessesColumns.ID];
                String name = row[BusinessesColumns.NAME].ToString();
                String description = row[BusinessesColumns.DESCRIPTION].ToString();
                String address = row[BusinessesColumns.ADDRESS].ToString();
                String city = row[BusinessesColumns.CITY].ToString();
                int ownerId = (int)row[BusinessesColumns.OWNER_ID];

                Business business = new Business(id, name, description, ownerId, address, city);
                return business;
            }
            catch
            {
                return null; // failed
            }
        }
    }
}
