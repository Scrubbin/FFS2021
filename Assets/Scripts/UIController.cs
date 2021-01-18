using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Set UI version
        GameObject version = this.gameObject.transform.GetChild(2).gameObject;
        version.GetComponent<TextMeshProUGUI>().SetText("v " + Application.version);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
