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
using Coupons.BL;
using Coupons.Models;

namespace Coupons.GUI.BusinessOwnerGUI
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class BusinessOwnerWindow : Window
    {
        private UserBL mUserBL;
        private BusinessOwnerBL mBusinessOwnerBL;


        private List<BusinessOwner> mBusniessOwners;
        private List<Business> mBusiness;
        private List<Coupon> mTableCoupons;
        private List<Deal> mTableDeals;


        public BusinessOwnerWindow(BusinessOwner owner)
        {
            InitializeComponent();
            mUserBL = new UserBL();
            mBusinessOwnerBL = new BusinessOwnerBL();
            mBusniessOwners = mUserBL.getAllBusinessOwner();
            //HOW? mBusiness = mBusinessOwnerBL.getBusinessOwnerById();

            setBusinessDataGrid(mBusiness);
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

            dgBusinesses.Columns.Add(colId);
            dgBusinesses.Columns.Add(colName);

            dgBusinesses.ItemsSource = business;
        }

        private void btnAddBusiness_Click(object sender, RoutedEventArgs e)
        {
            // mBusinessOwnerBL.UpdateBusiness()
        }
    }
}
