using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for RecipePage.xaml
    /// </summary>
    public partial class RecipePage : Page
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly string _apiKey = "f409ca598a494057a1eec3804ea58e13";
        public RecipePage()
        {

            InitializeComponent();
        }
        
        private async void But_Search_Click(object sender, RoutedEventArgs e)
        {

           
            var url = $"https://api.spoonacular.com/recipes/findByIngredients?apiKey={_apiKey}&ingredients={Search.Text}";

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

                    //show_recipe.Text += tmp + "\n";
                    Button button = new Button();

                    // задаем текст кнопки
                    button.Content = tmp;

                    // добавляем кнопку на панель
                    show_recipe.Children.Add(button);

                }

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }

        
}

