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

        public DataTable getDealsNotApproval()
        {
            DataTable table = mTableDeals.selectDealsNotApproval();
            return table;
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
