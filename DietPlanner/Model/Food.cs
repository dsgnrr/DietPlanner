using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Model
{
    public class Food
    {
        public string Name { get; set; }
        public int Carbohydrates { get; set; }
        public int Proteins { get; set; }
        public int Fats { get; set; }
        public int Calories { get; set; }
    }
}
