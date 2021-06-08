using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculator.Classes;
using TollCalculator.DAL.Interface;
using TollCalculator.Handler;
using TollCalculator.ViewModel;
using static TollCalculator.DAL.Enums.Enums;

namespace TollCalculator.Model
{
    class mOther : ViewModelBase, IVehicle
    {
        public int Fee { get; set; }
        public DateTime? FeeDate { get; set; }
        public int TotalDayFee { get; set; }
        public int MaxHourFee { get; }
        public enumVhicleType Typ { get; set; }
        public Boolean IsFeeVehicle()
        {
            return false;

        }
        public string LicensePlate { get; set; }
        public string TypString
        {
            get
            {
                return DataHandler.ToFriendlyString(Typ);
            }
        }
        public bool HasVehicleBeenChargedThisHour { get; }
        public List<VehicleFees> lstTotalFee { get; set; }


    }
}
