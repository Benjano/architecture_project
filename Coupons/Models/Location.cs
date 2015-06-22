using Coupons.Models.Interface;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.Models
{
    public class Location : ISensor
    {
        private string mLongitude;
        public string Longitude {
            get {return mLongitude;}
        }

        private string mLatitude;
        public string Latitude
        {
            get { return mLatitude; }
        }

        public Location(string rawLocation)
        {
            string[] raw = rawLocation.Split(',');
            if (raw.Count() == 2)
            {
                mLongitude = raw[0];
                mLatitude = raw[1];
            }
            else
            {
                mLongitude = null;
                mLatitude = null;
            }

        }
        GeoCoordinateWatcher mGeoWatcher;

        public Location()
        {
            mGeoWatcher = new GeoCoordinateWatcher();
            mGeoWatcher.Start();
            mLongitude = mGeoWatcher.Position.Location.Longitude.ToString();
            mLatitude =  mGeoWatcher.Position.Location.Latitude.ToString();
        }

        public void CalcLocation(){
            mLongitude = mGeoWatcher.Position.Location.Longitude.ToString();
            mLatitude =  mGeoWatcher.Position.Location.Latitude.ToString();
        }

        public string ToString()
        {
            return mLongitude + "," + mLatitude;
        }



        public string GetType()
        {
            return "Location";
        }
    }
}
