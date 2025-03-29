using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_1DialogueScript : MonoBehaviour
{
    public class DialogueLine
    {
        
        public string line1;
        public string line2;
        public string line3;
        public string speaker;
        public bool isMonologue; 
    }
    public List<DialogueLine> VNscene_1_dialogue = new List<DialogueLine>()
    {
      new DialogueLine()
      {
          line1 = "Um...",
          line2 = "",
          line3 = "",
          speaker = "Corporal",
          
      },
       new DialogueLine()
      {
          line1 = "Well, technically it?s not satellite morning. Back at the Nairobi station it?s 7:27 am, though! And I don?t know about you, Corporal, but I?m still on Nairobi time!",
          line2 = "",
          line3 = "",
          speaker = "Cadet",
          isMonologue = false,
      },
        new DialogueLine()
      {
          line1 = "Look, um, I don't know how to say this but ... Have we met before?",
          line2 =  "Look, um, I don't know how to say this but ... Who the hell are you?",
          line3 = "",
          speaker = "Corporal",
          isMonologue = false,
      },
         new DialogueLine()
      {
          line1 = "Senior Cadet Aimee Kennington, Beta Branch two five seven!",
          line2 = "",
          line3 = "",
          speaker = "Cadet",
          isMonologue = false,
      },
           new DialogueLine()
      {
          line1 = "Um. Shouldn’t you be planetside, then? Taking classes?",
          line2 = "",
          line3 = "",
          speaker = "Corporal",
          isMonologue = false,
      },
    };

    //// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
