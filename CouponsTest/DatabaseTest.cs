using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coupons.DAL;
using Coupons;
using Coupons.Enums;
using Coupons.Models;

namespace CouponsTest
{
    [TestClass]
    public class DatabaseTest
    {

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
            Assert.IsTrue(mClientDAL.InsertNewClient("Aviv2", "password1", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null), "Client was not created");
            int createdClient = mClientDAL.GetClientId("Aviv2", "password1");
            mAdminDAL.deleteUser(createdClient);
        }

        [TestMethod]
        public void TestAddAdminAndBusiness()
        {
            Assert.IsTrue(mAdminDAL.insertNewAdmin("Aviv23", "password20", "avivasi@post.bgu.ac.il", "05433"), "Admin was not created");
            Assert.IsTrue(mAdminDAL.insertBusinessOwner("Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433"), "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessDAL.GetBusinessOwnerById("Aviv24", "password20");
            Assert.IsTrue(mAdminDAL.insertNewBusiness("Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs"), "Business was not created");
            int createdBusiness = mAdminDAL.findBusinessId(businessOwner.ID);
            int createdAdmin = mAdminDAL.findAdnimId("Aviv23", "password20");
            mAdminDAL.deleteBusiness(createdBusiness);
            mAdminDAL.deleteUser(businessOwner.ID);    
            mAdminDAL.deleteUser(createdAdmin);      
        }

        [TestMethod]
        public void TestAddBusinessAndDeal()
        {
            Assert.IsTrue(mAdminDAL.insertBusinessOwner("Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433"), "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessDAL.GetBusinessOwnerById("Aviv24", "password20");
            Assert.IsTrue(mAdminDAL.insertNewBusiness("Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs"), "Business was not created");
            int crrdBusinessid = mAdminDAL.findBusinessId(businessOwner.ID);
            Business crrdBusiness = mAdminDAL.selectBusinessById(crrdBusinessid);
            Assert.IsTrue(mBusinessDAL.InsertNewDeal("1+1", "one plus one", crrdBusiness, 100, new DateTime(1990, 3, 3), DateTime.Now, DateTime.Now), "Deal was not created");
            mAdminDAL.deleteDeal(crrdBusiness.Deals[0].ID);
            mAdminDAL.deleteBusiness(crrdBusinessid);
            mAdminDAL.deleteUser(businessOwner.ID);
        }

        [TestMethod]
        public void TestAddAndUpdateClient()
        {
            Assert.IsTrue(mClientDAL.InsertNewClient("Aviv3", "password1", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null), "Client was not created");
            int clinetId = mClientDAL.UpdateUser("Aviv3", "password2", "blabla@", "05466666", "password1");
            int updateClient = mClientDAL.GetClientId("Aviv3", "password2");
            Assert.IsTrue(clinetId == updateClient, "Client was not update");
            mAdminDAL.deleteUser(updateClient);
        }

       [TestMethod]
        public void TestBuyCoupon()
        {
            Assert.IsTrue(mAdminDAL.insertBusinessOwner("Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433"), "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessDAL.GetBusinessOwnerById("Aviv24", "password20");
            Assert.IsTrue(mAdminDAL.insertNewBusiness("Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs"), "Business was not created");
            int crrdBusinessId = mAdminDAL.findBusinessId(businessOwner.ID);
            Business crrdBusiness = mAdminDAL.selectBusinessById(crrdBusinessId);
            Assert.IsTrue(mBusinessDAL.InsertNewDeal("1+1", "one plus one", crrdBusiness, 100, new DateTime(1990, 3, 3), DateTime.Now, DateTime.Now), "Deal was not created");
            Assert.IsTrue(mClientDAL.InsertNewClient("Aviv89", "password4", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null), "Client was not created");
            int createdClientId = mClientDAL.GetClientId("Aviv89", "password4");
            int dealId = mClientDAL.SelectDeal(crrdBusinessId);
            Client crrClient = mClientDAL.GetClientById(createdClientId);
            Deal crrDeal = mClientDAL.GetDealById(dealId);

            int couponId = mClientDAL.InsertCoupon(crrDeal, crrClient);
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
            Assert.IsTrue(mClientDAL.InsertNewClient("hen2", "password4", "avi@bgu.ac.il", "054631", new DateTime(1990, 12, 12), Gender.Male, null), "Client was not created");
            int createdClient1Id = mClientDAL.GetClientId("hen2", "password4");
            Assert.IsTrue(mClientDAL.InsertNewClient("Nir2", "password3", "nir@bgu.ac.il", "05455531", new DateTime(1991, 12, 4), Gender.Male, null), "Client was not created");
            int createdClient2Id = mClientDAL.GetClientId("Nir2", "password3");
            Assert.IsTrue(mClientDAL.CreateNewGroup(createdClient1Id, "bla"), "group was not created");
            int groupId = mClientDAL.GetGroupByName("bla");
            Assert.IsTrue(mClientDAL.AddClientToGroup(createdClient1Id, groupId), "Client dose not added to the group");
            Assert.IsTrue(mClientDAL.AddClientToGroup(createdClient2Id, groupId), "Client dose not added to the group");

            mAdminDAL.deleteGroup(groupId);
            mAdminDAL.deleteUser(createdClient1Id);
            mAdminDAL.deleteUser(createdClient2Id);
        }



        [TestMethod]
        public void TestAddSocialNetwork()
        {
            mClientDAL.InsertNewClient(clientName, clientPassword, clientMail, clientPhone, clientBornDate, clientGender, null);
            int clientId = mClientDAL.GetClientId(clientName,clientPassword);
            mClientDAL.AddSocialNetwork(clientId,SocialNetwork.Facebook, "Aviv Asido", "ASDASDJ!@#ASD!@#ASD");
            List<ClientNetwork> networks = mClientDAL.GetClientSocialNetworks(clientId);
            Assert.AreEqual(networks[0].Name, SocialNetwork.Facebook);
            mAdminDAL.deleteUser(clientId);
        }

        [TestMethod]
        public void TestAddSocialFriends()
        {
            mClientDAL.InsertNewClient(clientName, clientPassword, clientMail, clientPhone, clientBornDate, clientGender, null);
            mClientDAL.InsertNewClient(client2Name, client2Password, client2Mail, client2Phone, client2BornDate, client2Gender, null);
            int clientId = mClientDAL.GetClientId(clientName, clientPassword);
            int client2Id = mClientDAL.GetClientId(client2Name, client2Password);
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
            mClientDAL.InsertNewClient(clientName, clientPassword, clientMail, clientPhone, clientBornDate, clientGender, null);
            mClientDAL.InsertNewClient(client2Name, client2Password, client2Mail, client2Phone, client2BornDate, client2Gender, null);
            int clientId = mClientDAL.GetClientId(clientName, clientPassword);
            int client2Id = mClientDAL.GetClientId(client2Name, client2Password);

            mClientDAL.AddContact(clientId, client2Id);

            List<Client> contacts = mClientDAL.GetContacts(clientId);

            Assert.AreEqual(contacts[0].ID, client2Id, "Wrong client");

            mAdminDAL.deleteUser(clientId);
            mAdminDAL.deleteUser(client2Id);
        }

        [TestMethod]
        public void TestUpdatBusiness()
        {
            Assert.IsTrue(mAdminDAL.insertBusinessOwner("Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433"), "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessDAL.GetBusinessOwnerById("Aviv24", "password20");
            Assert.IsTrue(mAdminDAL.insertNewBusiness("Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs"), "Business was not created");
            int crrdBusinessId = mAdminDAL.findBusinessId(businessOwner.ID);
            Assert.IsTrue(mBusinessDAL.UpdateBusiness(crrdBusinessId, "Grag", "coffee shop", businessOwner.ID, "bs 13", "bs"), "Business was not update");
            mAdminDAL.deleteBusiness(crrdBusinessId);
            mAdminDAL.deleteUser(businessOwner.ID);
        }
        
    }
}