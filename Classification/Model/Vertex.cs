using System;
using Classification.Util;

namespace Classification.Model
{
    public class Vertex : PropertyChangedBase
    {
        private double _x;
        private double _y;

        public Guid Id { get; set; }

        public double X
        {
            get { return _x; }
            set
            {
                if (_x.Equals(value)) return;
                _x = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                if (_y.Equals(value)) return;
                _y = value;
                OnPropertyChanged();
            }
        }

        public Vertex(Guid id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
        }
    }
}
