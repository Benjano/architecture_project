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

namespace Coupons.GUI
{
    /// <summary>
    /// Interaction logic for CreateBusiness.xaml
    /// </summary>
    public partial class CreateBusinessWindow : Window
    {
        BusinessOwnerBL mOwnerBL;
        BusinessOwner mOwner;
        public CreateBusinessWindow(BusinessOwner owner)
        {
            InitializeComponent();
            mOwnerBL = new BusinessOwnerBL();
            mOwner = owner;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            String name = tbName.Text;
            String description = tbDescription.Text;
            String address = tbAddress.Text;
            String city = tbCity.Text;
            mOwnerBL.insertNewBusiness(mOwner.ID, name, description, city, address);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
