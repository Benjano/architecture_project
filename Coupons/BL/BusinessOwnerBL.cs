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

        public BusinessOwnerBL(BusinessOwnerDAL dal)
        {
            mDal = new BusinessOwnerDAL(); ;
        }

        public BusinessOwner logBusinessOwner(String username, String password)
        {
            return mDal.logBusinessOwner(username, password);
        }

        public bool insertNewDeal(String name, String details, Business business, decimal price, DateTime experationDate)
        {
            return mDal.insertNewDeal(name, details, business, price, experationDate);
        }

        public void loadBusinesses(BusinessOwner owner)
        {
            mDal.loadBusinesses(owner);
        }

        private void loadDealsToBusiness(Business business)
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

        private void loadDealCoupons(Deal deal)
        {
            CouponsDatasetTableAdapters.CouponsTableAdapter mTableCoupons = new CouponsDatasetTableAdapters.CouponsTableAdapter();
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
            return mDal.getBusinessOwnerById(userName, password);
        }

        public bool UpdateBusiness(int businessId, String name, String description, int ownerId, String address, String city)
        {
            return mDal.UpdateBusiness(businessId, name, description, ownerId, address, city);
        }
    }
}

