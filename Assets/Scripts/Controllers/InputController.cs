using UnityEngine;

namespace Controllers
{
    public class InputController : MonoBehaviour
    {
        private GameObject player;

        private GameObject UI;

        private GameObject menu;
        private GameObject playerInv;

        private bool buttonHeld;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player");
            UI = GameObject.Find("UI");
            menu = GameObject.Find("MenuCanvas");
            playerInv = GameObject.Find("Inventory");
            playerInv.SetActive(false);
            menu.SetActive(false);
            buttonHeld = false;
        }

        // Update is called once per frame
        void Update()
        {
            var menuInput = Input.GetAxisRaw("Menu");
            var invInput = Input.GetAxisRaw("Inventory");
            if (menuInput == 0 && invInput == 0)
            {
                buttonHeld = false;
            }
        
            if (menuInput > 0 && buttonHeld == false)
            {
                buttonHeld = true;
                int pause = menu.activeSelf ? 1 : 0;
                menu.SetActive(!menu.activeSelf);
                Time.timeScale = pause;

            } else if (invInput > 0 && buttonHeld == false)
            {
                buttonHeld = true;
                playerInv.SetActive(!playerInv.activeSelf);
                
            } 
        }
    }
}
