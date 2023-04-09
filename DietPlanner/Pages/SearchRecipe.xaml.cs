using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
    /// 

    public class RecipeInstructions
    {
        public string Name { get; set; }
        public List<Step> Steps { get; set; }
    }

    public class Step
    {
        public int Number { get; set; }
        public string Steps { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public partial class SearchRecipe : Page
    {
        private readonly HttpClient _client = new HttpClient();

        private readonly string _apiKey = "5330ed77c83a4ecf965e38cdb7b80ac3";
        public SearchRecipe()
        {
            InitializeComponent();
        }

        private Button createSearchResult(string result)
        {
            Button button = new Button();
            button.Style = (Style)FindResource("DefaultButton");
            button.Margin = new Thickness(5);
            button.FontSize = 15;
            button.Content = result;
            button.Click += new RoutedEventHandler(openRecipe);
            return button;
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
                    string result = missedIngredient.title;
                    searchResultsBlock.Visibility = Visibility.Collapsed;
                    show_recipe.Children.Add(createSearchResult(result));
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message, "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void openRecipe(object sender, RoutedEventArgs e)
        {
            // Start Recipe Window
            RecipeNavigate.Navigate(new RecipePage() { owner = this });

            Button button = (Button)sender;
            string buttonText = button.Content.ToString();



        }
    }
}

        


