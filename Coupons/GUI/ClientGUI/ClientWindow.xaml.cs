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


namespace Coupons.GUI.ClientGUI
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {

        CategoryBL mCategoryBL = new CategoryBL();
        BrandBL mBrandBL = new BrandBL();
        BusinessBL mBusinessBL = new BusinessBL();
        DealBL mDealBL = new DealBL();
        GeoCoordinateWatcher _watcher;

        public ClientWindow(Client client)
        {
            InitializeComponent();
            lblUserName.Content = client.Username;
    
            cbCategory.ItemsSource = mCategoryBL.GetAllCategories();
            cbCity.ItemsSource = mBusinessBL.GetBusinessCities();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string city = cbCity.SelectedItem.ToString();
            Category category = (Category)cbCategory.SelectedItem;
            //lvDeals.ItemsSource = mDealBL.GetDealsByCategoryAndCity(category,city);
            List<Deal> deals = mDealBL.GetDealsByCategoryAndCity(category, city);
            foreach (Deal deal in deals)
            {
                //ListViewItem item = new ListViewItem(){
                    
                //}
                
                lvDeals.Items.Add(item);
            }

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
    }
}
