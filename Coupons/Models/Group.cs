using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.Models
{
 

    public class Group
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

        public Group(int id, String name)
        {
            mId = id;
            mName = name;
        }


    }
}
