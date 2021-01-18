using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private AsyncOperation loadGame;

    public TextMeshProUGUI startText;

    private Scene activeScene;
    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        GameObject version = this.gameObject.transform.GetChild(0).gameObject;
        version.GetComponent<TextMeshProUGUI>().SetText("v " + Application.version);
    }

    void OnEnable()
    {
        activeScene = SceneManager.GetActiveScene();
        if (activeScene.Equals(SceneManager.GetSceneByName("Game")))
        {
            startText.SetText("RESUME");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
        //toggles between start and resume depending on context
        if (!activeScene.Equals(SceneManager.GetSceneByName("Game")))
        {
            DontDestroyOnLoad(this);
            loadGame = SceneManager.LoadSceneAsync("Game");
            Time.timeScale = 1;
            Destroy(gameObject.transform.GetChild(4).gameObject);
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
