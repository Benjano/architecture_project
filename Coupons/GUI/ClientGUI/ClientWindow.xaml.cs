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
using System.Device;
using System.Device.Location;
using Coupons.Enums;


namespace Coupons.GUI.ClientGUI
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {

        ClientBL mClientBl = new ClientBL();
        List<Deal> mDeals;
        List<Business> mBusinesses;
        List<Category> mCategories;
        List<string> mPossibleCities;

        GeoCoordinateWatcher _watcher;

        public ClientWindow(Client client)
        {
            InitializeComponent();
            lblUserName.Content = client.Username;



            mDeals = mClientBl.getAllDeal();
            mBusinesses = mClientBl.getAllBusiness();
            mCategories = Enum.GetValues(typeof(Category)).Cast<Category>().ToList<Category>();
            mPossibleCities = mClientBl.getPossibleCities();

            setDealsDataGrid(mDeals);



            cbCategory.ItemsSource = mCategories;
            cbCategory.SelectedIndex = 0;

            cbCity.ItemsSource = mPossibleCities;
            cbCity.SelectedIndex = 0;
        }


        private void reloadData()
        {
            mDeals = mClientBl.getAllDeal();
            mBusinesses = mClientBl.getAllBusiness();
            mCategories = Enum.GetValues(typeof(Category)).Cast<Category>().ToList<Category>();
            mPossibleCities = mClientBl.getPossibleCities();
        }

        /***************** SETCTION DEALS *****************/
        public void setDealsDataGrid(List<Deal> deals)
        {
            dgDeals.Columns.Clear();
            DataGridTextColumn colName = new DataGridTextColumn();
            colName.Header = "Name";
            colName.Binding = new Binding("Name");
            colName.Width = 100;

            DataGridTextColumn colDetails = new DataGridTextColumn();
            colDetails.Header = "Details";
            colDetails.Binding = new Binding("Details");
            colDetails.Width = 200;

         

            dgDeals.Columns.Add(colName);
            dgDeals.Columns.Add(colDetails);

            dgDeals.ItemsSource = deals;
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string city = cbCity.SelectedItem.ToString();
            Category category = (Category) cbCategory.SelectedItem;

            List<Deal> result = new List<Deal>();

            foreach (Business business in mBusinesses)
            {
                if (business.Categories.Contains(category) && business.City.ToLower().Equals(city))
                {
                    foreach (Deal deal in mDeals)
                    {
                        if (deal.Business == business.ID && deal.ExperationDate >= DateTime.Now)
                        {
                            result.Add(deal);
                        }
                    }
                }
            }

            setDealsDataGrid(result);
        }

        private void btLocation_Click(object sender, RoutedEventArgs e)
        {
            _watcher = new GeoCoordinateWatcher();


            _watcher.PositionChanged += watcher_PositionChanged;
            _watcher.Start();


            MessageBox.Show(_watcher.Position.Location.Latitude.ToString() + "," + _watcher.Position.Location.Longitude.ToString());

        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            MessageBox.Show(e.Position.Location.Latitude.ToString() + "," + e.Position.Location.Longitude.ToString());
            _watcher.Stop();
        }

        private void btnFavorit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
