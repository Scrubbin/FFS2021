using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        private AsyncOperation loadGame;

        public TextMeshProUGUI startText;
        public GameObject saveButton;

        private Scene activeScene;
        // Start is called before the first frame update
        void Start()
        {
            activeScene = SceneManager.GetActiveScene();
        }

        void OnEnable()
        {
            activeScene = SceneManager.GetActiveScene();
            if (activeScene.Equals(SceneManager.GetSceneByName("Game")))
            {
                startText.SetText("RESUME");
                saveButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                startText.SetText("NEW GAME");
                saveButton.GetComponent<Button>().interactable = false;
            }
        }

        public void StartGame()
        {
            //toggles between start and resume depending on context
            if (!activeScene.Equals(SceneManager.GetSceneByName("Game")))
            {
                DontDestroyOnLoad(this);
                Destroy(transform.Find("Main Camera").gameObject);
                loadGame = SceneManager.LoadSceneAsync("Loading");
                Time.timeScale = 1;
                

            }
            else
            {
                Time.timeScale = 1;
                gameObject.SetActive(false);
            }
        
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
