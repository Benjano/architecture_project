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
    public class ClientBL
    {
        public ClientDAL mDal;

        public ClientBL()
        {
            mDal = new ClientDAL();
        }

        public bool insertNewClient(String username, String password, String mail, String phone, DateTime birthDate, Gender gender, String location)
        {

            return mDal.insertNewClient(username, password, mail, phone, birthDate, gender, location);

        }

        public bool createNewGroup(int managerId, String name)
        {
            return mDal.createNewGroup(managerId, name);
        }

        public int findGroupId(String groupName)
        {
            return mDal.findGroupId(groupName);
        }

        public bool addClientToGroup(int clientId, int groupId)
        {
            return mDal.addClientToGroup(clientId, groupId);
        }

        public Client logClient(String username, String password)
        {
            return mDal.logClient(username, password);
        }

        public int findClientId(String username, String password)
        {
            return mDal.findClientId(username, password);
        }

        public int updateClient(String username, String password, String mail, String phone, String originalPassword)
        {
            return mDal.updateClient(username, password, mail, phone, originalPassword);

        }

        public int selectDeal(int businessId)
        {
            return mDal.selectDeal(businessId);
        }

        public int buyCoupon(Deal deal, Client client)
        {
            return mDal.buyCoupon(deal, client);
        }

        public List<Deal> getDealById(int dealId)
        {
            return mDal.getDealById(dealId);
        }

        public Client getClientById(int clientId)
        {
            return mDal.getClientById(clientId);
        }

        public Client getClientByUsername(String username)
        {
            return mDal.getClientByName(username);
        }


        public void loadClientNetworks(Client client)
        {
            mDal.loadClientNetworks(client);
        }

        public List<ClientNetwork> getClientSocialNetworks(int clientId)
        {
            return mDal.getClientSocialNetworks(clientId);
        }

        public List<Coupon> getClientCouponsByClient(Client client)
        {
            return mDal.getClientCouponsByClient(client);
        }
        
        public bool addSocialNetwork(int clientId, SocialNetwork networkName, string username, string token)
        {
            return mDal.addSocialNetwork(clientId, networkName, username, token);
        }

        public bool addSocialFriend(ClientNetwork clientSocialNetwork, ClientNetwork friendSocialNetwork)
        {
            return mDal.addSocialFriend(clientSocialNetwork, friendSocialNetwork);
        }

        public bool addContact(int clientId, int friendId)
        {
            return mDal.addContact(clientId, friendId);
        }

        public List<Client> getSocialFriends(int clientId, SocialNetwork network)
        {
            return mDal.getSocialFriends(clientId, network);
        }

        public List<Client> getContacts(int clientId)
        {
            return mDal.getContacts(clientId);
        }


        public List<Business> getAllBusiness()
        {
            return mDal.getAllBusinesses();
        }

        public List<String> getPossibleCities()
        {
            return mDal.getPossibleCities();
        }

        public List<Deal> getAllDeal()
        {
            return mDal.getAllDeals();
        }
    }
}







