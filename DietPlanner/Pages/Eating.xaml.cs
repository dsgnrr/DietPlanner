﻿using DietPlanner.Model;
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
            string apiKey = "5330ed77c83a4ecf965e38cdb7b80ac3";

            var httpClient = new HttpClient();
            string query = SearchFood.Text;
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

           

            string fatString = nutritionData.fat;
            food.Fats= Convert.ToInt32(Regex.Replace(fatString, @"[^\d]+", ""));
            int count_fats = 0;
            


            string carbs = nutritionData.carbs;
            food.Carbohydrates = Convert.ToInt32(Regex.Replace(carbs, @"[^\d]+", ""));
            int count_carb = 0;
           

            string protein = nutritionData.protein;
            food.Proteins = Convert.ToInt32(Regex.Replace(protein, @"[^\d]+", ""));
            int count_prot = 0;
           
            Foods.Add(food);
            for (int i = 0; i < Foods.Count; i++)
            {
                count_fats += Foods[i].Fats;
            }
            FatsProgress.Value = count_fats;
            FatsInfo.Text = count_fats + "g";

            for (int i = 0; i < Foods.Count; i++)
            {
                count_carb += Foods[i].Carbohydrates;
            }
            CarbohydratesProgress.Value = count_carb;
            CarbohydratesInfo.Text = count_carb + "g";
            for (int i = 0; i < Foods.Count; i++)
            {
                count_prot += Foods[i].Proteins;
            }
            ProteinsProgress.Value = count_prot;
            ProteinsInfo.Text = count_prot + "g";
            int count_kcal = 0;
            for (int i = 0; i < Foods.Count; i++)
            {
                count_kcal += Foods[i].Calories;
            }
            DailyConsumption.Text = count_kcal + "g";
        }
    }

    
}

