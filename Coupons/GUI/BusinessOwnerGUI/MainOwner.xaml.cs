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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainOwner : Window
    {


        public MainOwner()
        {
            InitializeComponent();
        }


        private void Button_InserNewDeal(object sender, RoutedEventArgs e)
        {
            var form1 = new OwnerNewDeal();
            form1.Show();   
        }
    }
}
