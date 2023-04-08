using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietPlanner.Classes
{
    [Serializable]
    public class User
    {
        public int Gender { get; set; }
        public int Goal { get; set; }
        public Double Weight { get; set; }
        public Double Height { get; set; }
        public DateTime BirthDate { get; set; }

        public User()
        {
            Gender = 0;
            Goal = 0;
            Weight = 0;
            Height = 0;
            BirthDate = DateTime.Now;
        }
    }
}
