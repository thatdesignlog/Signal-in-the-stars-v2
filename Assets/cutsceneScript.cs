using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class cutsceneScript : MonoBehaviour { 

    // Scene #2 (Visual Novel Scene #1)
    bool unveiling_text;
    int remaining_characters_to_unveil;


    public TextMeshProUGUI CadetDialogText;

    public GameObject CorporalTextPanel;
    public GameObject CadetTextPanel;
    public TextMeshProUGUI CorporalDialogOption1;
    public TextMeshProUGUI CorporalDialogOption2;
    public TextMeshProUGUI CorporalDialogOption3;
    public TextMeshProUGUI MonologueDialog;
    public VN_1DialogueScript ds;
    VideoPlayer vp;
    GameObject visualNovel;
    mouseOverScript CorporalDialogOptionColor1;
    mouseOverScript CorporalDialogOptionColor2;
    mouseOverScript CorporalDialogOptionColor3;

    int dialogOptionIndex;

    // this determines which cutscene we're on
    public int videoIndex;

    // Start is called before the first frame update
    void Start()
    {
        CorporalDialogOptionColor1 = CorporalDialogOption1.gameObject.GetComponent<mouseOverScript>();
        CorporalDialogOptionColor2 = CorporalDialogOption2.gameObject.GetComponent<mouseOverScript>();
        CorporalDialogOptionColor3 = CorporalDialogOption3.gameObject.GetComponent<mouseOverScript>();
        //MonologueDialog = GameObject.FindGameObjectWithTag("monologueButton").GetComponent<TextMeshProUGUI>();
        ds = FindObjectOfType<VN_1DialogueScript>();
        vp = FindAnyObjectByType<VideoPlayer>();
        visualNovel = GameObject.FindGameObjectWithTag("visualNovel");
        
        Debug.Log(ds.VNscene_1_dialogue[dialogOptionIndex]);
        Debug.Log(ds.VNscene_1_dialogue[dialogOptionIndex].speaker);

    }

    // Update is called once per frame
    void Update()
    {
        visualNovel.gameObject.SetActive(false);
        if (videoIndex > 0)
        {
            vp.gameObject.SetActive(false);



            visualNovel.gameObject.SetActive(true);
            clickHandler();

            VN_1DialogueScript.DialogueLine currentDialogLine = ds.VNscene_1_dialogue[dialogOptionIndex];
            if (currentDialogLine.speaker == "Corporal")
            {
                CorporalDialogOption1.gameObject.SetActive(!string.IsNullOrEmpty(currentDialogLine.line1));
                CorporalDialogOption2.gameObject.SetActive(!string.IsNullOrEmpty(currentDialogLine.line2));
                CorporalDialogOption3.gameObject.SetActive(!string.IsNullOrEmpty(currentDialogLine.line3));
                CorporalDialogOption1.text = currentDialogLine.line1;
                CorporalDialogOption2.text = currentDialogLine.line2;
                CorporalDialogOption3.text = currentDialogLine.line3;
            }

            else if (currentDialogLine.speaker == "Cadet")
            {
                if (!unveiling_text) 
                {
                    CadetDialogText.text = currentDialogLine.line1;
                }
                else
                {

                    CadetDialogText.text = currentDialogLine.line1.Substring(0, currentDialogLine.line1.Length - remaining_characters_to_unveil);
                }
            }


        }



        

    }

    IEnumerator unveil_letter()
    {
        yield return new WaitForSeconds(.03f);
        if (remaining_characters_to_unveil > 0)
        {
            remaining_characters_to_unveil -= 1;
            StartCoroutine(unveil_letter());
        }
        else
        {
            unveiling_text = false;
        }
        
    }

    void clickHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CadetDialogText.gameObject.activeSelf && !CorporalDialogOption1.gameObject.activeSelf)
            {
                if (unveiling_text)
                {
                    unveiling_text = false;
                    remaining_characters_to_unveil = 0;
                }
                else
                {
                    NextDialog();
                }
                
            }
        }
    }
    public void SelectDialog()
    {
      
        if (dialogOptionIndex < ds.VNscene_1_dialogue.Count-1)
        {
            CorporalTextPanel.gameObject.SetActive(false);

            CadetTextPanel.gameObject.SetActive(true);
            CadetDialogText.gameObject.SetActive(true);
            dialogOptionIndex++;
            CorporalDialogOption1.gameObject.SetActive(false);
            CorporalDialogOption2.gameObject.SetActive(false);
            CorporalDialogOption3.gameObject.SetActive(false);

            unveiling_text = true;
            remaining_characters_to_unveil = ds.VNscene_1_dialogue[dialogOptionIndex].line1.Length;
            StartCoroutine(unveil_letter());
        }
        else
        {
            //add stuff for once all dialog is finished
        }

    }
    public void NextDialog()
    {
        if (dialogOptionIndex < ds.VNscene_1_dialogue.Count - 1)
        {
            CorporalTextPanel.gameObject.SetActive(true);
            CadetTextPanel.gameObject.SetActive(false);

            CadetDialogText.gameObject.SetActive(false);
            dialogOptionIndex++;

            CorporalDialogOption1.gameObject.SetActive(true);
            CorporalDialogOption2.gameObject.SetActive(true);
            CorporalDialogOption3.gameObject.SetActive(true);
            CorporalDialogOptionColor1.resetColor();
            CorporalDialogOptionColor2.resetColor();
            CorporalDialogOptionColor3.resetColor();

        }
    }

}
