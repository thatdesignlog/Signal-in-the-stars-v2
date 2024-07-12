using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Script : MonoBehaviour
{
    public string Upgrade_Name;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>(); 
        // this is to get button in upgrade button function(?) 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
