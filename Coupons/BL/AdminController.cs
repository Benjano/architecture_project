using Coupons.DAL;
using Coupons.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.BL
{
    class AdminController
    {
        AdminDAL mDal;
        DataParser mParser;

        public AdminController()
        {
            mDal = new AdminDAL();
            mParser = new DataParser();
        }
        
        public bool insertBusinessOwner(String username, String password, String mail, String phone)
        {
            if (username.Length < 50 & username.Length > 0 & password.Length < 50 & password.Length > 0 & phone.Length < 20 & phone.Length > 0)
                return mDal.insertBusinessOwner(username, password, mail, phone);
            return false;
        }

        public List<Deal> getDealsNotApproval()
        {
            DataTable notApprovedDeals = mDal.getDealsNotApproval();
            List<Deal> result = mParser.ParseDeals(notApprovedDeals);
            return result;
        }

        public bool deleteBusiness(int businessId)
        {
            if (businessId < 1000 & businessId >= 0)
                return mDal.deleteBusiness(businessId);
            return false;
        }

        public bool deleteDeal(Deal mSelectedDeal)
        {
            return mDal.deleteDeal(mSelectedDeal.ID);
        }

        public bool ApproveDeal(Deal selectedDeal)
        {
            if (selectedDeal != null)
                return mDal.approveDeal(selectedDeal.ID);
            else
                return false;
        }

        public bool updateDeal(Deal selectedDeal, string name, string details, decimal originalPrice, DateTime experationDate, int startHour_h, int startHour_m, int endHour_h, int endHour_m)
        {
            if (startHour_h >= 0 & startHour_h < 24 & startHour_m >= 0 & startHour_m < 60 & endHour_h >= 0 & endHour_h < 24 & endHour_m >= 0 & endHour_m < 60)
                if (selectedDeal != null & name.Length < 50 & name.Length > 0 & originalPrice > 0)
                    return mDal.updateDeal(selectedDeal, name, details, originalPrice, experationDate, startHour_h + ":" + startHour_m, endHour_h + ":" + endHour_m);
            return false;
        }
    }
}
