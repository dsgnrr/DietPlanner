using DietPlanner.Classes;
using DietPlanner.Interfaces;
using NodaTime.Text;
using NodaTime;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DietPlanner.Pages
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private User user;
        private IFile file;
        public Profile()
        {
            InitializeComponent();
            LoadUserInfo();

        }
        private void LoadUserInfo()
        {
            file = new XmlFormat("UserConfig.xml");
            user = file.Load<User>();
            if(user is not null)
            {
                // User Goal
                if (user.Goal == 1)
                    goal1.IsChecked = true;
                else if (user.Goal == 2)
                    goal2.IsChecked = true;
                else goal3.IsChecked = true;

                // User Gender
                if (user.Gender == 1)
                    gender1.IsChecked = true;
                else gender2.IsChecked = true;

                //User Weight
                if (user.UnitOfWeight == 1)
                    u_weight1.IsChecked = true;
                else u_weight2.IsChecked = true;
                WeightTextBox.Text = user.Weight.ToString();

                // User Height
                if (user.UnitOfHeight == 1)
                    u_height1.IsChecked = true;
                else u_height2.IsChecked = true;
                HeightTextBox.Text = user.Height.ToString();

                // User BirthDate
                DayTextBox.Text = user.BirthDate.Day.ToString("00");
                MounthTextBox.Text = user.BirthDate.Month.ToString("00");
                YearTextBox.Text = user.BirthDate.Year.ToString("0000");
            }
        }

        #region VALIDATION
        private bool WeightValid()
        {
            WeightTextBox.Text.Replace(',', '.');
            bool isValid = Regex.IsMatch(WeightTextBox.Text, @"^[0-9]*\.?[0-9]+$");
            if (WeightTextBox.Text.Trim() != String.Empty && isValid)
            {
                weightError.Visibility = Visibility.Hidden;
                user.Weight = double.Parse(WeightTextBox.Text, CultureInfo.InvariantCulture);
                if (user.Weight <= 200.0)
                    return true;
                else if(user.Weight > 200.0)
                {
                    WeightTextBox.Clear();
                    weightError.Text = "Incorrect weight";
                    weightError.Visibility = Visibility.Visible;
                }
            }
            else
            {
                WeightTextBox.Clear();
                weightError.Text = "Incorrect weight format";
                weightError.Visibility = Visibility.Visible;
            }
            return false;
        }
        private bool HeightValid()
        {
            HeightTextBox.Text.Replace(',', '.');
            bool isValid = Regex.IsMatch(HeightTextBox.Text, @"^[0-9]*\.?[0-9]+$");
            if (HeightTextBox.Text.Trim() != String.Empty && isValid)
            {
                heightError.Visibility = Visibility.Hidden;

                user.Height = double.Parse(HeightTextBox.Text, CultureInfo.InvariantCulture);
                if (user.Height <= 260)
                {
                    return true;
                }
                else
                {
                    HeightTextBox.Clear();
                    heightError.Text = "Incorrect height";
                    heightError.Visibility = Visibility.Visible;
                }
            }
            else
            {
                HeightTextBox.Clear();
                heightError.Text = "Incorrect height format";
                heightError.Visibility = Visibility.Visible;
            }
            return true;
        }
        private bool BirthDateValid()
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
                        return true;
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
            return false;
        }
        #endregion

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(WeightValid()&&HeightValid()&&BirthDateValid())
            {
                // User Goal
                if (goal1.IsChecked == true)
                    user.Goal = 1;
                else if (goal2.IsChecked == true)
                    user.Goal = 2;
                else user.Goal = 3;

                // User Gender
                if (gender1.IsChecked == true)
                    user.Gender = 1;
                else user.Gender = 2;

                //User Weight
                if (u_weight1.IsChecked == true)
                    user.UnitOfWeight = 1;
                else user.UnitOfWeight = 2;

                // User Height
                if (u_height1.IsChecked == true)
                    user.UnitOfHeight = 1;
                else user.UnitOfHeight = 2;
                file.Save(user);
                SuccessButton.Visibility = Visibility.Visible;
            }
            else
            {
                SuccessButton.Visibility = Visibility.Hidden;
            }
        }
    }
}

