using DietPlanner.Classes;
using DietPlanner.Interfaces;
using NodaTime;
using NodaTime.Text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class Intro : Window
    {
        private int pagePosition;
        private StackPanel[] stackPanels;
        private User user;
        private IFile file;
        public Intro()
        {
            InitializeComponent();
            user = new();
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

        #region BUTTONS_EVENTS
        private void goalSelector_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button == goal1)
                user.Goal = 1;
            else if (button == goal2)
                user.Goal = 2;
            else user.Goal = 3;
            stackPanels[pagePosition].Visibility = Visibility.Collapsed;
            pagePosition++;
            stackPanels[pagePosition].Visibility = Visibility.Visible;
            
        }
        private void genderSelector_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button == gender1)
                user.Gender = 1;
            else user.Gender = 2;
            stackPanels[pagePosition].Visibility = Visibility.Collapsed;
            pagePosition++;
            stackPanels[pagePosition].Visibility = Visibility.Visible;
        }
        private void selectWeight_Click(object sender, RoutedEventArgs e)
        {
            WeightTextBox.Text.Replace(',', '.');
            bool isValid = Regex.IsMatch(WeightTextBox.Text, @"^[0-9]*\.?[0-9]+$");
            if (WeightTextBox.Text.Trim() != String.Empty && isValid)
            {
                weightError.Visibility = Visibility.Hidden;

                user.Weight = double.Parse(WeightTextBox.Text, CultureInfo.InvariantCulture);
                if (u_weight1.IsChecked == true)
                    user.UnitOfWeight = 1;
                else user.UnitOfWeight = 2;

                stackPanels[pagePosition].Visibility = Visibility.Collapsed;
                pagePosition++;
                stackPanels[pagePosition].Visibility = Visibility.Visible;
            }
            else
            {
                WeightTextBox.Clear();
                weightError.Text = "Incorrect weight format";
                weightError.Visibility = Visibility.Visible;
            }
        }
        private void selectHeight_Click(object sender, RoutedEventArgs e)
        {
            HeightTextBox.Text.Replace(',', '.');
            bool isValid = Regex.IsMatch(HeightTextBox.Text, @"^[0-9]*\.?[0-9]+$");
            if (HeightTextBox.Text.Trim() != String.Empty && isValid)
            {
                heightError.Visibility = Visibility.Hidden;

                user.Height = double.Parse(HeightTextBox.Text, CultureInfo.InvariantCulture);
                if (u_height1.IsChecked == true)
                    user.UnitOfHeight = 1;
                else user.UnitOfHeight = 2;

                stackPanels[pagePosition].Visibility = Visibility.Collapsed;
                pagePosition++;
                stackPanels[pagePosition].Visibility = Visibility.Visible;
            }
            else
            {
                HeightTextBox.Clear();
                heightError.Text = "Incorrect height format";
                heightError.Visibility = Visibility.Visible;
            }
        }
        private void selectBirthDate_Click(object sender, RoutedEventArgs e)
        {
            if (YearTextBox.Text.Trim() != String.Empty &&
            MounthTextBox.Text.Trim() != String.Empty &&
            DayTextBox.Text.Trim() != String.Empty)
            {
                Regex regex = new Regex(@"^(\d{4})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$");
                var dateResult = $"{YearTextBox.Text}-{MounthTextBox.Text}-{DayTextBox.Text}";
                if (regex.IsMatch(dateResult))
                {
                     LocalDate localDate;
                    try
                    {
                        localDate = LocalDatePattern.Iso.Parse(dateResult).Value;
                        user.BirthDate = localDate.AtMidnight().ToDateTimeUnspecified();
                        file = new XmlFormat("UserConfig.xml");
                        file.Save(user);
                        stackPanels[pagePosition].Visibility = Visibility.Collapsed;
                    }
                    catch (UnparsableValueException)
                    {
                        YearTextBox.Clear();
                        MounthTextBox.Clear();
                        DayTextBox.Clear();
                        birthDateError.Visibility = Visibility.Visible;
                        birthDateError.Text = "Incorrect date";
                    }
                }
                else
                {
                    YearTextBox.Clear();
                    MounthTextBox.Clear();
                    DayTextBox.Clear();
                    birthDateError.Visibility = Visibility.Visible;
                    birthDateError.Text = "Incorrect date format";
                }
            }
            else
            {
                birthDateError.Visibility = Visibility.Visible;
                birthDateError.Text = "Birth date fields are empty";
            }
           
        }
        #endregion

    }
}
