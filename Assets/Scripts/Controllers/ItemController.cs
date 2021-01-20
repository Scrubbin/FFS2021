using System.Collections;
using System.Collections.Generic;
using Items;
using Newtonsoft.Json;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<Item, string> itemList = new Dictionary<Item, string>();
    void Start()
    {
        //load all items from json list
        //load all sprite assets from resources
        //attach sprites to item objects
        TextAsset jsonFoodList = Resources.Load("Items/Food/foodItems") as TextAsset;
        List<Food> tempFoodList = JsonConvert.DeserializeObject<List<Food>>(jsonFoodList.ToString());
        foreach (Food thisFood in tempFoodList)
        {
            string path = "Items/Food/" + thisFood.name;
            thisFood.icon = Resources.Load(path) as Sprite;
            thisFood.type = "food";
            itemList.Add(thisFood, thisFood.name);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
