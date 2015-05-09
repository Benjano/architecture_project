using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.Models
{
    public class Location
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

        public Location(string longitude, string latitude)
        {
            mLongitude = longitude;
            mLatitude = latitude;
        }
    }
}
