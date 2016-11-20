using System;
using Classification.Util;

namespace Classification.Model
{
    public class Building : PropertyChangedBase
    {
        private int _year;
        private int _countFloors;

        #region Properties

        public Guid Id { get; set; }

        public int Year
        {
            get { return _year; }
            set
            {
                if (_year == value) return;
                _year = value;
                OnPropertyChanged();
            }
        }

        public int CountFloors
        {
            get { return _countFloors; }
            set
            {
                if (_countFloors == value) return;
                _countFloors = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructors

        private Building() { }

        public Building(int year, int countFloors)
        {
            Id = Guid.NewGuid();
            Year = year;
            CountFloors = countFloors;
        }

        #endregion
    }
}
