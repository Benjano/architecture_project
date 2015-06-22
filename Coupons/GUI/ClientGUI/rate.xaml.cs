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

namespace Coupons.GUI.ClientGUI
{
    /// <summary>
    /// Interaction logic for rate.xaml
    /// </summary>
    public partial class rate : Window
    {
        private ClientController mClientBL;
        private Coupon mCoupon;

        public rate(Coupon coupon)
        {
            InitializeComponent();
            mCoupon = coupon;
            mClientBL = new ClientController();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int rate=-1;
            if ((bool)RB1.IsChecked)
                rate = 1;
            else if ((bool)RB2.IsChecked)
                rate = 2;
            else if ((bool)RB3.IsChecked)
                rate = 3;
            else if ((bool)RB4.IsChecked)
                rate = 4;
            else if ((bool)RB5.IsChecked)
                rate = 5;
            mClientBL.RateCoupon(mCoupon, rate);
            Close();


        }




    }
}
