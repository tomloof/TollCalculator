using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TollCalculator.DAL.Enums.Enums;

namespace TollCalculator.Handler
{
    public static class DataHandler
    {
        public static List<string> lstVehicleType { get; set; }
        public static void loadData()
        {
            lstVehicleType = new List<string> { "Swedish Car", "Motorbike", "Tractor", "Emergency", "Diplomat", "Foreign", "Military" };
        }
        public static string ToFriendlyString(enumVhicleType VhicleType)
        {
            switch (VhicleType)
            {
                case enumVhicleType.Diplomat:
                    return "Diplomat";
                case enumVhicleType.Emergency:
                    return "Emergency";
                case enumVhicleType.Foreign:
                    return "Foreign";
                case enumVhicleType.Military:
                    return "Military";
                case enumVhicleType.Motorbike:
                    return "Motorbike";
                case enumVhicleType.SwedishCar:
                    return "Swedish Car";
                case enumVhicleType.Tractor:
                    return "Tractor";
                default:
                    return "Not implemented yet!";
            }
        }
    }
}
