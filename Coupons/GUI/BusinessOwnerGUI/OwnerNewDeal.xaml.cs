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

namespace Coupons.GUI.BusinessOwner
{

    public partial class OwnerNewDeal : Window
    {
        public BusinessOwnerBL mBL;

        public OwnerNewDeal(BusinessOwner owner )
        {
            mBL = new BusinessOwnerBL(); 
        }
        public OwnerNewDeal()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = txtUserName.ToString();
            string details = txtDetails.ToString();
            decimal price = Convert.ToDecimal(txtPrice.Text);
            DateTime date = Convert.ToDateTime(txtDate.Text);

            mBL.insertNewDeal(name, details, ,price,date);

        }

      
    }
}
