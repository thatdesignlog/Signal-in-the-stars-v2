using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class tab_menu_script : MonoBehaviour
{
    GameObject currentTab;
    [Serializable]
    public class Tab_Button
    {
        public Image button;
        public GameObject menu;
        public Sprite button_pressed;
        public Sprite button_default;
    }

    //public Tab_Button tab_button = new Tab_Button();

    public List<Tab_Button> tab_button_list = new List<Tab_Button>();

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
