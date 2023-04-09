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
    public partial class RecipePage : Page
    {
        private readonly HttpClient _client = new HttpClient();

        private readonly string _apiKey = "bf72237be72d44549d0acc3588ce6dfb";
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

               


                foreach (var missedIngredient in data)
                {
                    string tmp = missedIngredient.title;
                    //string id = missedIngredient.id;
                    Button button = new Button();

                    
                    button.Content = tmp;

                    button.Click += Button_Click;
                    show_recipe.Children.Add(button);

                }
                
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Content.ToString();

            
            string apiKey = "bf72237be72d44549d0acc3588ce6dfb";

            using var client = new HttpClient();
            var id = 673463; // замените на реальный идентификатор рецепта
            var response = await client.GetAsync($"https://api.spoonacular.com/recipes/{id}/information?apiKey={apiKey}");
            var content = await response.Content.ReadAsStringAsync();
            var recipe = JObject.Parse(content);

            MessageBox.Show($"Название: {recipe["title"]}");
            MessageBox.Show($"Количество порций: {recipe["servings"]}");
            MessageBox.Show($"Время приготовления: {recipe["readyInMinutes"]} минут");





            string url = $"https://api.spoonacular.com/recipes/{id}/analyzedInstructions?apiKey={apiKey}";
            using var _client = new HttpClient();

            // Execute the API request and get the response as a JSON string
            string json = await client.GetStringAsync(url);

            // Deserialize the JSON string into an object of type List<RecipeInstructions>
            List<RecipeInstructions> instructions = JsonConvert.DeserializeObject<List<RecipeInstructions>>(json);

            // Display the recipe name and the instructions
            //MessageBox.Show($"Instructions for {instructions[0].Name}:");
            foreach (var step in instructions[0].Steps)
            {
                MessageBox.Show($"Step {step.Number}: {step.Steps}");
                if (step.Ingredients != null && step.Ingredients.Count > 0)
                {
                    MessageBox.Show("Ingredients:");
                    foreach (var ingredient in step.Ingredients)
                    {
                        MessageBox.Show($"- {ingredient.Name}");
                    }
                }
            }
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }

        
}

