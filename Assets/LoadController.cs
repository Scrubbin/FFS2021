using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadController : MonoBehaviour
{
    public float loadProgress;

    public AsyncOperation loadGame;

    public RectTransform loadingFG;
    // Start is called before the first frame update
    void Start()
    {
        loadGame = SceneManager.LoadSceneAsync("Game");
    }

    // Update is called once per frame
    void Update()
    {
        loadingFG.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp01(loadGame.progress) * 1000);
    }
}
