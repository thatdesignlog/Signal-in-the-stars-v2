using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cutsceneScript : MonoBehaviour { 

    // Scene #2 (Visual Novel Scene #1)
     List<string> CadetDialog = new List<string>()
    {
        "",
        "Well, technically it’s not satellite morning. Back at the Nairobi station it’s 7:27 am, though! And I don’t know about you, Corporal, but I’m still on Nairobi time!",
        "Would you like some juice?",
         "More for me, then! Are you more of a tea person?",
         "Senior Cadet Aimee Kennington, Beta Branch 257!", 
         "Usually, yes. But you hired me for the summer!",
         "Whoa, we hit another jump point!",
         "Good thing you bailed on the juice. I guess tequila shots followed by hyperspace is too much even for a vet.",
        "The orange flecks are paint. Mostly.",
        "No, no, no. You said you’d give me career advice for free, but that to be pleasant about it you’d need a shot.", 
        "We got to talking and realized I’d actually applied for an internship at your outpost!", 
        "That’s what you said last night!", 
        "You said that too! Well, your actual words were \"None of these daisy-eyed grasshoppers are gonna last a month anyway before they crack like the outer hull on the Europa 17, but at least that chick looks like she can handle her liquor. I’ll hire her.\"", 
        "Why did I accept an unpaid internship with an underfunded post office on the Outer Rim?", 
        "That’ll just have to remain a mystery for now, won’t it? I will say this, though. I’m looking forward to changing the universe together, Corporal. One day at a time.",
    };


     List<List<string>> CorporalDialog = new List<List<string>>() 
    {
           new List<string>()
         {
             "Um ...",
             "",
             "",
       
         },

            new List<string>()
         {
             "Right, tequila …. Okay, it’s coming back to me. We were at the grad ceremony …. My old squadmates and I decided to get drinks after … We walked into that place with the rusty door ….. ",
             "",
             "",

         },

           new List<string>()
            {
             "Right. My buddy Horaka and I saw a bunch of you dancing and bought drinks for everyone … we all started talking …. I said I’d give out career advice in exchange for free shots …. ",
             "",
             "",
       
         },

           new List<string>()
         {
             "Okay, that checks out. And then, er…. I’m actually drawing a blank here …. ",
             "",
             "",
       
         },

        new List<string>()
           {
             "Er. You did?  ",
             "",
             "",
       
         },
          new List<string>()
           {
             "Erm. Look, I’ve been really busy, didn’t have time to read all the cover letters … ",
             "",
             "",
       
         },
            new List<string>()
           {
             "…. Okay, it’s coming back. Look, I have a lot of questions, but there’s one I really want to get out of the way.",
             "",
             "",
       
         },
               new List<string>()
           {
             "Yes. That.",
             "Did we do anything, uh … did we do anything else last night?",
             "Can you possibly forgive me?",
       
         },

    };

    bool unveiling_text;
    int remaining_characters_to_unveil;


    public TextMeshProUGUI CadetDialogText;
    int CadetDialogIndex;

    public TextMeshProUGUI CorporalDialogOption1;
    public TextMeshProUGUI CorporalDialogOption2;
    public TextMeshProUGUI CorporalDialogOption3;
    mouseOverScript CorporalDialogOptionColor1;
    mouseOverScript CorporalDialogOptionColor2;
    mouseOverScript CorporalDialogOptionColor3;

    int CorporalDialogOptionIndex;

    // Start is called before the first frame update
    void Start()
    {
        CorporalDialogOptionColor1 = CorporalDialogOption1.gameObject.GetComponent<mouseOverScript>();
        CorporalDialogOptionColor2 = CorporalDialogOption2.gameObject.GetComponent<mouseOverScript>();
        CorporalDialogOptionColor3 = CorporalDialogOption3.gameObject.GetComponent<mouseOverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        clickHandler();
        Debug.Log(CadetDialogIndex);



        if (!unveiling_text)
        {
            CadetDialogText.text = CadetDialog[CadetDialogIndex];
        }
        else
        {

            CadetDialogText.text = CadetDialog[CadetDialogIndex].Substring(0, CadetDialog[CadetDialogIndex].Length - remaining_characters_to_unveil);        
        }
        



        CorporalDialogOption1.text = CorporalDialog[CorporalDialogOptionIndex][0];
        CorporalDialogOption2.text = CorporalDialog[CorporalDialogOptionIndex][1];
        CorporalDialogOption3.text = CorporalDialog[CorporalDialogOptionIndex][2];

    }

    IEnumerator unveil_letter()
    {
        yield return new WaitForSeconds(.2f);
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
      
        if (CadetDialogIndex < CadetDialog.Count-1)
        {
            CadetDialogText.gameObject.SetActive(true);
            CadetDialogIndex++;
            CorporalDialogOption1.gameObject.SetActive(false);
            CorporalDialogOption2.gameObject.SetActive(false);
            CorporalDialogOption3.gameObject.SetActive(false);

            unveiling_text = true;
            remaining_characters_to_unveil = CadetDialog[CadetDialogIndex].Length;
            StartCoroutine(unveil_letter());
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
            CorporalDialogOptionColor1.resetColor();
            CorporalDialogOptionColor2.resetColor();
            CorporalDialogOptionColor3.resetColor();

        }
    }

}
