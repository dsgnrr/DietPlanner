using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для DailyStatistics.xaml
    /// </summary>
    public partial class DailyStatistics : Page
    {
        public DailyStatistics()
        {
            InitializeComponent();
            KCals();
            TodayDate.Text = DateTime.Now.ToShortDateString();
        }
        private async void KCals()
        {
            string apiKey = "bf72237be72d44549d0acc3588ce6dfb";

            
            string query = "chicken";

            
            string url = $"https://api.spoonacular.com/recipes/complexSearch?apiKey={apiKey}&query={query}";

            
            HttpClient client = new HttpClient();

            
            HttpResponseMessage response = await client.GetAsync(url);

            
            string responseBody = await response.Content.ReadAsStringAsync();

            
            dynamic data = JsonConvert.DeserializeObject(responseBody);

            
            dynamic recipe = data.results[0];

           
            int recipeId = recipe.id;

           
            url = $"https://api.spoonacular.com/recipes/{recipeId}/nutritionWidget.json?apiKey={apiKey}";

            
            response = await client.GetAsync(url);

           
            responseBody = await response.Content.ReadAsStringAsync();

            
            dynamic nutritionData = JsonConvert.DeserializeObject(responseBody);

            int calories = nutritionData.calories;


            EatenInfo.Text = calories.ToString();
            
            string fatString = nutritionData.fat;
            string carbs = nutritionData.carbs;
            string protein = nutritionData.protein;

            FatsInfo.Text = fatString.ToString();
            CarbohydratesInfo.Text = carbs.ToString();
            ProteinsInfo.Text = protein.ToString();
        }
    }
}
