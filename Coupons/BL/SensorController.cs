using Coupons.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.BL
{
    class SensorController
    {
        private List<ISensor> mSensors;

        public SensorController()
        {
            mSensors = new List<ISensor>();
        }

        public void AddSensor(ISensor sensor)
        {
            mSensors.Add(sensor);
        }

        public ISensor GetSensor(string type)
        {
            foreach (ISensor sensor in mSensors)
            {
                if (sensor.GetType().Equals(type))
                {
                    return sensor;
                }
            }
            return null;
        }


    }
}
