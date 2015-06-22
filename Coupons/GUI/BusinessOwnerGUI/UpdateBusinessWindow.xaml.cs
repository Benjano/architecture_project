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
        private BusinessOwnerController mBusinessOwnerBL;
        Business mSelectedBusiness;


        public UpdateBusinessWindow(Business buisness)
        {
            InitializeComponent();
            mSelectedBusiness = buisness;
            mBusinessOwnerBL = new BusinessOwnerController();
            tbBusinessName.Text = mSelectedBusiness.Name;
            tbDescription.Text = mSelectedBusiness.Description;
            tbAddress.Text = mSelectedBusiness.Address;
            tbCity.Text = mSelectedBusiness.City;
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string name = tbBusinessName.Text;
            string description = tbDescription.Text;
            string address = tbAddress.Text;
            string city = tbCity.Text;
            mBusinessOwnerBL.UpdateBusiness(mSelectedBusiness.ID, name, description, mSelectedBusiness.Owner, address, city);
            Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }

}
