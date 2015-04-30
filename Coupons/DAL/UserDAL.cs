using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coupons.Models;
using Coupons.Constants;
using Coupons.Enums;

namespace Coupons.DAL
{
    class UserDAL
    {
        CouponsDatasetTableAdapters.UsersTableAdapter mTableUsers = new CouponsDatasetTableAdapters.UsersTableAdapter();
        CouponsDatasetTableAdapters.ClientsTableAdapter mTableClient = new CouponsDatasetTableAdapters.ClientsTableAdapter();


        public User login(String username, String password)
        {
            CouponsDataset.UsersDataTable user = mTableUsers.SelectUser(username, password);

            if (user.Rows.Count == 1)
            {
                int id = (int) user.Rows[0][UserColumns.ID];
                String mail = (String)user.Rows[0][UserColumns.MAIL]; ;
                String phone = (String)user.Rows[0][UserColumns.PHONE];
                String type = (String) user.Rows[0][UserColumns.TYPE];

                switch (type){
                    case ("Admin"):
                        return new Admin(id, username, mail, phone);
                        
                    case ("BusinessOwner"):
                        return new BusinessOwner(id, username, mail, phone);
                        
                    case ("Client"):
                        CouponsDataset.ClientsDataTable client =  mTableClient.selectClientById(id);
                        if (client.Rows.Count == 1)
                        {
                            DateTime birthDate;
                            DateTime.TryParse(client.Rows[0][ClientColumns.BIRTHDATE].ToString(), out birthDate);
                            Gender gender = (Gender)Enum.Parse(typeof(Gender), client.Rows[0][ClientColumns.GENDER].ToString());
                            String location = client.Rows[0][ClientColumns.LOCATION].ToString();
                            return new Client(id, username, mail, phone,birthDate,gender,location);
                        }
                        break;
                        

                }
            }

            return null;
        }


    }
}
