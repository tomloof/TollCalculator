using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculator.ViewModel;

namespace TollCalculator.Classes
{
    public class VehicleFees : ViewModelBase
    {
        public VehicleFees(DateTime _dateTime, int _fee)
        {
            FeeDateTime = _dateTime;
            Fee = _fee;
        }
        public VehicleFees()
        {
            
        }

        private DateTime _FeeDateTime;
        public DateTime FeeDateTime
        {
            get
            {
                return _FeeDateTime;
            }
            set
            {
                _FeeDateTime = value;
                NotifyPropertyChanged(nameof(FeeDateTime));
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
    }
}
