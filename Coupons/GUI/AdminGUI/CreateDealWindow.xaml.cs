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
        BusinessOwnerBL mOwnerBL;
        Business mBusiness;
        public CreateDealWindow(Business business)
        {
            InitializeComponent();
            mOwnerBL = new BusinessOwnerBL();
            mBusiness = business;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            String name = tbName.Text;
            String details = tbDetails.Text;
            decimal originalPrice = Convert.ToDecimal(tbOriginalPrice.Text);
            DateTime experationDate = (DateTime)dpExperationDate.SelectedDate;
            mOwnerBL.insertNewDeal(name, details, mBusiness, originalPrice, experationDate);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
