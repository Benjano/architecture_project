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
    /// Interaction logic for CreateDeal.xaml
    /// </summary>
    public partial class CreateDealWindow : Window
    {
        BusinessOwnerController mOwnerBL;
        Business mBusiness;
        public CreateDealWindow(Business business)
        {
            InitializeComponent();
            mOwnerBL = new BusinessOwnerController();
            mBusiness = business;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            String name = tbName.Text;
            String details = tbDetails.Text;
            decimal originalPrice = Convert.ToDecimal(tbOriginalPrice.Text);
            DateTime experationDate = (DateTime)dpExperationDate.SelectedDate;
            int startHour_h = Convert.ToInt32(tbStart_hour_h.Text);
            int startHour_m = Convert.ToInt32(tbStart_hour_m.Text);
            int endHour_h = Convert.ToInt32(tbEnd_Hour_h.Text);
            int endHour_m = Convert.ToInt32(tbEnd_Hour_m.Text);

            mOwnerBL.InsertNewDeal(name, details, mBusiness, originalPrice, experationDate, startHour_h, startHour_m, endHour_h, endHour_m);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
