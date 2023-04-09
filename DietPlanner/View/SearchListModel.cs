using DietPlanner.Model;
using DietPlanner.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.View
{
    internal class SearchListModel :INotifyPropertyChanged
    {
        private ObservableCollection<Food> food;

        public ObservableCollection<Food> Foods
        {
            get { return food; }
            set
            {
                if (food != value)
                {
                    food = value;
                    OnPropertyChanged("Food");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
