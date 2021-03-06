﻿using System;
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
        private CouponsDatasetTableAdapters.BusinessCategoriesTableAdapter mTableBusinessCategory = new CouponsDatasetTableAdapters.BusinessCategoriesTableAdapter();


        public bool InsertNewClient(String username, String password, String mail, String phone)
        {
            return (mTableUsers.InsertClient(username, password, mail, phone, "Client") == 1);
        }

        public bool InsertClientInformation(int clientId, DateTime birthDate, Gender gender, String location)
        {
            return (mTableClients.InsertClient(clientId, birthDate.ToShortDateString(), gender.ToString(), location) == 1);
        }

        public bool DeleteClientById(int clientId)
        {
            return (mTableUsers.DeleteUser(clientId) == 0);
        }

        public bool CreateNewGroup(int managerId, String name)
        {
            return (mTableGroups.CreateNewGroup(name, managerId) == 1);
        }

        public DataTable GetGroupByName(String groupName)
        {
            return mTableGroups.SelectGroupIdByName(groupName);
        }

        public bool AddClientToGroup(int clientId, int groupId)
        {
            return (mTableClientsInGroups.AddClientToGroup(clientId, groupId) == 1);
        }

        public DataTable GetClientId(String username, String password)
        {
            return mTableUsers.SelectUser(username, password);
        }

     

        public int SelectDeal(int businessId)
        {
            CouponsDataset.DealsDataTable deal = mTableDeals.SelectDealByBusinessId(businessId);
            if (deal.Rows.Count >= 1)
            {
                DataRow row = deal[0];
                int id = (int)row[DealsColumns.ID];
                return id;
            }
            else
            {
                return -1;
            }
        }

        public bool InsertCoupon(int dealId, int clientId, decimal dealPrice, string serialKey)
        {
           int result = 0;
           result = mTableCoupons.InsertCoupon(dealId, clientId, dealPrice, serialKey);
           return result > 0;
            /*
           / int result = 0;
            //TODO   clac bought price
            result = mTableCoupons.InsertCoupon(deal.ID, client.ID, deal.Price, deal.Price);
            CouponsDataset.CouponsDataTable coupons = mTableCoupons.SelectCoupon(deal.ID, client.ID);
            if (coupons.Rows.Count >= 1)
            {
                DataRow row = coupons[0];

                int id = (int)row[CouponsColumns.ID];
                decimal originalPrice = (decimal)row[CouponsColumns.ORIGINAL_PRICE];
                decimal boughtPrice = (decimal)row[CouponsColumns.BOUGHT_PRICE];
                bool isUsed = (row[CouponsColumns.IS_USED].ToString().Equals("True      "));
                String serialKey = row[CouponsColumns.SERIAL_KEY].ToString();
                int rate = Convert.ToInt32(row[CouponsColumns.RATE]);


                Coupon coupon = new Coupon(id, client.ID, deal.ID, originalPrice, boughtPrice, rate, isUsed, serialKey);
                client.addCoupon(coupon);
                return id;
            }
            return -1;*/
        }

        public DataTable GetCouponBySerialKey(string serialKey)
        {
            return mTableCoupons.SelectCouponBySerialKey(serialKey);
        }

        public List<Deal> GetDealById(int dealId)
        {
            CouponsDataset.DealsDataTable deals = mTableDeals.SelectDealById(dealId);
            List<Deal> result = new List<Deal>();
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
                bool isApproved = (row[DealsColumns.IS_APPROVED].ToString().Equals("True      "));
                int businessId = (int)row[DealsColumns.BUSINESS_ID];
                String startHour = row[DealsColumns.START_HOUR].ToString();
                String endHour = row[DealsColumns.END_HOUR].ToString();


                Deal deal = new Deal(id, name, details, businessId, originalPrice, rate, experationDate, isApproved, startHour, endHour);
                result.Add(deal);
            }
            return result;
        }

        public List<Coupon> GetClientCouponsByClient(Client client)
        {
            CouponsDataset.CouponsDataTable coupons = mTableCoupons.SelectAllClientCoupons(client.ID);
            List<Coupon> result = new List<Coupon>();
            foreach (DataRow row in coupons.Rows)
            {

                int id = (int)row[CouponsColumns.ID];
                int dealId = (int)row[CouponsColumns.DEAL_ID];
                int boughtPrice = Convert.ToInt32(row[CouponsColumns.BOUGHT_PRICE]);
                int rate = Convert.ToInt32(row[CouponsColumns.RATE]);
                bool isUsed = (row[CouponsColumns.IS_USED].ToString().Equals("True      "));
                String serialKey = row[CouponsColumns.SERIAL_KEY].ToString();

                Coupon coupon = new Coupon(id, client.ID, dealId, boughtPrice, rate, isUsed, serialKey);
                result.Add(coupon);
            }
            return result;
        }

        public Client GetClientById(int clientId)
        {
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
                DateTime timestamp;
                DateTime.TryParse(row[ClientColumns.TIMESTAMP].ToString(), out timestamp);

                Client client = new Client(id, username, mail, phone, birthDate, Gender.Female, new Location(location), timestamp);
                return client;
            }
            return null;
        }

        public Client GetClientByName(String username)
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
                DateTime timestamp;
                DateTime.TryParse(clients.Rows[0][ClientColumns.TIMESTAMP].ToString(), out timestamp);

                Client client = new Client(id, username, mail, phone, birthDate, Gender.Female, new Location(location), timestamp);
                return client;
            }
            return null;
        }

        public void LoadClientNetworks(Client client)
        {
            CouponsDataset.ClientSocialNetworkDataTable networks = mTableSocialNetworks.SelectClientSocialNetworks(client.ID);
            foreach (DataRow row in networks.Rows)
            {
                String name = row[SocialNetworksColumns.NAME].ToString();
                SocialNetwork network = (SocialNetwork)Enum.Parse(typeof(SocialNetwork), name, true);
                client.addSocialNetwork(network);
            }
        }

        public List<ClientNetwork> GetClientSocialNetworks(int clientId)
        {
            CouponsDataset.ClientSocialNetworkDataTable networks = mTableSocialNetworks.SelectClientSocialNetworks(clientId);
            List<ClientNetwork> clientNetworks = new List<ClientNetwork>();
            foreach (DataRow row in networks.Rows)
            {
                String name = row[SocialNetworksColumns.NAME].ToString();
                String username = row[SocialNetworksColumns.USERNAME].ToString();
                String token = row[SocialNetworksColumns.TOKEN].ToString();
                SocialNetwork network = (SocialNetwork)Enum.Parse(typeof(SocialNetwork), name, true);
                ClientNetwork clientNetwork = new ClientNetwork(clientId, network, username, token);
                clientNetworks.Add(clientNetwork);
            }
            return clientNetworks;
        }

        public bool AddSocialNetwork(int clientId, SocialNetwork networkName, string username, string token)
        {
            return mTableSocialNetworks.InsertClientSocialNetwork(clientId, networkName.ToString(), username, token) == 1;
        }

        public bool AddSocialFriend(ClientNetwork clientSocialNetwork, ClientNetwork friendSocialNetwork)
        {
            if (clientSocialNetwork.Name == friendSocialNetwork.Name)
                return mTableSocialFriends.InsertSocialFriend(clientSocialNetwork.ClientId, friendSocialNetwork.ClientId, clientSocialNetwork.Name.ToString()) == 1 &&
                     mTableSocialFriends.InsertSocialFriend(friendSocialNetwork.ClientId, clientSocialNetwork.ClientId, friendSocialNetwork.Name.ToString()) == 1;
            return false;
        }

        public bool AddContact(int clientId, int friendId)
        {
            return mTableFriends.InsertFriend(clientId, friendId) == 1;
        }

        public List<Client> GetSocialFriends(int clientId, SocialNetwork network)
        {
            CouponsDataset.SocialFriendsDataTable socialFriends = mTableSocialFriends.SelectClientSocialNetwork(clientId, network.ToString());

            List<Client> result = new List<Client>();
            foreach (DataRow row in socialFriends)
            {
                int friendId = (int)row[SocialFriendsColumns.FRIEND_ID];
                Client client = GetClientById(friendId);

                result.Add(client);
            }

            return result;
        }

        public List<Client> GetContacts(int clientId)
        {
            CouponsDataset.FriendsDataTable socialFriends = mTableFriends.SelectClientContacts(clientId);

            List<Client> result = new List<Client>();
            foreach (DataRow row in socialFriends)
            {
                int friendId = (int)row[FriendsColumns.FRIEND_ID];
                Client client = GetClientById(friendId);

                result.Add(client);
            }

            return result;
        }


        internal List<Business> GetAllBusinesses()
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

                Business business = new Business(id, name, description, ownerId, address, city); 

                DataTable categories = mTableBusinessCategory.SelectBusinessCategory(id);

                foreach (DataRow catRow in categories.Rows)
                    business.addCategory((Category) Enum.Parse(typeof(Category), catRow["Name"].ToString()));
                
                result.Add(business);
            }
            return result;
        }

        public List<Deal> GetAllDeals()
        {
            List<Deal> result = new List<Deal>();
            CouponsDataset.DealsDataTable deals = mTableDeals.selectAllDeals();
            foreach (DataRow row in deals.Rows)
            {
                int id = (int)row[DealsColumns.ID];
                String name = row[DealsColumns.NAME].ToString();
                String details = row[DealsColumns.DETAILS].ToString(); ;
                decimal originalPrice = (decimal)row[DealsColumns.ORIGINAL_PRICE];
                float rate = (float)(double)row[DealsColumns.RATE];
                DateTime experationDate;
                DateTime.TryParse(row[DealsColumns.EXPERATION_DATE].ToString(), out experationDate);
                bool isApproved = (row[DealsColumns.IS_APPROVED].ToString().Equals("True"));
                int businessId = (int)row[DealsColumns.BUSINESS_ID];
                String startHour = row[DealsColumns.START_HOUR].ToString();
                String endHour = row[DealsColumns.END_HOUR].ToString();


                Deal deal = new Deal(id, name, details, businessId, originalPrice, rate, experationDate, isApproved, startHour, endHour);
                result.Add(deal);
            }
            return result;
        }



        public List<string> GetPossibleCities()
        {
        

            List<string> result = new List<string>();
            DataTable cities = mTableBusiness.selectAllBusiness();

            foreach (DataRow row in cities.Rows)
            {
                string city = row[BusinessesColumns.CITY].ToString();
                string cityLower = city.ToLower();

                if (result.IndexOf(cityLower) == -1)
                    result.Add(cityLower);
            }

            return result;
        }

        public void RateCoupon(Coupon coupon, int rate)
        {
             mTableCoupons.rate(rate, coupon.ID);
        }

        public int UpdateUser(string userName, string newPassword, string newEmail, string newPhoneNum, string oldPassword)
        {
            int id = (int)GetClientId(userName, oldPassword).Rows[0][UserColumns.ID];
            return mTableUsers.UpdateUser(newPassword, newEmail, newPhoneNum, id, oldPassword);
        }
    }
}
