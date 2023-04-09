using Newtonsoft.Json.Linq;
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
    /// Логика взаимодействия для RecipePage.xaml
    /// </summary>
    public partial class RecipePage : Page
    {
        public SearchRecipe owner;
        public RecipePage()
        {
            InitializeComponent();
            ShowResult();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (owner != null)
                owner.RecipeNavigate.Content = null;
        }


        public async void ShowResult()
        {
            string apiKey = "5330ed77c83a4ecf965e38cdb7b80ac3";

            using var client = new HttpClient();
            var id = 715538; // замените на реальный идентификатор рецепта
            var response = await client.GetAsync($"https://api.spoonacular.com/recipes/{id}/information?apiKey={apiKey}");
            var content = await response.Content.ReadAsStringAsync();
            var recipe = JObject.Parse(content);

            foodName.Text = $"{recipe["title"]}";
            Servings.Text += $"{recipe["servings"]}";
            coockingTime.Text += $"{recipe["readyInMinutes"]}";





            string url = $"https://api.spoonacular.com/recipes/{id}/analyzedInstructions?apiKey={apiKey}";
            using var _client = new HttpClient();

            // Execute the API request and get the response as a JSON string
            string json = await _client.GetStringAsync(url);

            // Deserialize the JSON string into an object of type List<RecipeInstructions>
            List<RecipeInstructions> instructions = JsonConvert.DeserializeObject<List<RecipeInstructions>>(json);

            // Display the recipe name and the instructions
            //MessageBox.Show($"Instructions for {instructions[0].Name}:");
            foreach (var step in instructions[0].Steps)
            {
                StepsBlock.Text = $"Step {step.Number}: {step.Steps}";
                if (step.Ingredients != null && step.Ingredients.Count > 0)
                {
                    
                    foreach (var ingredient in step.Ingredients)
                    {
                        IngridientsBlock.Text += $"- {ingredient.Name}\n";
                    }
                }
            }
        }
    }
}

