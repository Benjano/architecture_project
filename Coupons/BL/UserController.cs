using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using Coupons.DAL;
using Coupons.Models;

namespace Coupons.BL
{
    class UserController
    {
        UserDAL mUserDal;

        public UserController()
        {
            mUserDal = new UserDAL();
        }

        public User login(String username, String password)
        {
            return mUserDal.login(username, password);
        }

        public bool resetPassword(String username)
        {
            string Email = mUserDal.getUserEmailByUsername(username);
            if (Email != "")
            {
                MailMessage message = new MailMessage("couponServes@gmail.com", Email, "Coupon password reset", "your new password is ABC123DSA");
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential("couponServes", "Cou@pon432Se");
                smtp.EnableSsl = true;
                smtp.Send(message);
                return true;
            }
            return false ;
        }

        public List<Client> getAllClients()
        {
            return mUserDal.getAllClients();
        }

        public List<BusinessOwner> getAllBusinessOwner()
        {
            return mUserDal.getAllBusinessOwner();
        }

        public bool UpdateUser(int clientId, String username, String password, String mail, String phone, String originalPassword)
        {
            return mUserDal.UpdateUser(clientId, username, password, mail, phone, originalPassword);
        }

    }
}
