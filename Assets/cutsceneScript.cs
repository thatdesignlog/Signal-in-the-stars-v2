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


    public TextMeshProUGUI CadetDialogText;
    int CadetDialogIndex;

    public TextMeshProUGUI CorporalDialogOption1;
    public TextMeshProUGUI CorporalDialogOption2;
    public TextMeshProUGUI CorporalDialogOption3;
    int CorporalDialogOptionIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        clickHandler();
        Debug.Log(CadetDialogIndex);
        CadetDialogText.text = CadetDialog[CadetDialogIndex];
        CorporalDialogOption1.text = CorporalDialog[CorporalDialogOptionIndex][0];
        CorporalDialogOption2.text = CorporalDialog[CorporalDialogOptionIndex][1];
        CorporalDialogOption3.text = CorporalDialog[CorporalDialogOptionIndex][2];

    }

    void clickHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CadetDialogText.gameObject.activeSelf && !CorporalDialogOption1.gameObject.activeSelf)
            {
                NextDialog();
            }
        }
    }
    public void SelectDialog()
    {
      
        if (CadetDialogIndex < CadetDialog.Count-1)
        {
            CadetDialogText.gameObject.SetActive(true);
            CadetDialogIndex++;
            CorporalDialogOption1.gameObject.SetActive(false);
            CorporalDialogOption2.gameObject.SetActive(false);
            CorporalDialogOption3.gameObject.SetActive(false);
        }
        else
        {
            //add stuff for once all dialog is finished
        }

    }
    public void NextDialog()
    {
        if (CorporalDialogOptionIndex < CorporalDialog.Count - 1)
        {
            CadetDialogText.gameObject.SetActive(false);
            CorporalDialogOptionIndex++;
            CorporalDialogOption1.gameObject.SetActive(true);
            CorporalDialogOption2.gameObject.SetActive(true);
            CorporalDialogOption3.gameObject.SetActive(true);
        }
    }

}