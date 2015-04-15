using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.Models
{
    public class BusinessOwner:User
    {

        private List<Business> mBusinesses;
        public List<Business> Businesses
        {
            get { return mBusinesses; }
        }


        public BusinessOwner(int id, String username, String mail, String phone) : base(id, username, mail, phone) 
        { 
            mBusinesses = new List<Business>();
        }


        public bool addBusiness(Business business)
        {
            bool result = false;
            if (!mBusinesses.Contains(business))
            {
                mBusinesses.Add(business);
                result = true;
            }

            return result;
        }


        public void loadBusiness()
        {
            
        }

    }
}
