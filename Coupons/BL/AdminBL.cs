using Coupons.DAL;
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
    }
}
