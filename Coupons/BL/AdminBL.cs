using Coupons.DAL;
using Coupons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.BL
{
    class AdminBL
    {
        AdminDAL mDal;
        public AdminBL()
        {
            mDal = new AdminDAL();
        }
        
        public bool insertBusinessOwner(String username, String password, String mail, String phone)
        {
            return mDal.insertBusinessOwner(username, password, mail, phone);
        }

        public List<Deal> getDealsNotApproval()
        {
            return mDal.getDealsNotApproval();
        }

        public bool deleteBusiness(int businessId)
        {
            return mDal.deleteBusiness(businessId);
        }

        public bool deleteDeal(Deal mSelectedDeal)
        {

            return mDal.deleteDeal(mSelectedDeal.ID);
        }

        public bool ApproveDeal(Deal mSelectedDeal)
        {
            return mDal.approveDeal(mSelectedDeal.ID);
        }
    }
}
