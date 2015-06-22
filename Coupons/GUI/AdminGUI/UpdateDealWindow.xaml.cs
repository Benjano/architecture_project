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
            string[] StartHour = mDeal.StartHour.Split(new Char[] { ':' }), EndHour = mDeal.EndHour.Split(new Char[] { ':' });
            tbStart_hour_h.Text = StartHour[0];
            tbStart_hour_m.Text = StartHour[1];
            tbEnd_Hour_h.Text = EndHour[0];
            tbEnd_Hour_m.Text = EndHour[1];
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
           

            String name = tbName.Text;
            String details = tbDetails.Text;
            decimal originalPrice = Convert.ToDecimal(tbOriginalPrice.Text);
            DateTime experationDate = (DateTime)dpExperationDate.SelectedDate;
            int startHour_h = Convert.ToInt32(tbStart_hour_h.Text);
            int startHour_m = Convert.ToInt32(tbStart_hour_m.Text);
            int endHour_h = Convert.ToInt32(tbEnd_Hour_h.Text);
            int endHour_m = Convert.ToInt32(tbEnd_Hour_m.Text);
            mAdminBL.updateDeal(mDeal, name, details, originalPrice, experationDate, endHour_h, endHour_m, endHour_h, endHour_m);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
