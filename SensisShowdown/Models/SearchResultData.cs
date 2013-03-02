using System;
using System.ComponentModel;

namespace SensisShowdown.Models
{
    public class SearchResultData : INotifyPropertyChanged, IComparable
    {
        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                NotifyPropertyChanged("Latitude");
            }
        }

        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                NotifyPropertyChanged("Longitude");
            }
        }

        private string _locationName;
        public string LocationName
        {
            get { return _locationName; }
            set
            {
                _locationName = value;
                NotifyPropertyChanged("LocationName");
            }
        }

        private bool _isResult1;
        public bool IsResult1
        {
            get { return _isResult1; }
            set
            {
                _isResult1 = value;
                NotifyPropertyChanged("IsResult1");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SearchResultData()
        {
        }

        public SearchResultData(string locationString)
        {
            SetLocation(locationString);
        }

        public void SetLocation(string locationString)
        {
            if (string.IsNullOrWhiteSpace(locationString))
                throw new InvalidBeachCoordinates();

            var stringPortions = locationString.Split(' ');
            if (stringPortions.Length != 2)
                throw new InvalidBeachCoordinates();

            Latitude = Convert.ToDouble(stringPortions[0]);
            Longitude = Convert.ToDouble(stringPortions[1]);
        }

        public void SetName(string statusString)
        {
            if (string.IsNullOrWhiteSpace(statusString))
                return;

            string result;
            if (statusString.Contains("pollution Likely"))
                result = statusString.Replace("pollution Likely", string.Empty);
            else if (statusString.Contains("pollution Unlikely"))
                result = statusString.Replace("pollution Unlikely", string.Empty);
            else
                result = statusString.Split(' ')[0];

            LocationName = result.Trim();
        }

        public int CompareTo(object obj)
        {
            var beachData = obj as SearchResultData;
            return string.CompareOrdinal(LocationName, beachData.LocationName);
        }
    }

    public class InvalidBeachCoordinates : Exception
    {
    }
}