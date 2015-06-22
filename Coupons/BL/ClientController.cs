using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coupons.Models;
using Coupons.Enums;
using Coupons.Constants;
using System.Data;
using Coupons.DAL;

namespace Coupons.BL
{
    public class ClientController
    {
        public ClientDAL mDal;
        private DataParser mParser;

        public ClientController()
        {
            mDal = new ClientDAL();
            mParser = new DataParser();
        }

        public bool InsertNewClient(String username, String password, String mail, String phone, DateTime birthDate, Gender gender, String location)
        {
            bool isInsertedClient = mDal.InsertNewClient(username, password, mail, phone);
            if (isInsertedClient)
            {
                int clientId = GetClientId(username, password);
                bool isInsertedInformation = mDal.InsertClientInformation(clientId, birthDate, gender, location);
                if (isInsertedInformation)
                {
                    mDal.DeleteClientById(clientId);
                    return false;
                }

            }
            return true;

        }

        public bool CreateNewGroup(int managerId, String name)
        {
            return mDal.CreateNewGroup(managerId, name);
        }

        public Group GetGroupByName(String groupName)
        {
            DataTable table =  mDal.GetGroupByName(groupName);
            if (table.Rows.Count == 1){
                Group group = mParser.ParseGroup(table.Rows[0]);
                return group;
            } else{
                // Not found
                return null;
            }
        }

        public bool AddClientToGroup(int clientId, int groupId)
        {
            return mDal.AddClientToGroup(clientId, groupId);
        }


        public int GetClientId(String username, String password)
        {
            DataTable table = mDal.GetClientId(username, password);;
            if (table.Rows.Count == 1)
            {
                DataRow row = table.Rows[0];
                if (row[UserColumns.TYPE].ToString().Equals("Client"))
                {
                    return (int)row[UserColumns.ID];
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }


        public int GetDealByBusinessId(int businessId)
        {
            return mDal.SelectDeal(businessId);
        }

        public int BuyCoupon(Deal deal, Client client)
        {
            return mDal.BuyCoupon(deal, client);
        }

        public List<Deal> GetDealById(int dealId)
        {
            return mDal.GetDealById(dealId);
        }

        public Client GetClientById(int clientId)
        {
            return mDal.GetClientById(clientId);
        }

        public Client GetClientByUsername(String username)
        {
            return mDal.GetClientByName(username);
        }


        public void LoadClientNetworks(Client client)
        {
            mDal.LoadClientNetworks(client);
        }

        public List<ClientNetwork> GetClientSocialNetworks(int clientId)
        {
            return mDal.GetClientSocialNetworks(clientId);
        }

        public List<Coupon> GetClientCouponsByClient(Client client)
        {
            return mDal.GetClientCouponsByClient(client);
        }
        
        public bool AddSocialNetwork(int clientId, SocialNetwork networkName, string username, string token)
        {
            return mDal.AddSocialNetwork(clientId, networkName, username, token);
        }

        public bool AddSocialFriend(ClientNetwork clientSocialNetwork, ClientNetwork friendSocialNetwork)
        {
            return mDal.AddSocialFriend(clientSocialNetwork, friendSocialNetwork);
        }

        public bool AddContact(int clientId, int friendId)
        {
            return mDal.AddContact(clientId, friendId);
        }

        public List<Client> GetSocialFriends(int clientId, SocialNetwork network)
        {
            return mDal.GetSocialFriends(clientId, network);
        }

        public List<Client> GetContacts(int clientId)
        {
            return mDal.GetContacts(clientId);
        }


        public List<Business> GetAllBusiness()
        {
            return mDal.GetAllBusinesses();
        }

        public List<String> GetPossibleCities()
        {
            return mDal.GetPossibleCities();
        }

        public List<Deal> GetAllDeal()
        {
            return mDal.GetAllDeals();
        }

        public void RateCoupon(Coupon coupon, int rate)
        {
            mDal.RateCoupon(coupon, rate);
        }
    }
}







