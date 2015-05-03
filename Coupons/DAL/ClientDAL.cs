using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coupons.Models;
using Coupons.Enums;
using Coupons.Constants;
using System.Data;
using Coupons.BL;

namespace Coupons.DAL
{
    public class ClientDAL
    {
        
        private CouponsDatasetTableAdapters.UsersTableAdapter mTableUsers = new CouponsDatasetTableAdapters.UsersTableAdapter();
        private CouponsDatasetTableAdapters.ClientsTableAdapter mTableClients = new CouponsDatasetTableAdapters.ClientsTableAdapter();
        private CouponsDatasetTableAdapters.DealsTableAdapter mTableDeals = new CouponsDatasetTableAdapters.DealsTableAdapter();
        private CouponsDatasetTableAdapters.CouponsTableAdapter mTableCoupons = new CouponsDatasetTableAdapters.CouponsTableAdapter();
        private CouponsDatasetTableAdapters.GroupsTableAdapter mTableGroups = new CouponsDatasetTableAdapters.GroupsTableAdapter();
        private CouponsDatasetTableAdapters.ClientsInGroupTableAdapter mTableClientsInGroups = new CouponsDatasetTableAdapters.ClientsInGroupTableAdapter();
        private CouponsDatasetTableAdapters.ClientSocialNetworkTableAdapter mTableSocialNetworks = new CouponsDatasetTableAdapters.ClientSocialNetworkTableAdapter();
        private CouponsDatasetTableAdapters.SocialFriendsTableAdapter mTableSocialFriends = new CouponsDatasetTableAdapters.SocialFriendsTableAdapter();
        private CouponsDatasetTableAdapters.FriendsTableAdapter mTableFriends = new CouponsDatasetTableAdapters.FriendsTableAdapter();
        private CouponsDatasetTableAdapters.BusinessesTableAdapter mTableBusiness = new CouponsDatasetTableAdapters.BusinessesTableAdapter();
        
        public bool insertNewClient(String username, String password, String mail, String phone, DateTime birthDate, Gender gender, String location)
        {
            bool isOk = (mTableUsers.InsertClient(username, password, mail, phone) == 1);
            if (isOk){
                int clientId = findClientId(username, password);
                if (clientId >= 0){
                    isOk = (mTableClients.InsertClient(clientId, birthDate.ToShortDateString(), gender.ToString(), location) == 1);
                    if (!isOk)
                    {
                        mTableUsers.DeleteUser(clientId);
                    }
                } else {
                    isOk = false;
                }
            }
            return isOk;

        }

        public bool createNewGroup(int managerId, String name)
        {
            bool isOk = (mTableGroups.CreateNewGroup(name, managerId) == 1);
            return isOk;
        }

        public int findGroupId(String groupName) 
        {
            CouponsDataset.GroupsDataTable group = mTableGroups.SelectGroupIdByName(groupName);
            if (group.Rows.Count >= 1)
            {
                DataRow row = group[0];
               return (int)row[GroupColumns.ID];
            }
            else
            {
                return -1;
            }
        }

        public bool addClientToGroup(int clientId, int groupId)
        {
            bool isOk = (mTableClientsInGroups.AddClientToGroup(clientId, groupId) == 1);
            return isOk;
        }

        public Client logClient(String username, String password)
        {
            return null;
        }

        public int findClientId(String username, String password)
        {
            CouponsDataset.UsersDataTable user = mTableUsers.SelectUser(username, password);
            if (user.Rows.Count == 1){
                DataRow row = user[0];
                if (row[UserColumns.TYPE].ToString().Equals("Client"))
                {
                    return (int) row[UserColumns.ID];
                }
                else
                {
                    return -1;
                }
            } else {
                return -1;
            }
        }

        public int updateClient(String username, String password, String mail, String phone, String originalPassword)
        { 
             CouponsDataset.UsersDataTable user = mTableUsers.SelectUser(username, originalPassword);
             if (user.Rows.Count == 1){
                DataRow row = user[0];
                int id = (int)row[UserColumns.ID];
                mTableUsers.UpdateUser(password, mail, phone, id, originalPassword);
                return id;
             }
             else
             {
                 return -1;
             }
        
        }

