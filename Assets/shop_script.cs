using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class shop_script : MonoBehaviour
{
    public gamestate_script gs;
    public class Upgrade{
        
        public bool purchased;
        public int price;
    
    }
 

    Dictionary<string, Upgrade> upgrade_dictionary = new Dictionary<string, Upgrade>()
    {
        {"phasebeam", new Upgrade()
            {
                purchased = false,
        
                price = 15

             }
        }
    };

    

    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<gamestate_script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buy_upgrade(string name){
        if (gs.money >= upgrade_dictionary[name].price) {
            gs.money -= upgrade_dictionary[name].price;
            upgrade_dictionary[name].purchased= true;
        }

    }
}
