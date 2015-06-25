using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coupons.BL;
using Coupons.DAL;
using Coupons;
using Coupons.Enums;
using Coupons.Models;
using Coupons.Util;

namespace CouponsTest
{
    [TestClass]
    public class DatabaseTest
    {
        ClientController mClientBL = new ClientController();
        BusinessOwnerController mBusinessOwnerBl = new BusinessOwnerController();

        public AdminDAL mAdminDAL = new AdminDAL();
        public ClientDAL mClientDAL = new ClientDAL();
        public BusinessOwnerDAL mBusinessDAL = new BusinessOwnerDAL();

        public String clientName = "TestAviv";
        public String clientPassword = "password1";
        public String clientMail = "avivasi@post.bgu.ac.il";
        public String clientPhone = "0546310736";
        public DateTime clientBornDate = new DateTime(1990, 4, 4);
        public Gender clientGender = Gender.Female;

        public String client2Name = "TestYarden";
        public String client2Password = "password2";
        public String client2Mail = "yardenChen@post.bgu.ac.il";
        public String client2Phone = "0546310736";
        public DateTime client2BornDate = new DateTime(1990, 4, 4);
        public Gender client2Gender = Gender.Other;
        
        [TestMethod]
        public void TestAddClient()
        {
            Assert.IsTrue(mClientBL.InsertNewClient("Test1Aviv", "password1", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null), "Client was not created");
            int createdClient = mClientBL.GetClientId("Test1Aviv", "password1");
            mAdminDAL.deleteUser(createdClient);
        }
        
