using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TollCalculator.Classes;
using TollCalculator.DAL.Interface;
using TollCalculator.Handler;
using TollCalculator.Model;
using TollCalculator.Service;
using static TollCalculator.DAL.Enums.Enums;

namespace TollCalculator.ViewModel
{
    class vmMainWindow : ViewModelBase
    {
        public vmMainWindow()
        {
            DataHandler.loadData();
            SelectedVhicleType = "Swedish Car";
            lstIVehicle = new ObservableCollection<IVehicle>();
        }

        #region Command
        private ICommand _RegisterNewVehicleCommand;
        public ICommand RegisterNewVehicleCommand
        {
            get
            {
                if (_RegisterNewVehicleCommand == null)
                {
                    _RegisterNewVehicleCommand = new RelayCommand(param => RegisterVehicle(), null);
                }
                return _RegisterNewVehicleCommand;
            }
        }
        #endregion

        #region Properties
        private int _VehicleFee;
        public int VehicleFee
        {
            get
            {
                return _VehicleFee;
            }
            set
            {
                _VehicleFee = value;
                NotifyPropertyChanged(nameof(VehicleFee));
            }
        }

        private ObservableCollection<IVehicle> _lstIVehicle;
        public ObservableCollection<IVehicle> lstIVehicle
        {
            get
            {
                return _lstIVehicle;
            }
            set
            {
                _lstIVehicle = value;
                NotifyPropertyChanged(nameof(lstIVehicle));
            }
        }

        public List<string> lstVehicleType
        {
            get
            {
                return DataHandler.lstVehicleType;
            }
        }

        private int _SelectedVhicleIndex;
        public int SelectedVhicleIndex
        {
            get
            {
                return _SelectedVhicleIndex;
            }
            set
            {
                _SelectedVhicleIndex = value;
                NotifyPropertyChanged(nameof(SelectedVhicleIndex));
            }
        }

        private string _SelectedVhicleType;
        public string SelectedVhicleType
        {
            get
            {
                return _SelectedVhicleType;
            }
            set
            {
                _SelectedVhicleType = value;
                NotifyPropertyChanged(nameof(SelectedVhicleType));
            }
        }

        private string _LicensePlateNumber;
        public string LicensePlateNumber
        {
            get
            {
                return _LicensePlateNumber;
            }
            set
            {
                _LicensePlateNumber = value;
                NotifyPropertyChanged(nameof(LicensePlateNumber));
            }
        }
        #endregion

        #region Functions
        private void RegisterVehicle()
        {
            IVehicle Vehicle = GetVehicle();

            if (Vehicle == null)
                return;
            SetVehicleFee(Vehicle);
            AddVehicleToList(Vehicle);
        }

        private IVehicle GetVehicle()
        {
            //Kolla om fordonet redan är registrerat idag och denna timmen
            if (lstIVehicle.Any(a => a.LicensePlate == LicensePlateNumber && (int)a.Typ != SelectedVhicleIndex))
            {
                MessageBox.Show("Det finns redan ett annat typ av fordon med registreringsskyllt " + LicensePlateNumber, "Fel registreringsskyllt", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
            else if (lstIVehicle.Any(a => a.LicensePlate == LicensePlateNumber && a.HasVehicleBeenChargedThisHour))
            {
                return lstIVehicle.Where(a => a.LicensePlate == LicensePlateNumber && a.FeeDate?.Hour >= DateTime.Now.AddHours(-1).Hour).Single();
            }
            else
            {
                return CreateNewVehicle();
            }
        }

        private void AddVehicleToList(IVehicle _Vehicle)
        {
            IVehicle listedVehicle = lstIVehicle.Where(a => a.LicensePlate == LicensePlateNumber && a.HasVehicleBeenChargedThisHour).SingleOrDefault();
            int indexOfValue = lstIVehicle.IndexOf(listedVehicle);
            if (_Vehicle.FeeDate == null)
                _Vehicle.FeeDate = DateTime.Now;

            if (listedVehicle == null)
            {
                lstIVehicle.Add(_Vehicle);
            }
            else
            {
                lstIVehicle.RemoveAt(indexOfValue);
                lstIVehicle.Insert(indexOfValue, _Vehicle);

            }
        }

        private void SetVehicleFee(IVehicle _Vehicle)
        {
            //Kolla om vi skall ta betalt för datum och fordon
            if (TollCalculatorHandler.IsTollFeeDate(DateTime.Now) && _Vehicle.IsFeeVehicle())
            {
                VehicleFee = TollCalculatorHandler.GetTollFee(DateTime.Now);
                if (_Vehicle.HasVehicleBeenChargedThisHour)
                {
                    if (VehicleFee > _Vehicle.Fee)
                    {
                        _Vehicle.TotalDayFee += (_Vehicle.TotalDayFee + VehicleFee - _Vehicle.Fee) <= 60 ? VehicleFee - _Vehicle.Fee : 60;
                        _Vehicle.Fee = VehicleFee;
                    }
                }
                else
                {
                    int VehicleDailyFee = lstIVehicle.Where(a => a.LicensePlate == _Vehicle.LicensePlate && a.FeeDate.Value.Date == DateTime.Now.Date).Sum(b => b.Fee);
                    _Vehicle.Fee = VehicleFee;
                    _Vehicle.TotalDayFee = VehicleDailyFee + VehicleFee;
                }
            }   
        }

        private IVehicle CreateNewVehicle()
        {
            IVehicle Vehicle = null;
            switch (SelectedVhicleType)
            {
                case "Swedish Car":
                    Vehicle = new mCar();
                    break;
                case "Motorbike":
                    Vehicle = new mMotorbike();
                    break;
                default:
                    Vehicle = new mOther();
                    break;
            }

            Vehicle.Typ = (enumVhicleType)SelectedVhicleIndex;
            Vehicle.LicensePlate = LicensePlateNumber;
            Vehicle.FeeDate = null;
            return Vehicle;
        }

        #endregion
    }
}
