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

namespace DietPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string api_key = "bf72237be72d44549d0acc3588ce6dfb";
        public MainWindow()
        {
            InitializeComponent();
            GetRecipesAsync();



        }
        private async Task GetRecipesAsync()
        {
            using (var client = new HttpClient())
            {
                var url = $"https://api.spoonacular.com/recipes/complexSearch?apiKey={api_key}";
                var response = await client.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Connected");
                }
                else
                {
                    MessageBox.Show("No Connect");
                }
            }
        }
    }
}
