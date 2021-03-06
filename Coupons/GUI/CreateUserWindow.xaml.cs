﻿using Coupons.BL;
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
using Coupons.Models;

namespace Coupons
{
    /// <summary>
    /// Interaction logic for CreateClientWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        ClientController mClientBL;
        AdminController mAdminBL;
        Window mSourceWindow;
        private SensorController mSensorController;
        bool mIsClient;

        public CreateUserWindow(Window sourceWindow, bool isClient)
        {
            InitializeComponent();
            mSourceWindow = sourceWindow;
            mClientBL = new ClientController();
            mAdminBL = new AdminController();
            mSensorController = new SensorController();
            mSensorController.AddSensor(new Location());
            cbGender.ItemsSource = Enum.GetValues(typeof(Gender));
            cbGender.SelectedIndex = 0;
            dpBirthDate.SelectedDate = DateTime.Today;
            mIsClient = isClient;

            if (!mIsClient)
            {
                lblBirthDate.Visibility = Visibility.Hidden;
                lblGender.Visibility = Visibility.Hidden;

                cbGender.Visibility = Visibility.Hidden;
                dpBirthDate.Visibility = Visibility.Hidden;

                lblPayPal.Visibility = Visibility.Hidden;
                tbPayPal_info.Visibility = Visibility.Hidden;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {

            String username = tbUsername.Text;
            String password = tbPassword.Text;
            String mail = tbMail.Text;
            String phone = tbPhone.Text;
            Gender gender = (Gender) cbGender.SelectedItem;
            DateTime birthdate = (DateTime)dpBirthDate.SelectedDate;
            Location loc = (Location)mSensorController.GetSensor("Location");

            if (username.Length > 2 && password.Length > 5 && mail.Length > 7 && phone.Length > 6)
            {
                if (mIsClient)
                {
                    if (birthdate != null)
                    {
                        mClientBL.InsertNewClient(username, password, mail, phone, birthdate, gender, loc.ToString());
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

        private void btnPayPal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("www.PayPal.com");
            }
            catch (Exception exc1)
            {
                if (exc1.GetType().ToString() != "System.ComponentModel.Win32Exception")
                {
                    try
                    {
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("IExplore.exe", "www.google.com");
                        System.Diagnostics.Process.Start(startInfo);
                        startInfo = null;
                    }
                    catch (Exception exc2)
                    {
                        MessageBoxResult result = MessageBox.Show("whay you don't have browser",
                                  "loser", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}