        [TestMethod]
        public void TestAddAdminAndBusiness()
        {
            bool ans = mAdminDAL.insertNewAdmin("Test2Aviv23", "password20", "avivasi@post.bgu.ac.il", "054534533");
            Assert.IsTrue(ans, "Admin was not created");
            ans = mAdminDAL.insertBusinessOwner("Test2Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433");
            Assert.IsTrue(ans, "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessOwnerBl.getBusinessOwnerByUserName("Test2Aviv24").ElementAt(0);
            ans = mAdminDAL.insertNewBusiness("Test2Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs");
            Assert.IsTrue(ans, "Business was not created");
            int createdBusiness = mAdminDAL.findBusinessId(businessOwner.ID);
            int createdAdmin = mAdminDAL.findAdnimId("Test2Aviv23", "password20");
            mAdminDAL.deleteBusiness(createdBusiness);
            mAdminDAL.deleteUser(businessOwner.ID);    
            mAdminDAL.deleteUser(createdAdmin);
        }

        [TestMethod]
        public void TestAddBusinessAndDeal()
        {
            bool ans = mAdminDAL.insertBusinessOwner("Test3Aviv24", "password20", "avivasi@post.bgu.ac.il", "054343243");
            Assert.IsTrue(ans, "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessOwnerBl.getBusinessOwnerByUserName("Test3Aviv24").ElementAt(0);
            ans = mAdminDAL.insertNewBusiness("Test3Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs");
            Assert.IsTrue(ans, "Business was not created");
            int crrdBusinessid = mAdminDAL.findBusinessId(businessOwner.ID);
            Business crrdBusiness = mBusinessOwnerBl.GetBusinessById(crrdBusinessid).ElementAt(0);
            ans = mBusinessOwnerBl.InsertNewDeal("Test31+1", "one plus one", crrdBusiness, 100, new DateTime(1990, 3, 3), 12, 18, 14, 13);
            Assert.IsTrue(ans, "Deal was not created");
            crrdBusiness = mBusinessOwnerBl.GetBusinessById(crrdBusinessid).ElementAt(0);
            mAdminDAL.deleteDeal(crrdBusiness.Deals[0].ID);
            mAdminDAL.deleteBusiness(crrdBusinessid);
            mAdminDAL.deleteUser(businessOwner.ID);
        }

        [TestMethod]
        public void TestAddAndUpdateClient()
        {
            bool ans = mClientBL.InsertNewClient("Test4Aviv3", "password1", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null);
            Assert.IsTrue(ans, "Client was not created");
            int clinetId = mClientBL.GetClientId("Test4Aviv3", "password1");
            mClientBL.UpdateUser("Test4Aviv3", "password2", "blabla@", "05466666", "password1");
            int updateClient = mClientBL.GetClientId("Test4Aviv3", "password2");
            Assert.IsTrue(clinetId == updateClient, "Client was not update");
            mAdminDAL.deleteUser(updateClient);
        }

       [TestMethod]
        public void TestBuyCoupon()
        {
            Assert.IsTrue(mAdminDAL.insertBusinessOwner("Test5Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433"), "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessOwnerBl.getBusinessOwnerByUserName("Test5Aviv24").ElementAt(0);
            Assert.IsTrue(mAdminDAL.insertNewBusiness("Test5Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs"), "Business was not created");
            int crrdBusinessId = mAdminDAL.findBusinessId(businessOwner.ID);
            Business crrdBusiness = mBusinessOwnerBl.GetBusinessById(crrdBusinessId).ElementAt(0);
            Assert.IsTrue(mBusinessOwnerBl.InsertNewDeal("Test51+1", "one plus one", crrdBusiness, 100, new DateTime(1990, 3, 3), 12, 18, 14, 13), "Deal was not created");
            Assert.IsTrue(mClientBL.InsertNewClient("Test5Aviv89", "password4", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null), "Client was not created");
            int createdClientId = mClientBL.GetClientId("Test5Aviv89", "password4");
            int dealId = mClientDAL.SelectDeal(crrdBusinessId);
            Client crrClient = mClientDAL.GetClientById(createdClientId);
            Deal crrDeal = mClientBL.GetDealById(dealId).ElementAt(0);

            int couponId = mClientBL.BuyCoupon(crrDeal.ID, crrClient.ID, 200, 10).ID;
            Assert.IsTrue(couponId != -1, "Coupon was not buying");

            mAdminDAL.deleteCoupon(couponId);
            mAdminDAL.deleteDeal(crrDeal.ID);
            mAdminDAL.deleteBusiness(crrdBusinessId);
            mAdminDAL.deleteUser(createdClientId);
            mAdminDAL.deleteUser(businessOwner.ID);
        
        }

        
        [TestMethod]
        public void TestAddGroup()
        {
            Assert.IsTrue(mClientBL.InsertNewClient("Test6hen2", "password4", "avi@bgu.ac.il", "054631", new DateTime(1990, 12, 12), Gender.Male, null), "Client was not created");
            int createdClient1Id = mClientBL.GetClientId("Test6hen2", "password4");
            Assert.IsTrue(mClientBL.InsertNewClient("Test6Nir2", "password3", "nir@bgu.ac.il", "05455531", new DateTime(1991, 12, 4), Gender.Male, null), "Client was not created");
            int createdClient2Id = mClientBL.GetClientId("Test6Nir2", "password3");
            Assert.IsTrue(mClientDAL.CreateNewGroup(createdClient1Id, "Test6bla"), "group was not created");
            int groupId = mClientBL.GetGroupByName("Test6bla").ID;
            Assert.IsTrue(mClientDAL.AddClientToGroup(createdClient1Id, groupId), "Client dose not added to the group");
            Assert.IsTrue(mClientDAL.AddClientToGroup(createdClient2Id, groupId), "Client dose not added to the group");

            mAdminDAL.deleteGroup(groupId);
            mAdminDAL.deleteUser(createdClient1Id);
            mAdminDAL.deleteUser(createdClient2Id);
        }



        [TestMethod]
        public void TestAddSocialNetwork()
        {
            mClientBL.InsertNewClient("Test7" + clientName, clientPassword, clientMail, clientPhone, clientBornDate, clientGender, null);
            int clientId = mClientBL.GetClientId("Test7"+clientName, clientPassword);
            mClientDAL.AddSocialNetwork(clientId,SocialNetwork.Facebook, "Aviv Asido", "ASDASDJ!@#ASD!@#ASD");
            List<ClientNetwork> networks = mClientDAL.GetClientSocialNetworks(clientId);
            Assert.AreEqual(networks[0].Name, SocialNetwork.Facebook);
            mAdminDAL.deleteUser(clientId);
        }

        [TestMethod]
        public void TestAddSocialFriends()
        {
            mClientBL.InsertNewClient("Test8" + clientName, clientPassword, clientMail, clientPhone, clientBornDate, clientGender, null);
            mClientBL.InsertNewClient("Test8" + client2Name, client2Password, client2Mail, client2Phone, client2BornDate, client2Gender, null);
            int clientId = mClientBL.GetClientId("Test8" + clientName, clientPassword);
            int client2Id = mClientBL.GetClientId("Test8" + client2Name, client2Password);
            mClientDAL.AddSocialNetwork(clientId, SocialNetwork.Facebook, "Aviv Asido", "ASDASDJ!@#ASD!@#ASD");
            mClientDAL.AddSocialNetwork(client2Id, SocialNetwork.Facebook, "Yarden Chen", "ASDASDJ!@#ASD!@#ASD");

            List<ClientNetwork> networks = mClientDAL.GetClientSocialNetworks(clientId);
            List<ClientNetwork> networks2 = mClientDAL.GetClientSocialNetworks(client2Id);

            mClientDAL.AddSocialFriend(networks[0], networks2[0]);

            List<Client> socialFriends = mClientDAL.GetSocialFriends(clientId, SocialNetwork.Facebook);

            Assert.AreEqual(socialFriends[0].ID, client2Id);

            mAdminDAL.deleteUser(clientId);
            mAdminDAL.deleteUser(client2Id);
        }

        [TestMethod]
        public void TestAddContacts()
        {
            mClientBL.InsertNewClient("Test9" + clientName, clientPassword, clientMail, clientPhone, clientBornDate, clientGender, null);
            mClientBL.InsertNewClient("Test9" + client2Name, client2Password, client2Mail, client2Phone, client2BornDate, client2Gender, null);
            int clientId = mClientBL.GetClientId("Test9" + clientName, clientPassword);
            int client2Id = mClientBL.GetClientId("Test9" + client2Name, client2Password);

            mClientDAL.AddContact(clientId, client2Id);

            List<Client> contacts = mClientDAL.GetContacts(clientId);

            Assert.AreEqual(contacts[0].ID, client2Id, "Wrong client");

            mAdminDAL.deleteUser(clientId);
            mAdminDAL.deleteUser(client2Id);
        }
        
        [TestMethod]
        public void TestUpdatBusiness()
        {
            Assert.IsTrue(mAdminDAL.insertBusinessOwner("Test10Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433"), "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessOwnerBl.getBusinessOwnerByUserName("Test10Aviv24").ElementAt(0);
            Assert.IsTrue(mAdminDAL.insertNewBusiness("Test10Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs"), "Business was not created");
            int crrdBusinessId = mAdminDAL.findBusinessId(businessOwner.ID);
            Assert.IsTrue(mBusinessDAL.UpdateBusiness(crrdBusinessId, "Test10Grag", "coffee shop", businessOwner.ID, "bs 13", "bs"), "Business was not update");
            mAdminDAL.deleteBusiness(crrdBusinessId);
            mAdminDAL.deleteUser(businessOwner.ID);
        }

        [TestMethod]
        public void TestCouponSetUsed()
        {
            bool ans = mAdminDAL.insertBusinessOwner("Test11Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433");
            Assert.IsTrue(ans, "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessOwnerBl.getBusinessOwnerByUserName("Test11Aviv24").ElementAt(0);
            ans = mAdminDAL.insertNewBusiness("Test11Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs");
            Assert.IsTrue(ans, "Business was not created");
            int crrdBusinessId = mAdminDAL.findBusinessId(businessOwner.ID);
            Business crrdBusiness = mBusinessOwnerBl.GetBusinessById(crrdBusinessId).ElementAt(0);
            ans = mBusinessOwnerBl.InsertNewDeal("Test111+1", "one plus one", crrdBusiness, 100, new DateTime(1990, 3, 3), 12, 18, 14, 13);
            Assert.IsTrue(ans, "Deal was not created");
            ans = mClientBL.InsertNewClient("Test11Aviv89", "password4", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null);
            Assert.IsTrue(ans, "Client was not created");
            int createdClientId = mClientBL.GetClientId("Test11Aviv89", "password4");
            int dealId = mClientDAL.SelectDeal(crrdBusinessId);
            Client crrClient = mClientDAL.GetClientById(createdClientId);
            Deal crrDeal = mClientBL.GetDealById(dealId).ElementAt(0);
            Coupon coupon = mClientBL.BuyCoupon(crrDeal.ID, crrClient.ID, 200, 10);
            Assert.IsTrue(coupon.ID != -1, "Coupon was not buying");

            mBusinessOwnerBl.updateCouponUsed(coupon);
            bool isUsed = mClientBL.GetClientCouponBySerialKey(coupon.SerialKey).IsUsed;
            Assert.IsTrue(isUsed, "Coupon was not set to used");

            mAdminDAL.deleteCoupon(coupon.ID);
            mAdminDAL.deleteDeal(crrDeal.ID);
            mAdminDAL.deleteBusiness(crrdBusinessId);
            mAdminDAL.deleteUser(createdClientId);
            mAdminDAL.deleteUser(businessOwner.ID);
        }

        [TestMethod]
        public void TestRateCoupon()
        {
            bool ans = mAdminDAL.insertBusinessOwner("Test12Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433");
            Assert.IsTrue(ans, "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessOwnerBl.getBusinessOwnerByUserName("Test12Aviv24").ElementAt(0);
            ans = mAdminDAL.insertNewBusiness("Test12Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs");
            Assert.IsTrue(ans, "Business was not created");
            int crrdBusinessId = mAdminDAL.findBusinessId(businessOwner.ID);
            Business crrdBusiness = mBusinessOwnerBl.GetBusinessById(crrdBusinessId).ElementAt(0);
            ans = mBusinessOwnerBl.InsertNewDeal("Test121+1", "one plus one", crrdBusiness, 100, new DateTime(1990, 3, 3), 12, 18, 14, 13);
            Assert.IsTrue(ans, "Deal was not created");
            ans = mClientBL.InsertNewClient("Test12Aviv89", "password4", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null);
            Assert.IsTrue(ans, "Client was not created");
            int createdClientId = mClientBL.GetClientId("Test12Aviv89", "password4");
            int dealId = mClientDAL.SelectDeal(crrdBusinessId);
            Client crrClient = mClientDAL.GetClientById(createdClientId);
            Deal crrDeal = mClientBL.GetDealById(dealId).ElementAt(0);
            Coupon coupon = mClientBL.BuyCoupon(crrDeal.ID, crrClient.ID, 200, 10);
            Assert.IsTrue(coupon.ID != -1, "Coupon was not buying");
            mBusinessOwnerBl.updateCouponUsed(coupon);
            coupon = mClientBL.GetClientCouponBySerialKey(coupon.SerialKey);
            Assert.IsTrue(coupon.IsUsed, "Coupon was not set to used");

            mClientBL.RateCoupon(coupon, 4);
            coupon = mClientBL.GetClientCouponBySerialKey(coupon.SerialKey);
            Assert.IsTrue(coupon.Rate == 4, "Coupon was not rated");

            mAdminDAL.deleteCoupon(coupon.ID);
            mAdminDAL.deleteDeal(crrDeal.ID);
            mAdminDAL.deleteBusiness(crrdBusinessId);
            mAdminDAL.deleteUser(createdClientId);
            mAdminDAL.deleteUser(businessOwner.ID);
        }

    }
}