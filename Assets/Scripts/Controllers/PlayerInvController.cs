using System.Collections.Generic;
using Items;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    //PlayerInvController handles player inventory interactions and inventory UI
    //adding/removing inventory space
    //adding/removing items from inventory
    //interacting with items from inventory 
    public class PlayerInvController : MonoBehaviour
    {
        private List<Item> inv = new List<Item>();
        public GameObject playerInv;

        public int invSize = 36;

        public int invMin = 0;

        public int invMax = 36;

        public int panelsPerRow = 6;
        // Start is called before the first frame update
        void Start()
        {
            inv.Capacity = invSize;
            invMax = invSize;
            BuildInventoryUI();
        }
        
        //adjust inventory size by a given amount + triggers a UI resize
        //TODO - inventory size is currently hardcoded - revisit in later patch
        bool adjustSize(int amount)
        {
            int requestedSize = invSize + amount;
            if (requestedSize >= invMin && requestedSize <= invMax)
            {
                invSize += amount;
                inv.Capacity = invSize;
                return true;
                BuildInventoryUI();
            }

            return false;

        }
        
        //TODO Inventory UI size and row x and y offset for panels
        //TODO are currently hardcoded - need to address to be dynamic later
        //Builds Inventory UI with given number of rows and slots
        //Called whenever a inventory size change is required
        void BuildInventoryUI()
        {
            int currentPanels = 0;
            int totalPanels = 0;
            float invX = playerInv.GetComponent<RectTransform>().sizeDelta.x;
            float invY = playerInv.GetComponent<RectTransform>().sizeDelta.y;
            float xSpacing = invX / panelsPerRow;
            float ySpacing = invY / panelsPerRow;
            float startX = invX / 2 * -1;
            float startY = (invY / 2) - ySpacing;
            GameObject panel = new GameObject();
            RectTransform startTransform = panel.AddComponent<RectTransform>();
            startTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
            startTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
            panel.AddComponent<CanvasRenderer>();
            panel.AddComponent<Image>();
            
            //row positioning
            for (int i = 0; i < 6; i++)
            {
                float thisX = startX;
                float thisY = startY;
                GameObject row = new GameObject();
                row.name = "row";
                row.transform.SetParent(playerInv.transform);
                row.transform.SetPositionAndRotation(new Vector3(playerInv.transform.position.x - 70, playerInv.transform.position.y + thisY + 70, 0), Quaternion.identity);
                
                //slot positioning
                for (int j = 0; j < panelsPerRow; j++)
                {
                    thisX += xSpacing;
                    GameObject thisPanel = Instantiate(panel.gameObject);
                    thisPanel.name = "slot";
                    thisPanel.transform.SetParent(row.transform);
                    RectTransform thisTransform = thisPanel.GetComponent<RectTransform>();
                    thisTransform.SetPositionAndRotation(new Vector3(row.transform.position.x + thisX, row.transform.position.y, 0.0f), Quaternion.identity);
                    
                }
                //move starting point of next row
                startY -= ySpacing;
            }

            

        }

        //TODO
        public bool AddItem(Item item)
        {

            return false;

        }
    }
}
