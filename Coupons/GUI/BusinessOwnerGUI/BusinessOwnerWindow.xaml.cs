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
using Coupons.GUI.AdminGUI;

namespace Coupons.GUI.BusinessOwnerGUI
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class BusinessOwnerWindow : Window
    {
        private UserBL mUserBL;
        private ClientBL mClientBL;
        private BusinessOwnerBL mBusinessOwnerBl;

        private List<Client> mClients;
        private List<BusinessOwner> mBusniessOwners;
        private List<Business> mBusiness;
        private List<Coupon> mCoupons;
        private List<Deal> mDeals;


        Business mSelectedBusiness;
        Deal mSelectedDeal;
        Coupon mSelectedCoupon;

        public BusinessOwnerWindow(BusinessOwner mSelectedBusinessOwner)
        {

            InitializeComponent();
            mUserBL = new UserBL();
            mClientBL = new ClientBL();
            mBusinessOwnerBl = new BusinessOwnerBL();
            mClients = mUserBL.getAllClients();
            mBusniessOwners = mUserBL.getAllBusinessOwner();
            mBusiness = mClientBL.getAllBusiness();
            setBusinessDataGrid(mBusiness);
            setDealsDataGrid(mDeals);
            setPurchasDataGrid(mCoupons);

        }

        private bool isNumeric(String text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

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

            DataGridTextColumn colAdress = new DataGridTextColumn();
            colAdress.Header = "Address Name";
            colAdress.Binding = new Binding("Address");
            colAdress.Width = 100;

            DataGridTextColumn colCity = new DataGridTextColumn();
            colCity.Header = "City";
            colCity.Binding = new Binding("City");
            colCity.Width = 100;

            dgBusinesses.Columns.Add(colId);
            dgBusinesses.Columns.Add(colName);
            dgBusinesses.Columns.Add(colAdress);
            dgBusinesses.Columns.Add(colCity);

            dgBusinesses.ItemsSource = business;
        }
        private void dgBusinesses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mSelectedBusiness = (Business)dgBusinesses.SelectedItem;
        }


        private void btnUpdateBusiness_Click(object sender, RoutedEventArgs e)
        {
            UpdateBusinessWindow window = new UpdateBusinessWindow(mSelectedBusiness);
            window.Show();
        }
        private void btnAdddDeal_Click(object sender, RoutedEventArgs e)
        {
            CreateDealWindow window = new CreateDealWindow(mSelectedBusiness);
            window.Show();
        }

        public void setDealsDataGrid(List<Deal> deals)
        {

            btnSearchDeal.Background = CustomColors.BLUE;

            dgDeals.Columns.Clear();
            DataGridTextColumn colId = new DataGridTextColumn();
            colId.Header = "ID";
            colId.Binding = new Binding("ID");
            colId.Width = 60;

            DataGridTextColumn colName = new DataGridTextColumn();
            colName.Header = "Name";
            colName.Binding = new Binding("Name");
            colName.Width = 200;

            DataGridTextColumn colPrice = new DataGridTextColumn();
            colPrice.Header = "Price";
            colPrice.Binding = new Binding("Price");
            colPrice.Width = 200;

            DataGridTextColumn colRate = new DataGridTextColumn();
            colRate.Header = "Rate";
            colRate.Binding = new Binding("Rate");
            colRate.Width = 200;

            DataGridTextColumn colExpDate = new DataGridTextColumn();
            colExpDate.Header = "ExperationDate ";
            colExpDate.Binding = new Binding("ExperationDate");
            colExpDate.Width = 60;

            dgDeals.Columns.Add(colId);
            dgDeals.Columns.Add(colName);
            dgDeals.Columns.Add(colPrice);
            dgDeals.Columns.Add(colRate);
            dgDeals.Columns.Add(colExpDate);

            dgDeals.ItemsSource = deals;
        }

        private void btnSearchDeal_Click(object sender, RoutedEventArgs e)
        {

            String dealId = tbId.Text;



            if (tbId.Text.Length > 0 && isNumeric(tbId.Text) && (mClientBL.getDealById(Convert.ToInt32(dealId)) != null))
            {
                setDealsDataGrid(mClientBL.getDealById(Convert.ToInt32(dealId)));
            }
            else
            {
                MessageBoxResult results = MessageBox.Show("You enterd an invalid deal Id. Please try again",
                    "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        public void setPurchasDataGrid(List<Coupon> coupons)
        {

            dgPurchases.Columns.Clear();
            DataGridTextColumn colId = new DataGridTextColumn();
            colId.Header = "ID";
            colId.Binding = new Binding("ID");
            colId.Width = 60;

            DataGridTextColumn colBoughtPrice = new DataGridTextColumn();
            colBoughtPrice.Header = "BoughtPrice";
            colBoughtPrice.Binding = new Binding("BoughtPrice");
            colBoughtPrice.Width = 100;

            DataGridTextColumn colIsUsed = new DataGridTextColumn();
            colIsUsed.Header = "IsUsed";
            colIsUsed.Binding = new Binding("IsUsed");
            colIsUsed.Width = 100;


            dgPurchases.Columns.Add(colId);
            dgPurchases.Columns.Add(colBoughtPrice);
            dgPurchases.Columns.Add(colIsUsed);


            dgPurchases.ItemsSource = coupons;
        }

        private void dgPurchases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mSelectedCoupon = (Coupon)dgPurchases.SelectedItem;
        }





    }
}
