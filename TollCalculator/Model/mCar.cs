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
    public class mCar : ViewModelBase, IVehicle
    {
        #region Properties
        //Om man framöver kommer att vilja spara alla passeringar
        private List<VehicleFees> _lstTotalFee;
        public List<VehicleFees> lstTotalFee
        {
            get
            {
                return _lstTotalFee;
            }
            set
            {
                _lstTotalFee = value;
                NotifyPropertyChanged(nameof(lstTotalFee));
            }
        }

        private string _LicensePlate;
        public string LicensePlate
        {
            get
            {
                return _LicensePlate;
            }
            set
            {
                _LicensePlate = value;
                NotifyPropertyChanged(nameof(LicensePlate));
            }
        }
        private int _Fee;
        public int Fee
        {
            get
            {
                return _Fee;
            }
            set
            {
                _Fee = value;
                NotifyPropertyChanged(nameof(Fee));
            }
        }

        private DateTime? _FeeDate;
        public DateTime? FeeDate
        {
            get
            {
                return _FeeDate;
            }
            set
            {
                _FeeDate = value;
                NotifyPropertyChanged(nameof(FeeDate));
            }
        }

        private int _MaxHourFee;
        public int MaxHourFee
        {
            get
            {
                return _MaxHourFee;
            }    
            set
            {
                _MaxHourFee = value;
                NotifyPropertyChanged(nameof(MaxHourFee));
            }
        }

        private int _TotalDayFee;
        public int TotalDayFee
        {
            get
            {
                return _TotalDayFee;
            }
            set
            {
                _TotalDayFee = value;
                NotifyPropertyChanged(nameof(TotalDayFee));
            }
        }

        public string TypString
        {
            get
            {
                return DataHandler.ToFriendlyString(Typ);
            }
        }

        private enumVhicleType _Typ; 

        public enumVhicleType Typ
        {
            get
            {
                return _Typ;
            }
            set
            {
                _Typ = value;
                NotifyPropertyChanged(nameof(Typ));
            }
        }
            
        public Boolean IsFeeVehicle()
        {
            return enumVhicleType.SwedishCar == Typ;
        }
        #endregion
        #region Functions
        public bool HasVehicleBeenChargedThisHour
        {
            get
            {
                if (FeeDate == null || DateTime.Now.Date != (DateTime)FeeDate?.Date) return false;

                TimeSpan ts = DateTime.Now - (DateTime)FeeDate;
                if (ts.Hours == 0 && ts.Minutes < 60)
                    return true;
                return false;                        
            }
        }
       
        #endregion

    }
}
