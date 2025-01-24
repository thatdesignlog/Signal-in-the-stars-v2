using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Script : MonoBehaviour
{
    public string Upgrade_Name;
    public Button button;
    Image image;
    tab_menu_script tms;

    // Start is called before the first frame update
    void Start()
    {
        tms = GameObject.FindAnyObjectByType<tab_menu_script>();
        image = gameObject.GetComponent<Image>();
        button = gameObject.GetComponent<Button>(); 
        // this is to get button in upgrade button function(?) 
    }

    // Update is called once per frame
    void Update()
    {
        //image.sprite = null;
    }

    public void SelectButton()
    {
        foreach (tab_menu_script.Tab_Button tab_button in tms.tab_button_list)
        {
            if (tab_button.button == image)
            {
                image.sprite = tab_button.button_pressed;
            }
        }
    }
}
