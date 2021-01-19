using UnityEngine;

namespace Items
{
    public class Food : Item
    {
        public int foodVal;
        public int waterVal;

        Food(string itemName, string desc, int foodVal, int waterVal, Sprite worldSprite, Sprite invSprite)
        {
            this.itemName = itemName;
            this.desc = desc;
            this.foodVal = foodVal;
            this.waterVal = waterVal;
        }
    }
}
