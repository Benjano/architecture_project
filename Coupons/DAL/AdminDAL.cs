using Coupons.Constants;
using Coupons.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coupons.BL;



namespace Coupons.DAL
{



    public class AdminDAL
    {
        private CouponsDatasetTableAdapters.UsersTableAdapter mTableUsers = new CouponsDatasetTableAdapters.UsersTableAdapter();
        private CouponsDatasetTableAdapters.BusinessesTableAdapter mTableBusiness = new CouponsDatasetTableAdapters.BusinessesTableAdapter();
        private CouponsDatasetTableAdapters.DealsTableAdapter mTableDeals = new CouponsDatasetTableAdapters.DealsTableAdapter();
        private CouponsDatasetTableAdapters.CouponsTableAdapter mTableCoupons = new CouponsDatasetTableAdapters.CouponsTableAdapter();
        private CouponsDatasetTableAdapters.GroupsTableAdapter mTableGroups = new CouponsDatasetTableAdapters.GroupsTableAdapter();

        public bool insertBusinessOwner(String username, String password, String mail, String phone)
        {
            return (mTableUsers.InsertBusinessOwner(username, password, mail, phone) == 1);
        }

        public bool deleteUser(int userId)
        {
            return (mTableUsers.DeleteUser(userId) == 1);
        }

        public bool insertNewAdmin(String username, String password, String mail, String phone)
        {
            return (mTableUsers.InsertAdmin(username, password, mail, phone) == 1);
        }

        public bool insertNewBusiness(String name, String description, int ownerId, String address, String city)
        {
            return (mTableBusiness.InsertBusiness(name, description, ownerId, address, city) == 1);
        }

        public int findAdnimId(String username, String password)
        {
            CouponsDataset.UsersDataTable user = mTableUsers.SelectUser(username, password);
            if (user.Rows.Count == 1)
            {
                DataRow row = user[0];
                if (row[UserColumns.TYPE].ToString().Equals("Admin"))
                {
                    return (int)row[UserColumns.ID];
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

        }


        public int findBusinessId(int ownerId)
        {
            CouponsDataset.BusinessesDataTable business = mTableBusiness.SelectBusinessByOwner(ownerId);
            if (business.Rows.Count >= 1)
            {
                DataRow row = business[0];
                return (int)row[BusinessesColumns.ID];
            }
            else
            {
                return -1;
            }
        }

        public bool deleteBusiness(int Businessid)
        {
            return (mTableBusiness.DeleteBusiness(Businessid) == 1);
        }

        public Business selectBusinessById(int businessId){


            CouponsDataset.BusinessesDataTable businesses = mTableBusiness.SelectBusinessById(businessId);
            DataRow row = businesses[0];

            int idB = (int)row[BusinessesColumns.ID];
            String name = row[BusinessesColumns.NAME].ToString();
            String description = row[BusinessesColumns.DESCRIPTION].ToString();
            String address = row[BusinessesColumns.ADDRESS].ToString();
            String city = row[BusinessesColumns.CITY].ToString();
            int ownerId = (int) row[BusinessesColumns.OWNER_ID];
            // Find the business owner
            CouponsDataset.UsersDataTable ownertb = mTableUsers.selectUserById(ownerId);
            DataRow row2 = ownertb[0];

            int id = (int)row2[UserColumns.ID];
            String username = row2[UserColumns.USERNAME].ToString();
            String mail = row2[UserColumns.MAIL].ToString();
            String phone = row2[UserColumns.PHONE].ToString();
            
            BusinessOwner businessOwner = new BusinessOwner(id, username, mail, phone);


            Business business = new Business(idB, name, description, businessOwner.ID, address, city);
            return business;
        }

        public bool deleteCoupon(int couponId)
        {
            return mTableCoupons.DeleteCoupon(couponId) == 1;
        }

        public bool deleteDeal(int dealId)
        {
            return mTableDeals.DeleteDeal(dealId) == 1;
        }

        public bool deleteGroup(int groupId)
        {
            return mTableGroups.DeleteGroup(groupId) == 1;
        }

        public List<Deal> getDealsNotApproval()
        {
            List<Deal> result = new List<Deal>();
            CouponsDataset.DealsDataTable deals = mTableDeals.selectDealsNotApproval();
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
                int businessId = (int)row[DealsColumns.BUSINESS_ID];
                String startHour = row[DealsColumns.START_HOUR].ToString();
                String endHour = row[DealsColumns.END_HOUR].ToString();

            
                Deal deal = new Deal(id, name, details, businessId, originalPrice, rate, experationDate, isApproved, startHour, endHour);
                result.Add(deal);
            }
            return result;
        }

        public bool approveDeal(int dealId)
        {
            return  mTableDeals.approveDeal(dealId)==1 ;
        }

        public bool updateDeal(Deal selectedDeal, string name, string details, decimal originalPrice, DateTime experationDate, string startHour, string endHour)
        {
            return mTableDeals.UpdateDeal(name, details, originalPrice, experationDate, startHour, endHour, selectedDeal.ID)==1;
        }
    }

}
