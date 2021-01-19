using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Controllers
{
    public class PlayerInvController : MonoBehaviour
    {
        private List<Item> inv = new List<Item>();

        public int invSize = 10;
        // Start is called before the first frame update
        void Start()
        {
            inv.Capacity = invSize;
        }

        public bool AddItem(Item item)
        {
            if (inv.Count < inv.Capacity)
            {
                inv.Add(item);
                Debug.Log("Added inv item successfully");
                return true;  
            }

            return false;

        }
    }
}
