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

        private BusinessOwnerBL mBusinessOwnerBl;

        private List<Business> mBusiness;
        private List<Deal> mDeals;
        private List<Coupon> mCoupons;

        Business mSelectedBusiness;
        Deal mSelectedDeal;
        Coupon mSelectedCoupon;
        BusinessOwner mBusinessOwner;

        public BusinessOwnerWindow(BusinessOwner BusinessOwner)
        {

            InitializeComponent();

            mBusinessOwner = BusinessOwner;
            mUserBL = new UserBL();
            mBusinessOwnerBl = new BusinessOwnerBL();
            mBusiness = new List<Business>();
            mDeals = new List<Deal>();
            mCoupons = new List<Coupon>();
            loadAllOwnerDeals();
            setDealsDataGrid(mDeals);
            setBusinessDataGrid(mBusiness);
            setPurchasDataGrid(mCoupons);

            lblUsername.Content = mBusinessOwner.Username;

        }

        private void loadAllOwnerDeals()
        {
            List<Business> businesses = mBusinessOwnerBl.getBusinessesByOwnerId(mBusinessOwner.ID);
            mBusiness.Clear();
            mDeals.Clear();
            mCoupons.Clear();

            foreach (Business business in businesses)
            {
                mBusiness.Add(business);
                List<Deal> businessDeals = mBusinessOwnerBl.getAllDealsByBussinesId(business.ID);
                foreach (Deal deal in businessDeals)
                {
                    mDeals.Add(deal);
                    List<Coupon> businessDealsCoupon = mBusinessOwnerBl.getAllCouponByDealId(deal.ID);
                    foreach (Coupon coupon in businessDealsCoupon)
                    {
                        mCoupons.Add(coupon);
                    }
                }
            }
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
            if (mSelectedBusiness != null)
            {
                UpdateBusinessWindow window = new UpdateBusinessWindow(mSelectedBusiness);
                window.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a Business that you want to update!",
               "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
            }



        }
        private void btnAdddDeal_Click(object sender, RoutedEventArgs e)
        {
            if (mSelectedBusiness != null)
            {
                CreateDealWindow window = new CreateDealWindow(mSelectedBusiness);
                window.Show();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please select a Business",
                  "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            String businessId = tbBusinessId.Text;
            List<Deal> deals = new List<Deal>();
            Deal deal = null;

            if (dealId.Length > 0 && isNumeric(dealId) && mBusinessOwnerBl.getDealById(Convert.ToInt32(dealId))!= null)
            {
                deal = mBusinessOwnerBl.getDealById(Convert.ToInt32(dealId));
                deals.Add(deal);
                setDealsDataGrid(deals);
            }
            else if (businessId.Length > 0 && isNumeric(businessId) && mBusinessOwnerBl.getAllDealsByBussinesId(Convert.ToInt32(businessId)) != null)
            {
                deals = mBusinessOwnerBl.getAllDealsByBussinesId(Convert.ToInt32(businessId));  
                setDealsDataGrid(deals);
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

        private void dgDeals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mSelectedDeal = (Deal)dgDeals.SelectedItem;
        }

        private void btnRefreshBusiness_Click(object sender, RoutedEventArgs e)
        {
            setBusinessDataGrid(mBusiness);
        }

        private void btnRefreshDeals_Click(object sender, RoutedEventArgs e)
        {
            loadAllOwnerDeals();
            setDealsDataGrid(mDeals);
        }

        private void btnRefreshPurchases_Click(object sender, RoutedEventArgs e)
        {
            setPurchasDataGrid(mCoupons);
        }






    }
}
