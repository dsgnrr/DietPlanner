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


namespace DietPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _apiKey = "bf72237be72d44549d0acc3588ce6dfb";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var url = $"https://api.spoonacular.com/recipes/findByIngredients?apiKey={_apiKey}&ingredients={IngredientsTextBox}";
            try
            { 
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject<dynamic>(content);

                //Console.WriteLine(data);



                foreach (var missedIngredient in data)
                {
                    string tmp = missedIngredient.title;
                    names.Text += tmp + "\n";
                    

                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

