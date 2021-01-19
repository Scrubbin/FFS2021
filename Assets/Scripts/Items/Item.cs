using Controllers;
using UnityEngine;

namespace Items
{
    public class Item : Interactable 
    {
        
        public string itemName;
        public string desc;
        public string type;

        public override void Interact()
        {
            bool invSuccess = AddToInv();
            if (invSuccess)
            {
                Destroy(gameObject);
            }
        }

        public bool AddToInv()
        {
            return inv.AddItem(this);
        }

    }
}
