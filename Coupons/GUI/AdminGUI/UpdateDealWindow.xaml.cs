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

namespace Coupons.GUI.AdminGUI
{
    /// <summary>
    /// Interaction logic for UpdateDealWindow.xaml
    /// </summary>
    public partial class UpdateDealWindow : Window
    {

        Deal mDeal;
        AdminBL mAdminBL;

        public UpdateDealWindow(Deal deal)
        {
            InitializeComponent();
            mDeal = deal;
            mAdminBL = new AdminBL();
            tbName.Text = mDeal.Name;
            tbDetails.Text = mDeal.Details;
            tbOriginalPrice.Text = Convert.ToString(mDeal.Price);
            dpExperationDate.SelectedDate= mDeal.ExperationDate;
            tbStart_hour.Text= mDeal.StartHour;
            tbEnd_Hour.Text = mDeal.EndHour;
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
           

            String name = tbName.Text;
            String details = tbDetails.Text;
            decimal originalPrice = Convert.ToDecimal(tbOriginalPrice.Text);
            DateTime experationDate = (DateTime)dpExperationDate.SelectedDate;
            String startHour = tbStart_hour.Text;
            String endHour = tbEnd_Hour.Text;
            mAdminBL.updateDeal(mDeal, name, details, originalPrice, experationDate, startHour, endHour);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
