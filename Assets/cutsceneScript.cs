using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cutsceneScript : MonoBehaviour
{
    List<string> CadetDialog = new List<string>()
    {
        "",
        "Hi, my name is Cadet",
        "I like ube cupcakes.",

    };


    List<List<string>> CorporalDialog = new List<List<string>>()
    {
           new List<string>()
         {
             "",
             "I'm a nice dude.",
             "I'm a neutral dude.",
             "I'm an asshole.",

         },

            new List<string>()
         {
             "",
             "Oh, yeah? I like plums.",
             "Lame.",
             "",

         },


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