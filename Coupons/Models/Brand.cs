using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coupons.Models
{
    public class Brand
    {
         private int mId;
        public int ID
        {
            get { return mId; }
        }
        
        private String mName;
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public Brand(int id, String name)
        {
            mId = id;
            mName = name;
        }
    }
}