        public int selectDeal(int businessId)
        {
             CouponsDataset.DealsDataTable deal = mTableDeals.SelectDealByBusinessId(businessId);
             if (deal.Rows.Count >= 1) {
                 DataRow row = deal[0];
                 int id = (int)row[DealsColumns.ID];
                 return id;
             }
            else
             {
                 return -1;
             }
        }

        public int buyCoupon(Deal deal, Client client)
        {
            int result = 0; 
            //TODO   clac bought price
             result = mTableCoupons.InsertCoupon(deal.ID, client.ID, deal.Price, deal.Price);
             CouponsDataset.CouponsDataTable coupons = mTableCoupons.SelectCoupon(deal.ID, client.ID);
           if (coupons.Rows.Count >= 1)
           {
                DataRow row = coupons[0];

                int id = (int)row[CouponsColumns.ID];
                decimal originalPrice = (decimal)row[CouponsColumns.ORIGINAL_PRICE];
                decimal boughtPrice = (decimal)row[CouponsColumns.BOUGHT_PRICE];
                bool isUsed = (row[CouponsColumns.IS_USED].ToString().Equals("True"));
                String serialKey = row[CouponsColumns.SERIAL_KEY].ToString();
 
                
                   Coupon coupon = new Coupon(client, deal, originalPrice, boughtPrice, -1, isUsed, serialKey );
                   client.addCoupon(coupon);
                   return id;
            }
           return -1;
        }

        public Deal getDealById(int dealId)
        {
            CouponsDataset.DealsDataTable deals = mTableDeals.SelectDealById(dealId);
            if (deals.Rows.Count == 1) 
            {
                DataRow row = deals[0];
                int id = (int)row[DealsColumns.ID];
                String name = row[DealsColumns.NAME].ToString();
                String details = row[DealsColumns.DETAILS].ToString(); ;
                decimal originalPrice = (decimal)row[DealsColumns.ORIGINAL_PRICE];
                float rate = (float)(double)row[DealsColumns.RATE];
                DateTime experationDate;
                DateTime.TryParse(row[DealsColumns.EXPERATION_DATE].ToString(), out experationDate);
                bool isApproved = (row[DealsColumns.IS_APPROVED].ToString().Equals("True"));
                int businessId = (int)row[DealsColumns.BUSINESS_ID];
                
                AdminDAL mAdminDAL = new AdminDAL();
                Business business = mAdminDAL.selectBusinessById(businessId);


                Deal deal = new Deal(id, name, details, business, originalPrice, rate, experationDate, isApproved);
                return deal;
            }
            else 
            { 
                return null; 
            }
        }

        public Client getClientById(int clientId){
            CouponsDataset.ClientsDataTable clients = mTableClients.selectClientById(clientId);

            if (clients.Rows.Count == 1)
            {
                DataRow row = clients.Rows[0];
                int id = (int)row[ClientColumns.USER_ID];
                String username = row[UserColumns.USERNAME].ToString();
                String mail = row[UserColumns.MAIL].ToString();
                String phone = row[UserColumns.PHONE].ToString();
                      
                DateTime birthDate;
                DateTime.TryParse(row[ClientColumns.BIRTHDATE].ToString(), out birthDate);
                Gender gender = (Gender)Enum.Parse(typeof(Gender), clients.Rows[0][ClientColumns.GENDER].ToString());
                String location = row[ClientColumns.LOCATION].ToString();

                Client client = new Client(id, username, mail, phone, birthDate, Gender.Female, location);
                return client;
            }
            return null;
        }

        public Client getClientByName(String username)
        {
            CouponsDataset.ClientsDataTable clients = mTableClients.SelectClientByName(username);

            if (clients.Rows.Count == 1)
            {
                int id = (int)clients.Rows[0][ClientColumns.USER_ID];
                String mail = clients.Rows[0][UserColumns.MAIL].ToString();
                String phone = clients.Rows[0][UserColumns.PHONE].ToString();

                DateTime birthDate;
                DateTime.TryParse(clients.Rows[0][ClientColumns.BIRTHDATE].ToString(), out birthDate);
                Gender gender = (Gender)Enum.Parse(typeof(Gender), clients.Rows[0][ClientColumns.GENDER].ToString());
                String location = clients.Rows[0][ClientColumns.LOCATION].ToString();

                Client client = new Client(id, username, mail, phone, birthDate, Gender.Female, location);
                return client;
            }
            return null;
        }

