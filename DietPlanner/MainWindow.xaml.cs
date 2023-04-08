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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security.Policy;
using DietPlanner.Classes;
using DietPlanner.Interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DietPlanner
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User user;
        private IFile file = new XmlFormat("UserConfig.xml");
        public MainWindow()
        {
            InitializeComponent();
            user = file.Load<User>();

        }

        private void Male_Click(object sender, RoutedEventArgs e)
        {
            user.Gender = 1;
        }

        private void Female_Click(object sender, RoutedEventArgs e)
        {
            user.Gender = 2;
        }

        private void First_Click(object sender, RoutedEventArgs e)
        {
            user.Goal = 1;
        }

        private void Second_Click(object sender, RoutedEventArgs e)
        {
            user.Goal = 2;
        }

        private void Third_Click(object sender, RoutedEventArgs e)
        {
            user.Goal= 3;
        }

        private void Weight_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            Weight_Text.Text.Replace(',', '.');
            bool isValid = Regex.IsMatch(Weight_Text.Text, @"^[0-9]*\.?[0-9]+$");
            if (Weight_Text.Text.Trim() != String.Empty && isValid)
            {

                user.Weight = double.Parse(Weight_Text.Text, CultureInfo.InvariantCulture);
            }
        }

        private void Height_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            Height_Text.Text.Replace(',', '.');
            bool isValid = Regex.IsMatch(Height_Text.Text, @"^[0-9]*\.?[0-9]+$");
            if (Height_Text.Text.Trim() != String.Empty && isValid)
            {

                user.Weight = double.Parse(Height_Text.Text, CultureInfo.InvariantCulture);
            }
        }

        private void KG_w_Click(object sender, RoutedEventArgs e)
        {
            user.UnitOfWeight = 1;
        }

        private void FT_w_Click(object sender, RoutedEventArgs e)
        {
            user.UnitOfWeight = 2;
        }

        private void CM_h_Click(object sender, RoutedEventArgs e)
        {
            user.UnitOfHeight = 1;
        }

        private void FT_h_Click(object sender, RoutedEventArgs e)
        {
            user.UnitOfHeight = 2;
        }
    }

   
}

