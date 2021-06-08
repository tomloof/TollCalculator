using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculator.Classes;
using static TollCalculator.DAL.Enums.Enums;

namespace TollCalculator.DAL.Interface
{
    public interface IVehicle
    {
        int Fee { get; set; }
        DateTime? FeeDate { get; set; }
        int TotalDayFee { get; set; }
        enumVhicleType Typ { get; set; }
        Boolean IsFeeVehicle();
        string LicensePlate { get; set; }
        string TypString { get; }
        int MaxHourFee { get;}
        bool HasVehicleBeenChargedThisHour { get; }
        List<VehicleFees> lstTotalFee { get; set; }

    }
}
