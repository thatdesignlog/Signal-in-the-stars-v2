using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_1DialogueScript : MonoBehaviour
{
    public class DialogueLine
    {
        public string line;
        public string speaker;
        public bool isMonologue; 
    }
    public List<DialogueLine> VNscene_1_dialogue = new List<DialogueLine>()
    {
      new DialogueLine()
      {
          line = "",
          speaker = "Corporal",
          isMonologue = true,
      },

    };   
    public DialogueLine dialogueline = new DialogueLine()
    {
        line = "Well, technically it’s not satellite morning. Back at the Nairobi station it’s 7:27 am, though! And I don’t know about you, Corporal, but I’m still on Nairobi time!",
        speaker = "Cadet",
    };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
