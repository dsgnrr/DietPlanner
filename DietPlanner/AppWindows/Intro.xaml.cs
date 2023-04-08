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
    /// Логика взаимодействия для Intro.xaml
    /// </summary>
    public partial class Intro : Window
    {
        private int pagePosition;
        private StackPanel[] stackPanels;
        public Intro()
        {
            InitializeComponent();
            LoadPages();

        }
        private void LoadPages()
        {
            pagePosition = 0;
            stackPanels = new StackPanel[] { GoalPanel, GenderPanel, WeightPanel, HeightPanel, BirthDatePanel };
            GoalPanel.Visibility = Visibility.Visible;
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

        private void goalSelector_Click(object sender, RoutedEventArgs e)
        {
            stackPanels[pagePosition].Visibility = Visibility.Collapsed;
            pagePosition++;
            stackPanels[pagePosition].Visibility = Visibility.Visible;
        }
        private void genderSelector_Click(object sender, RoutedEventArgs e)
        {
            stackPanels[pagePosition].Visibility = Visibility.Collapsed;
            pagePosition++;
            stackPanels[pagePosition].Visibility = Visibility.Visible;
        }
        private void selectWeight_Click(object sender, RoutedEventArgs e)
        {
            stackPanels[pagePosition].Visibility = Visibility.Collapsed;
            pagePosition++;
            stackPanels[pagePosition].Visibility = Visibility.Visible;
        }
        private void selectHeight_Click(object sender, RoutedEventArgs e)
        {
            stackPanels[pagePosition].Visibility = Visibility.Collapsed;
            pagePosition++;
            //stackPanels[pagePosition].Visibility = Visibility.Visible;
        }
    }
}
