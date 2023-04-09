using DietPlanner.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DietPlanner.Pages
{
    /// <summary>
    /// Логика взаимодействия для Eating.xaml
    /// </summary>
    /// 

    //ДЛЯ ПРИМЕРА
    public class Food
    {
        public string Name { get; set; }
        public string Carbohydrates { get; set; }
        public string Proteins { get; set; }
        public string Fats { get; set; }
        public string Calories { get; set; }
    }

    public partial class Eating : Page
    {
        ObservableCollection<Food> Foods;
        public Eating()
        {
            InitializeComponent();
            Foods = new() { new Food()
            {
                Name="Example",
                Calories="12",
                Carbohydrates="13",
                Fats="14",
                Proteins="15"
            },
             new Food()
            {
                Name="Example",
                Calories="12",
                Carbohydrates="13",
                Fats="14",
                Proteins="15"
            }
            };
            DataContext = new SearchListModel { Foods = Foods };
        }

    }
}
