using Coupons.BL;
using Coupons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Coupons.Util;
using System.Text.RegularExpressions;

namespace Coupons.GUI.AdminGUI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        private UserBL mUserBL;
        private ClientBL mClientBL;
        private BusinessOwnerBL mBusinessOwnerBl;
        private AdminBL mAdminBL;

        private List<Client> mClients;
        private List<BusinessOwner> mBusniessOwners;
        private List<Business> mBusiness;
        private List<Deal> mDeal;

        private bool mIsClient;
        private Admin mAdmin;
        BusinessOwner mSelectedBusinessOwner;
        Business mSelectedBusiness;
        Deal mSelectedDeal;

        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            mUserBL = new UserBL();
            mClientBL = new ClientBL();
            mBusinessOwnerBl = new BusinessOwnerBL();
            mAdminBL = new AdminBL();
            mClients = mUserBL.getAllClients();
            mBusniessOwners = mUserBL.getAllBusinessOwner();
            mBusiness = mClientBL.getAllBusiness();
            mDeal = mClientBL.getAllDeal();
            mAdmin = admin;

            setClientDataGrid(mClients);
            setBusinessDataGrid(mBusiness);
            setDealsDataGrid(mDeal);

            lblUsername.Content = mAdmin.Username;
        }


        /***************** SETCTION USERS *****************/
        public void setClientDataGrid(List<Client> clients)
        {
            mIsClient = true;
            btnOwner.Background = CustomColors.BLUE_LIGHT;
            btnClient.Background = CustomColors.BLUE;

            dgUsers.Columns.Clear();
            DataGridTextColumn colId = new DataGridTextColumn();
            colId.Header = "ID";
            colId.Binding = new Binding("ID");
            colId.Width = 60;

            DataGridTextColumn colName = new DataGridTextColumn();
            colName.Header = "Username";
            colName.Binding = new Binding("Username");
            colName.Width = 200;

            dgUsers.Columns.Add(colId);
            dgUsers.Columns.Add(colName);

            dgUsers.ItemsSource = clients;
        }
        public void setBusniessOwnerDataGrid(List<BusinessOwner> owners)
        {
            mIsClient = false;
            btnOwner.Background = CustomColors.BLUE;
            btnClient.Background = CustomColors.BLUE_LIGHT;

            dgUsers.Columns.Clear();
            DataGridTextColumn colId = new DataGridTextColumn();
            colId.Header = "ID";
            colId.Binding = new Binding("ID");
            colId.Width = 60;

            DataGridTextColumn colName = new DataGridTextColumn();
            colName.Header = "Username";
            colName.Binding = new Binding("Username");
            colName.Width = 200;

            dgUsers.Columns.Add(colId);
            dgUsers.Columns.Add(colName);

            dgUsers.ItemsSource = owners;
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            setClientDataGrid(mClients);
        }

        private void btnOwner_Click(object sender, RoutedEventArgs e)
        {
            setBusniessOwnerDataGrid(mBusniessOwners);
        }

        private void btnAddClientOrOwner_Click(object sender, RoutedEventArgs e)
        {
            CreateUserWindow createClientWindow = new CreateUserWindow(null, mIsClient);
            createClientWindow.Show();
        }

        private void btnSearchUser_Click(object sender, RoutedEventArgs e)
        {
            if (mIsClient)
            {
                if (tbUserId.Text.Length > 0 && isNumeric(tbUserId.Text))
                { 
                    Client client = mClientBL.getClientById(Convert.ToInt32(tbUserId.Text));
                    if (client != null){
                        List<Client> toShow = new List<Client>();
                        toShow.Add(client);
                        setClientDataGrid(toShow);
                    }
                    else
                    {
                        setClientDataGrid(null);
                    }
                } else 

                if (tbName.Text.Length > 0)
                {
                    Client client = mClientBL.getClientByUsername(tbName.Text);
                    if (client != null)
                    {
                        List<Client> toShow = new List<Client>();
                        toShow.Add(client);
                        setClientDataGrid(toShow);
                    }
                    else
                    {
                        setClientDataGrid(null);
                    }
                }
            }
            else
            {
                if (tbUserId.Text.Length > 0 && isNumeric(tbUserId.Text))
                {
                    BusinessOwner businessOwner = mBusinessOwnerBl.getBusinessOwnerById(Convert.ToInt32(tbUserId.Text));
                    if (businessOwner != null)
                    {
                        List<BusinessOwner> toShow = new List<BusinessOwner>();
                        toShow.Add(businessOwner);
                        setBusniessOwnerDataGrid(toShow);
                    }
                    else
                    {
                        setBusniessOwnerDataGrid(null);
                    }
                }
                else

                    if (tbName.Text.Length > 0)
                    {
                        BusinessOwner businessOwner = mBusinessOwnerBl.getBusinessOwnerByUserName(tbName.Text);
                        if (businessOwner != null)
                        {
                            List<BusinessOwner> toShow = new List<BusinessOwner>();
                            toShow.Add(businessOwner);
                            setBusniessOwnerDataGrid(toShow);
                        }
                        else
                        {
                            setBusniessOwnerDataGrid(null);
                        }
                    }
            }
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mIsClient)
            {

            }
            else
            {
                mSelectedBusinessOwner = (BusinessOwner)dgUsers.SelectedItem;
            }
        }

        private bool isNumeric(String text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }




        /***************** SETCTION BUSINESS *****************/
        public void setBusinessDataGrid(List<Business> business)
        {

            dgBusinesses.Columns.Clear();
            DataGridTextColumn colId = new DataGridTextColumn();
            colId.Header = "ID";
            colId.Binding = new Binding("ID");
            colId.Width = 60;

            DataGridTextColumn colName = new DataGridTextColumn();
            colName.Header = "Name";
            colName.Binding = new Binding("Name");
            colName.Width = 200;

            dgBusinesses.Columns.Add(colId);
            dgBusinesses.Columns.Add(colName);

            dgBusinesses.ItemsSource = business;
        }

        private void btnAddBusiness_Click(object sender, RoutedEventArgs e)
        {
            if (mSelectedBusinessOwner != null && !mIsClient)
            {
                CreateBusinessWindow window = new CreateBusinessWindow(mSelectedBusinessOwner);
                window.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a Business Owner",
                  "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void dgBusinesses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mSelectedBusiness = (Business)dgBusinesses.SelectedItem;
        }


        private void btnRefreshBusiness_Click(object sender, RoutedEventArgs e)
        {
            setBusinessDataGrid(mClientBL.getAllBusiness());
        }

        private void btnSearchBusiness_Click(object sender, RoutedEventArgs e)
        {
            String name = tbBusinessName.Text;
            String ownerId = tbOwnerId.Text;
            String businessId = tbBusinessId.Text;

            if (name.Length > 0)
            {
                setBusinessDataGrid(mBusinessOwnerBl.getBusinessesByName(name));
            }
            else if (ownerId.Length > 0 && isNumeric(ownerId))
            {
                setBusinessDataGrid(mBusinessOwnerBl.getBusinessesByOwnerId(Convert.ToInt32(ownerId)));
            }
            else if (businessId.Length > 0 && isNumeric(businessId))
            {
                setBusinessDataGrid(mBusinessOwnerBl.getBusinessById(Convert.ToInt32(businessId)));
            }
        }
        private void btnDeleteBusiness_Click(object sender, RoutedEventArgs e)
        {
            if (mSelectedBusiness != null)
            {
                mSelectedBusiness = (Business)dgBusinesses.SelectedItem;
                bool isOk = mAdminBL.deleteBusiness(mSelectedBusiness.ID);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a Business that you want to delete!",
                  "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
     
        
        
        /***************** SETCTION DEALS *****************/
        public void setDealsDataGrid(List<Deal> deal)
        {
            dgDeals.Columns.Clear();
            DataGridTextColumn colId = new DataGridTextColumn();
            colId.Header = "ID";
            colId.Binding = new Binding("ID");
            colId.Width = 60;

            DataGridTextColumn colBusinessId = new DataGridTextColumn();
            colBusinessId.Header = "Business Id";
            colBusinessId.Binding = new Binding("Business");
            colBusinessId.Width = 80;

            DataGridTextColumn colName = new DataGridTextColumn();
            colName.Header = "Name";
            colName.Binding = new Binding("Name");
            colName.Width = 200;

            dgDeals.Columns.Add(colId);
            dgDeals.Columns.Add(colBusinessId);
            dgDeals.Columns.Add(colName);

            dgDeals.ItemsSource = deal;
        }

        private void btnNotApprove_Click(object sender, RoutedEventArgs e)
        {
            setDealsDataGrid(mAdminBL.getDealsNotApproval());
        }

        private void btnAllDeal_Click(object sender, RoutedEventArgs e)
        {
            setDealsDataGrid(mClientBL.getAllDeal());
        }

        private void btnAddDeal_Click(object sender, RoutedEventArgs e)
        {
            if (mSelectedBusiness != null)
            {
                mSelectedBusiness = (Business)dgBusinesses.SelectedItem;
                CreateDealWindow window = new CreateDealWindow(mSelectedBusiness);
                window.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a Business",
                  "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (mSelectedDeal != null)
            {
                mSelectedDeal = (Deal)dgDeals.SelectedItem;
                bool isOk = mAdminBL.deleteDeal(mSelectedDeal);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a Deal that you want to delete!",
                  "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (mSelectedDeal != null)
            {
                mSelectedDeal = (Deal)dgDeals.SelectedItem;
                UpdateDealWindow window = new UpdateDealWindow(mSelectedDeal);
                window.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a Deal that you want to update!",
               "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (mSelectedDeal != null)
            {
                bool isOk = mAdminBL.ApproveDeal(mSelectedDeal);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a Deal that you want to approve!",
                 "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnSearchDeal_Click(object sender, RoutedEventArgs e)
        {
            String dealId = tbDeal_Id.Text;


            if (dealId.Length > 0 && isNumeric(dealId))
            {
                setDealsDataGrid(mClientBL.getDealById(Convert.ToInt32(dealId)));

            }
        }

        private void dgDeals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mSelectedDeal = (Deal)dgDeals.SelectedItem;
        }


    }
}
