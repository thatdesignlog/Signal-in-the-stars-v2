
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Script : MonoBehaviour
{
    public string button_type;
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
                tab_button.menu.SetActive(true);
            }
            else
            {
                tab_button.button.gameObject.GetComponent<Image>().sprite = tab_button.button_default;
                tab_button.menu.SetActive(false);
            }
        }
    }

    public void Reset()
    {
        
    }
}
