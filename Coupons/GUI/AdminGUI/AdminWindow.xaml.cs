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

        private List<Client> mClients;
        private List<BusinessOwner> mBusniessOwners;
        private List<Business> mBusiness;

        private bool mIsClient;

        BusinessOwner mSelectedBusinessOwner;

        public AdminWindow(Admin admin)
        {
            InitializeComponent();
            mUserBL = new UserBL();
            mClientBL = new ClientBL();
            mBusinessOwnerBl = new BusinessOwnerBL();
            mClients = mUserBL.getAllClients();
            mBusniessOwners = mUserBL.getAllBusinessOwner();
            mBusiness = mClientBL.getAllBusiness();
            

            setClientDataGrid(mClients);
            setBusinessDataGrid(mBusiness);
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
                }
            }
        }

        private bool isNumeric(String text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        /***************** SETCTION DEALS *****************/


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
        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mIsClient)
            {

            }
            else
            {
                mSelectedBusinessOwner = (BusinessOwner) dgUsers.SelectedItem;
            }
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
            }
            else if (ownerId.Length > 0 && isNumeric(ownerId))
            {
                setBusinessDataGrid(mBusinessOwnerBl.getBusinessesByOwnerId(Convert.ToInt32(ownerId)));
            }
            else if (businessId.Length > 0)
            {

            }
        }

    }
}
