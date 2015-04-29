using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coupons.DAL;
using Coupons.Models;

namespace Coupons.BL
{
    class UserBL
    {
        UserDAL mUserDal;
        
        public UserBL()
        {
            mUserDal = new UserDAL();
        }

        public User login(String username, String password)
        {
            return mUserDal.login(username, password);
        }

        public List<Client> getAllClients()
        {
            return mUserDal.getAllClients();
        }

        public List<BusinessOwner> getAllBusinessOwner()
        {
            return mUserDal.getAllBusinessOwner();
        }
    }
}
