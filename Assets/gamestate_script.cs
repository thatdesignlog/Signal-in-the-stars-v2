using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Analytics;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class gamestate_script : MonoBehaviour
{




    public class GreyPackageTypes
    {

        public float cute_percentage = .2f;
        public float boring_percentage = .8f;

        public List<string> boring_types_teaser = new List<string>()
        {
            "Property of Tesker Mining Ltd. Invoic ... ",
            "Property of Bhangra Agricultural Holdings. Invoice ...",
            "Product Sample: 12 grams of authentic Appian fresh ...",

        };

        public List<string> boring_types_body = new List<string>()
        {
            @"Property of Tesker Mining Ltd. Invoice attached

Please confirm receipt of the following items.  

10 kilos of raw aluminum (industrial sample)
10 kilos of raw palladium (industrial sample)
10 kilos of raw cobalt (industrial sample) 

Asset Tag No. 17278524391
",
            "Property of Bhangra Agricultural Holdings. Invoice ...",
            "Product Sample: 12 grams of authentic Appian fresh ...",

        };

        public List<string> cute_types = new List<string>()
        {
            "packages from individual households",
            "packages from Tuscaloosa Primary School",
            "packages from Peaceful Groves Retirement Commmunity",
        };

    }

    public GreyPackageTypes greyPackageTypes = new GreyPackageTypes();

    public class RedPackageTypes
    {

        public float boring_percentage = .12f;
        public float sad_percentage = .88f;

        



        public List<string> sad_types = new List<string>()
        {
            "Human ashes, contained in copper urn",
            "Human ashes, contained in iridium urn",
            "Data cube, containing image files",
            "Data cube, containing video and image files",
            "Data cube, containing text and image files",
            "Centauri currency in cash",
            "Organic substance, likely consumables",
            "Inorganic substance, unspecified",


        };

    }
    
    public RedPackageTypes redPackageTypes = new RedPackageTypes();




    public List<List<string>> postFlareDialogue = new List<List<string>>()
    {

        new List<string>()
         {
        "Trainer: Big flare today",
        "Trainee: So many packages lost ... ",
        "Trainer: Come on, gotta log what we reeled in. ",  },

         new List<string>()
         {
        "Trainer: Big flare today",
        "Trainee: It's oddly beautiful ... ",  },
    };


    public List<List<string>> dialogue = new List<List<string>>()
    {


        new List<string>()
        {
            "Trainee: So many packages...",
            "Trainer: And only one good shot",
            // STAGE DIRECTION: TRAINEE's EYES NARROW
            /* FINE ALEX YOU MADE YOUR POINT
            */
        } ,
        new List<string>()
        {
            "Trainer: Point seven seconds to charge",
            "Trainee: Earth seconds? Or Centurion seconds?",
            "Trainer: … ",
        } ,
         new List<string>()
        {
            "Trainer: My hangover pill’s wearing off.",
            "Trainee: … Good thing you don’t pilot passengers.",
        } ,
        new List<string>()
           {
            "Trainee: There are so many!",
            "Trainer: … I know",
          } ,
        new List<string>()
           {
            "Trainee: Are there more today than last time?",
            "Trainer: You stop tracking after a while.",
           } ,

    };

    public float round_time_limit;

    float time_elapsed = 0;
    public string status;

    public int packages_collected;

    public int red_packages_collected;
    public int grey_packages_collected;

    public int red_package_value;
    public int grey_package_value;

    private string[] filePaths;

    private GameObject solar_flare;
    private Vector3 solar_flare_original_pos;

    private ui_script uis;

    public int money;
    // public int phase_beam_cost;


    void Start()
    {

        status = "collecting packages";
        StartCoroutine(spawn_box());

        solar_flare = GameObject.FindGameObjectWithTag("solar flare");
        solar_flare_original_pos = solar_flare.transform.position;

        uis = FindObjectOfType<ui_script>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("time elapsed: "+ time_elapsed + "        round time limit: "+ round_time_limit);
        if (status == "collecting packages")
        {
            //Debug.Log(round_time_limit);
            if (time_elapsed < round_time_limit)
            {
                time_elapsed += Time.deltaTime;
                
            }
            else
            {
                time_elapsed = 0;
                status = "solar flare";
                uis.CancelDialogue();
                StartCoroutine(EndRound());

            }
        }
        if (status == "solar flare")
        {

            solar_flare.transform.Translate(-0.07f, 0, 0);
        }
    }



    IEnumerator EndRound()
    {
        yield return new WaitForSeconds(4);
        status = "reacting to solar flare";
        solar_flare.transform.position = solar_flare_original_pos;
        uis.StartCoroutine(uis.TriggerFlareDialogue());
    }


    public IEnumerator spawn_box()
    {
        if (status == "collecting packages")
        {
            Debug.Log(Time.deltaTime);
            float randomInterval = Random.Range(.1f, 1);
            yield return new WaitForSeconds(randomInterval);

            StartCoroutine(spawn_box());

            GameObject[] allPrefabs = Resources.LoadAll<GameObject>("packages/");
            //Debug.Log(allPrefabs[0]);


            GameObject package = Instantiate(allPrefabs[Random.Range(0, allPrefabs.Length)]);


            Vector3 viewportPoint = new Vector3(0, 1, Camera.main.nearClipPlane);
            Vector2 topLeft = Camera.main.ViewportToWorldPoint(viewportPoint);

            viewportPoint = new Vector3(0, 0, Camera.main.nearClipPlane);
            Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(viewportPoint);

            package.transform.position = new Vector3(topLeft.x, Random.Range(topLeft.y, bottomLeft.y), -1);
        }

    }


}