        public void loadClientNetworks(Client client)
        {
            CouponsDataset.ClientSocialNetworkDataTable networks = mTableSocialNetworks.SelectClientSocialNetworks(client.ID);
            foreach (DataRow row in networks.Rows)
            {
                String name = row[SocialNetworksColumns.NAME].ToString();
                SocialNetwork network = (SocialNetwork) Enum.Parse( typeof(SocialNetwork), name, true );
                client.addSocialNetwork(network);
            }
        }

        public List<ClientNetwork> getClientSocialNetworks(int clientId)
        {
                CouponsDataset.ClientSocialNetworkDataTable networks = mTableSocialNetworks.SelectClientSocialNetworks(clientId);
            List<ClientNetwork> clientNetworks = new List<ClientNetwork>();
            foreach (DataRow row in networks.Rows)
            {
                String name = row[SocialNetworksColumns.NAME].ToString();
                String username = row[SocialNetworksColumns.USERNAME].ToString();
                String token = row[SocialNetworksColumns.TOKEN].ToString();
                SocialNetwork network = (SocialNetwork) Enum.Parse( typeof(SocialNetwork), name, true );
                ClientNetwork clientNetwork = new ClientNetwork(clientId, network, username, token);
                clientNetworks.Add(clientNetwork);
            }
            return clientNetworks;
        }

        public bool addSocialNetwork(int clientId, SocialNetwork networkName, string username, string token ) {
            return mTableSocialNetworks.InsertClientSocialNetwork(clientId, networkName.ToString(), username, token) == 1;
        }
        
        public bool addSocialFriend(ClientNetwork clientSocialNetwork, ClientNetwork friendSocialNetwork)
        {
            if (clientSocialNetwork.Name == friendSocialNetwork.Name)
                return mTableSocialFriends.InsertSocialFriend(clientSocialNetwork.ClientId, friendSocialNetwork.ClientId, clientSocialNetwork.Name.ToString()) == 1 &&
                     mTableSocialFriends.InsertSocialFriend(friendSocialNetwork.ClientId, clientSocialNetwork.ClientId, friendSocialNetwork.Name.ToString()) == 1;
            return false;
        }

        public bool addContact(int clientId, int friendId)
        {
            return mTableFriends.InsertFriend(clientId, friendId) == 1;
        }

        public List<Client> getSocialFriends(int clientId, SocialNetwork network)
        {
            CouponsDataset.SocialFriendsDataTable socialFriends = mTableSocialFriends.SelectClientSocialNetwork(clientId, network.ToString());

            List<Client> result = new List<Client>();
            foreach (DataRow row in socialFriends)
            {
                int friendId = (int)row[SocialFriendsColumns.FRIEND_ID];
                Client client = getClientById(friendId);

                result.Add(client);
            }

            return result;
        }

        public List<Client> getContacts(int clientId){
            CouponsDataset.FriendsDataTable socialFriends = mTableFriends.SelectClientContacts(clientId);

            List<Client> result = new List<Client>();
            foreach (DataRow row in socialFriends)
            {
                int friendId = (int)row[FriendsColumns.FRIEND_ID];
                Client client = getClientById(friendId);

                result.Add(client);
            }

            return result;
        }


        internal List<Business> getAllDeal()
        {
            List<Business> result = new List<Business>();
            DataTable Business = mTableBusiness.selectAllBusiness();

            foreach (DataRow row in Business.Rows)
            {
                int id = (int)row[BusinessesColumns.ID];
                String name = (String)row[BusinessesColumns.NAME];
                String description = (String)row[BusinessesColumns.DESCRIPTION];
                int ownerId = (int)row[BusinessesColumns.OWNER_ID];
                String address = (String)row[BusinessesColumns.ADDRESS];
                String city = (String)row[BusinessesColumns.CITY];
                

                result.Add(new Business(id, name, description, ownerId, address,city));
            }
            return result;
        }
    }
}
