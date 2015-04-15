using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.Models
{
    public abstract class User
    {
        private int mId;
        public int ID
        {
            get { return mId; }
        }

        private String mUsername;
        public String Username
        {
            get { return mUsername; }
        }

        private String mMail;
        public String Mail
        {
            get { return mMail; }
            set { mMail = value; }
        }

        private String mPhone;
        public String Phone
        {
            get { return mPhone; }
            set { mPhone = value; }
        }

        public User(int id, String username, String mail, String phone)
        {
            mId = id;
            mUsername = username;
            mMail = mail;
            mPhone = phone;
        }
    }
}
