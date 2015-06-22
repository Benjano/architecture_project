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


        public bool InsertNewDeal(String name, String details, Business business, decimal price, DateTime experationDate, String startHour, String endHour)
        {
            int result = mTableDeals.InsertDeal(name, details, business.ID, price, experationDate, startHour, endHour);
            return result == 1;
        }

        public bool InsertNewBusiness(int ownerId, String name, String description, String city, String address)
        {
            return mTableBusiness.InsertBusiness(name, description, ownerId, address, city) == 1;
        }

        public DataTable GetBusinessesByOwnerId(int ownerId)
        {
            DataTable table = mTableBusiness.SelectBusinessByOwner(ownerId);
            return table;
        }

        public DataTable GetBusinessDealsByBusinessId(int businessId)
        {
            DataTable table = mTableDeals.SelectDealByBusinessId(businessId);
            return table;
        }

        public DataTable GetDealCoupons(int dealId)
        {
            CouponsDataset.CouponsDataTable table = mTableCoupons.SelectCouponByDealID(dealId);
            return table;
        }

        public DataTable GetBusinessOwnerByName(string userName)
        {
            DataTable table = mTableUsers.selectBusinessOwnerByUserName(userName);
            return table;
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

        public DataTable GetBusinessesByName(string name)
        {
            DataTable table = mTableBusiness.selectBusinessByName(name);
            return table;
        }

        public DataTable GetBusinessById(int businessId)
        {
            DataTable table = mTableBusiness.SelectBusinessById(businessId);
            return table;
        }

        public DataTable GetBusinessOwnerById(int businessOwnerId)
        {
            DataTable table = mTableUsers.selectBusinessOwnerById(businessOwnerId);
            return table; 
        }

        public DataTable GetAllDealsByBussinesId(int businessId)
        {
            DataTable table = mTableDeals.SelectDealByBusinessId(businessId);
            return table;
        }

        public DataTable GetDealById(int dealId)
        {
            DataTable table = mTableDeals.SelectDealById(dealId);
            return table;
        }

        public DataTable GetAllCouponByDealId(int dealId)
        {
            DataTable table = mTableCoupons.SelectCouponByDealID(dealId);
            return table;
        }

    }
}
