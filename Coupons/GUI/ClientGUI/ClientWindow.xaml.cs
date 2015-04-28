using Coupons.Enums;
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

namespace CouponsApplication
{
    /// <summary>
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {

        Client mClient;
        public ClientWindow(Client client)
        {
            InitializeComponent();
            mClient = client;
            cbCategory.ItemsSource = Enum.GetValues(typeof(Category));



        }

        private void BUT_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }

}
