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
            Assert.IsTrue(mClientDAL.insertNewClient("Aviv2", "password1", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null), "Client was not created");
            int createdClient = mClientDAL.findClientId("Aviv2", "password1");
            mAdminDAL.deleteUser(createdClient);
        }

        [TestMethod]
        public void TestAddAdminAndBusiness()
        {
            Assert.IsTrue(mAdminDAL.insertNewAdmin("Aviv23", "password20", "avivasi@post.bgu.ac.il", "05433"), "Admin was not created");
            Assert.IsTrue(mAdminDAL.insertBusinessOwner("Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433"), "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessDAL.getBusinessOwnerById("Aviv24", "password20");
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
            BusinessOwner businessOwner = mBusinessDAL.getBusinessOwnerById("Aviv24", "password20");
            Assert.IsTrue(mAdminDAL.insertNewBusiness("Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs"), "Business was not created");
            int crrdBusinessid = mAdminDAL.findBusinessId(businessOwner.ID);
            Business crrdBusiness = mAdminDAL.selectBusinessById(crrdBusinessid);
            Assert.IsTrue(mBusinessDAL.insertNewDeal("1+1", "one plus one", crrdBusiness, 100, new DateTime(1990, 3, 3), DateTime.Now, DateTime.Now), "Deal was not created");
            mAdminDAL.deleteDeal(crrdBusiness.Deals[0].ID);
            mAdminDAL.deleteBusiness(crrdBusinessid);
            mAdminDAL.deleteUser(businessOwner.ID);
        }

        [TestMethod]
        public void TestAddAndUpdateClient()
        {
            Assert.IsTrue(mClientDAL.insertNewClient("Aviv3", "password1", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null), "Client was not created");
            int clinetId = mClientDAL.updateClient("Aviv3", "password2", "blabla@", "05466666", "password1");
            int updateClient = mClientDAL.findClientId("Aviv3", "password2");
            Assert.IsTrue(clinetId == updateClient, "Client was not update");
            mAdminDAL.deleteUser(updateClient);
        }

       [TestMethod]
        public void TestBuyCoupon()
        {
            Assert.IsTrue(mAdminDAL.insertBusinessOwner("Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433"), "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessDAL.getBusinessOwnerById("Aviv24", "password20");
            Assert.IsTrue(mAdminDAL.insertNewBusiness("Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs"), "Business was not created");
            int crrdBusinessId = mAdminDAL.findBusinessId(businessOwner.ID);
            Business crrdBusiness = mAdminDAL.selectBusinessById(crrdBusinessId);
            Assert.IsTrue(mBusinessDAL.insertNewDeal("1+1", "one plus one", crrdBusiness, 100, new DateTime(1990, 3, 3), DateTime.Now, DateTime.Now), "Deal was not created");
            Assert.IsTrue(mClientDAL.insertNewClient("Aviv89", "password4", "avivasi@post.bgu.ac.il", "0546310736", new DateTime(1990, 4, 4), Gender.Female, null), "Client was not created");
            int createdClientId = mClientDAL.findClientId("Aviv89", "password4");
            int dealId = mClientDAL.selectDeal(crrdBusinessId);
            Client crrClient = mClientDAL.getClientById(createdClientId);
            Deal crrDeal = mClientDAL.getDealById(dealId);

            int couponId = mClientDAL.buyCoupon(crrDeal, crrClient);
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
            Assert.IsTrue(mClientDAL.insertNewClient("hen2", "password4", "avi@bgu.ac.il", "054631", new DateTime(1990, 12, 12), Gender.Male, null), "Client was not created");
            int createdClient1Id = mClientDAL.findClientId("hen2", "password4");
            Assert.IsTrue(mClientDAL.insertNewClient("Nir2", "password3", "nir@bgu.ac.il", "05455531", new DateTime(1991, 12, 4), Gender.Male, null), "Client was not created");
            int createdClient2Id = mClientDAL.findClientId("Nir2", "password3");
            Assert.IsTrue(mClientDAL.createNewGroup(createdClient1Id, "bla"), "group was not created");
            int groupId = mClientDAL.findGroupId("bla");
            Assert.IsTrue(mClientDAL.addClientToGroup(createdClient1Id, groupId), "Client dose not added to the group");
            Assert.IsTrue(mClientDAL.addClientToGroup(createdClient2Id, groupId), "Client dose not added to the group");

            mAdminDAL.deleteGroup(groupId);
            mAdminDAL.deleteUser(createdClient1Id);
            mAdminDAL.deleteUser(createdClient2Id);
        }



        [TestMethod]
        public void TestAddSocialNetwork()
        {
            mClientDAL.insertNewClient(clientName, clientPassword, clientMail, clientPhone, clientBornDate, clientGender, null);
            int clientId = mClientDAL.findClientId(clientName,clientPassword);
            mClientDAL.addSocialNetwork(clientId,SocialNetwork.Facebook, "Aviv Asido", "ASDASDJ!@#ASD!@#ASD");
            List<ClientNetwork> networks = mClientDAL.getClientSocialNetworks(clientId);
            Assert.AreEqual(networks[0].Name, SocialNetwork.Facebook);
            mAdminDAL.deleteUser(clientId);
        }

        [TestMethod]
        public void TestAddSocialFriends()
        {
            mClientDAL.insertNewClient(clientName, clientPassword, clientMail, clientPhone, clientBornDate, clientGender, null);
            mClientDAL.insertNewClient(client2Name, client2Password, client2Mail, client2Phone, client2BornDate, client2Gender, null);
            int clientId = mClientDAL.findClientId(clientName, clientPassword);
            int client2Id = mClientDAL.findClientId(client2Name, client2Password);
            mClientDAL.addSocialNetwork(clientId, SocialNetwork.Facebook, "Aviv Asido", "ASDASDJ!@#ASD!@#ASD");
            mClientDAL.addSocialNetwork(client2Id, SocialNetwork.Facebook, "Yarden Chen", "ASDASDJ!@#ASD!@#ASD");

            List<ClientNetwork> networks = mClientDAL.getClientSocialNetworks(clientId);
            List<ClientNetwork> networks2 = mClientDAL.getClientSocialNetworks(client2Id);

            mClientDAL.addSocialFriend(networks[0], networks2[0]);

            List<Client> socialFriends = mClientDAL.getSocialFriends(clientId, SocialNetwork.Facebook);

            Assert.AreEqual(socialFriends[0].ID, client2Id);

            mAdminDAL.deleteUser(clientId);
            mAdminDAL.deleteUser(client2Id);
        }

        [TestMethod]
        public void TestAddContacts()
        {
            mClientDAL.insertNewClient(clientName, clientPassword, clientMail, clientPhone, clientBornDate, clientGender, null);
            mClientDAL.insertNewClient(client2Name, client2Password, client2Mail, client2Phone, client2BornDate, client2Gender, null);
            int clientId = mClientDAL.findClientId(clientName, clientPassword);
            int client2Id = mClientDAL.findClientId(client2Name, client2Password);

            mClientDAL.addContact(clientId, client2Id);

            List<Client> contacts = mClientDAL.getContacts(clientId);

            Assert.AreEqual(contacts[0].ID, client2Id, "Wrong client");

            mAdminDAL.deleteUser(clientId);
            mAdminDAL.deleteUser(client2Id);
        }

        [TestMethod]
        public void TestUpdatBusiness()
        {
            Assert.IsTrue(mAdminDAL.insertBusinessOwner("Aviv24", "password20", "avivasi@post.bgu.ac.il", "05433"), "BusinessOwner was not created");
            BusinessOwner businessOwner = mBusinessDAL.getBusinessOwnerById("Aviv24", "password20");
            Assert.IsTrue(mAdminDAL.insertNewBusiness("Aroma", "coffee shop", businessOwner.ID, "bs 11", "bs"), "Business was not created");
            int crrdBusinessId = mAdminDAL.findBusinessId(businessOwner.ID);
            Assert.IsTrue(mBusinessDAL.UpdateBusiness(crrdBusinessId, "Grag", "coffee shop", businessOwner.ID, "bs 13", "bs"), "Business was not update");
            mAdminDAL.deleteBusiness(crrdBusinessId);
            mAdminDAL.deleteUser(businessOwner.ID);
        }
        
    }
}