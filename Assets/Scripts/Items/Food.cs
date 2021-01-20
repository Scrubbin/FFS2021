using System;
using UnityEngine;

namespace Items
{
    public class Food : Item
    {
        public int foodVal;
        public int waterVal;

        public Food(string name, string desc, int foodVal, int waterVal)
        {
            this.name = name;
            this.desc = desc;
            this.foodVal = foodVal;
            this.waterVal = waterVal;
        }
    }
}
