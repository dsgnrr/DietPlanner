using DietPlanner.Pages;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DietPlanner.AppWindows
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            StatisticButton.IsChecked = true;
            NavigateFrame.Content = null;
            NavigateFrame.Navigate(new DailyStatistics());
        }

        #region WINDOW_EVENTS
        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Minimized;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowStateBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #endregion

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateFrame.Content = null;
            NavigateFrame.Navigate(new DailyStatistics());
        }

        private void ProgressButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateFrame.Content = null;
            //ВРЕМЕННО
            NavigateFrame.Navigate(new Eating());
        }

        private void DietsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateFrame.Content = null;
            NavigateFrame.Navigate(new SearchRecipe());
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateFrame.Content = null;
            NavigateFrame.Navigate(new Profile());
        }
    }
}
