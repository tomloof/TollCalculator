using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculator.Service;

namespace TollCalculator.DAL.Enums
{
    public class Enums
    {
        
        public enum enumVhicleType
        {
            SwedishCar = 0,
            Motorbike = 1,
            Tractor = 2,
            Emergency = 3,
            Diplomat = 4,
            Foreign = 5,
            Military = 6
        }
    }
}
