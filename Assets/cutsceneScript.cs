using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cutsceneScript : MonoBehaviour
{
     List<string> cutsceneDialog = new List<string>()
    {
        "",
        "testString1",
        "testString2",
       
    };
    public TextMeshProUGUI DialogText;
    int DialogIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DialogText.text = cutsceneDialog[DialogIndex];
    }
    public void SelectDialog()
    {
        Debug.Log("test");
        DialogText.gameObject.SetActive(true);
        if (DialogIndex < cutsceneDialog.Count)
        {
            DialogIndex++;
        }
        else
        {
            //add stuff for once all dialog is finished
        }
        
    }
}
