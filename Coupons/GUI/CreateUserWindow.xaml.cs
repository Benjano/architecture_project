using Coupons.BL;
using Coupons.Enums;
using CouponsApplication;
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
using System.Device.Location;

namespace Coupons
{
    /// <summary>
    /// Interaction logic for CreateClientWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        ClientBL mClientBL;
        AdminBL mAdminBL;
        GeoCoordinateWatcher mGeoWatcher;
        Window mSourceWindow;
        bool mIsClient;

        public CreateUserWindow(Window sourceWindow, bool isClient)
        {
            InitializeComponent();
            mSourceWindow = sourceWindow;
            mClientBL = new ClientBL();
            mAdminBL = new AdminBL();
            cbGender.ItemsSource = Enum.GetValues(typeof(Gender));
            cbGender.SelectedIndex = 0;
            dpBirthDate.SelectedDate = DateTime.Today;
            mGeoWatcher = new GeoCoordinateWatcher();
            mGeoWatcher.Start();
            mIsClient = isClient;

            if (!mIsClient)
            {
                lblBirthDate.Visibility = Visibility.Hidden;
                lblGender.Visibility = Visibility.Hidden;

                cbGender.Visibility = Visibility.Hidden;
                dpBirthDate.Visibility = Visibility.Hidden;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {

            String username = tbUsername.Text;
            String password = tbPassword.Text;
            String mail = tbMail.Text;
            String phone = tbPhone.Text;
            Gender gender = (Gender) cbGender.SelectedItem;
            DateTime birthdate = (DateTime) dpBirthDate.SelectedDate;
            String location = mGeoWatcher.Position.Location.ToString();

            if (username.Length > 2 && password.Length > 5 && mail.Length > 7 && phone.Length > 6)
            {
                if (mIsClient)
                {
                    if (birthdate != null)
                    {
                        mClientBL.insertNewClient(username, password, mail, phone, birthdate, gender, location);
                        FINISH();
                    }
                } else {
                        mAdminBL.insertBusinessOwner(username, password, mail, phone);
                        FINISH();
                    }   
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Please check your information",
                                "Missing information", MessageBoxButton.OK, MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {

                }
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            FINISH();
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        public void FINISH()
        {
            if (mSourceWindow != null)
            {
                mSourceWindow.Show();
            }
            this.Close();
        }
    }
}
