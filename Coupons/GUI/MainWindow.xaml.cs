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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Coupons.BL;
using Coupons.Models;
using Coupons.GUI.ClientGUI;
using Coupons;


namespace CouponsApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        UserBL mUserBL;

        public MainWindow()
        {
            InitializeComponent();
            mUserBL = new UserBL();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            String username = txtUsername.Text;
            String password = txtPassword.Password;

            User user = mUserBL.login(username, password);

            if (user != null)
            {
                if (user.GetType() == typeof(Admin))
                {

                }
                else if (user.GetType() == typeof(BusinessOwner))
                {

                }
                else if (user.GetType() == typeof(Client))
                {
                    ClientWindow clientWindow = new ClientWindow((Client)user);
                    clientWindow.Show();
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please check your username and password",
                                  "Wrong information", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {

                }
            }
            
        }

        private void btnCreateClient_Click(object sender, RoutedEventArgs e)
        {
            CreateClientWindow createClientWindow = new CreateClientWindow();
            createClientWindow.Show();
            this.Close();
        }
    }
}
