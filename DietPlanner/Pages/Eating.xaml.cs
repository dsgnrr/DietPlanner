using DietPlanner.Model;
using DietPlanner.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для Eating.xaml
    /// </summary>
    /// 

    //ДЛЯ ПРИМЕРА
   

    public partial class Eating : Page
    {
        
        ObservableCollection<Food> Foods;
        Food food = new Food();
        public Eating()
        {
            InitializeComponent();
            Foods = new();
            DataContext = new SearchListModel { Foods = Foods };
            
        }


        class SearchResult
        {
            [JsonProperty("products")]
            public ProductResult[] Products { get; set; }
        }

        class ProductResult
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("nutrition")]
            public Nutrition Nutrition { get; set; }
        }

        class Nutrition
        {
            [JsonProperty("calories")]
            public double Calories { get; set; }

            [JsonProperty("fat")]
            public double Fat { get; set; }

            [JsonProperty("carbohydrates")]
            public double Carbohydrates { get; set; }

            [JsonProperty("protein")]
            public double Protein { get; set; }
        }

        

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string apiKey = "bf72237be72d44549d0acc3588ce6dfb";

            var httpClient = new HttpClient();
            string query = "apple";
            var response = await httpClient.GetAsync($"https://api.spoonacular.com/food/products/search?apiKey={apiKey}&query={query}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var searchResult = JsonConvert.DeserializeObject<SearchResult>(responseContent);

            if (searchResult.Products != null)
            {
                var product = searchResult.Products[0];
                Console.WriteLine($"Название продукта: {product.Title}");
                Console.WriteLine($"Идентификатор продукта: {product.Id}");

            }
            else
            {
                Console.WriteLine("Продукт не найден.");
            }
            dynamic recipe = searchResult.Products.FirstOrDefault();
            int id = recipe.Id;
            string url = $"https://api.spoonacular.com/recipes/{id}/nutritionWidget.json?apiKey={apiKey}";

            HttpResponseMessage response2 = await httpClient.GetAsync(url);
            response2 = await httpClient.GetAsync(url);


            dynamic responseBody = await response2.Content.ReadAsStringAsync();


            dynamic nutritionData = JsonConvert.DeserializeObject(responseBody);

            food.Calories = nutritionData.calories;
            food.Name = SearchFood.Text;

            //CarbohydratesProgress.Value = calories;

            string fatString = nutritionData.fat;
            food.Fats= Convert.ToInt32(Regex.Replace(fatString, @"[^\d]+", ""));
            FatsProgress.Value = food.Fats;
            
            string carbs = nutritionData.carbs;
            food.Carbohydrates = Convert.ToInt32(Regex.Replace(carbs, @"[^\d]+", ""));
            CarbohydratesProgress.Value = food.Carbohydrates;

            string protein = nutritionData.protein;
            food.Proteins = Convert.ToInt32(Regex.Replace(protein, @"[^\d]+", ""));
            ProteinsProgress.Value = food.Proteins;
            Foods.Add(food);
        }
    }

    
}

