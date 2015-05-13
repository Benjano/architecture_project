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
using Coupons.Util;


namespace Coupons.GUI.BusinessOwnerGUI
{
    /// <summary>
    /// Interaction logic for UpdateBusinessWindow.xaml
    /// </summary>
    public partial class UpdateBusinessWindow : Window
    {
        private UserBL mUserBL;
        private ClientBL mClientBL;
        private BusinessOwnerBL mBusinessOwnerBL;

        private List<Client> mClients;

        private List<Business> mBusiness;
        private List<Coupon> mTableCoupons;
        private List<Deal> mTableDeals;
        private List<User> mTableUsers;

        private bool mIsOwner;
        Business mSelectedBusiness;



        public UpdateBusinessWindow(Business buisness)
        {
            InitializeComponent();
            mUserBL = new UserBL();
            mClientBL = new ClientBL();
            mBusinessOwnerBL = new BusinessOwnerBL();
            mClients = mUserBL.getAllClients();

            mBusiness = mClientBL.getAllBusiness();
            mSelectedBusiness = buisness;

        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string name = tbBuisName.Text;
            string description = tbDescription.Text;
            string adress = tbAdress.Text;
            string city = tbCity.Text;
            mBusinessOwnerBL.UpdateBusiness(mSelectedBusiness.ID, name, description, mSelectedBusiness.Owner, adress, city);
            Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }

}
