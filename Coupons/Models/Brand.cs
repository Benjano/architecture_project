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

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            Brand brandObj = (Brand)obj;
            if (this.Name.Equals(brandObj.Name) && this.ID.Equals(brandObj.ID))
            {
                return true;
            }
            
            
            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
