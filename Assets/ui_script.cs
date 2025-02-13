using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ui_script : MonoBehaviour
{
    public gamestate_script gs;
    public TextMeshProUGUI packages_collected_text;
    public TextMeshProUGUI red_packages_count_text;
    public TextMeshProUGUI grey_packages_count_text;

    public TextMeshProUGUI red_packages_total_value_text;
    public TextMeshProUGUI grey_packages_total_value_text;

    public TextMeshProUGUI mail_template;
    public GameObject check_mail_menu;
    public GameObject inbox;
    public GameObject message_content_menu;
    public TextMeshProUGUI message_content_text;
    public Button makePriorityButton;


    public TextMeshProUGUI trainee_text;
    public TextMeshProUGUI trainer_text;


    public GameObject grey_package_text;
    public GameObject red_package_text;

    public GameObject postRoundMenu; 

    public TextMeshProUGUI money_display_endgame;
    public TextMeshProUGUI money_display_shop;
    //public TextMeshProUGUI money_count;
    
    public GameObject shop_menu;
    public Button buy_upgrade;


    public List<string> mail_list = new List<string>();
    




    void Start()
    {
        gs = FindObjectOfType<gamestate_script>();
        StartCoroutine(update_dialogue());

        PopulatePackageTypes();

        for (int i = 0; i < 3; i++)
        {
            GameObject new_mail = Instantiate(mail_template.transform.parent.gameObject) as GameObject;
            new_mail.transform.parent = inbox.transform;

            new_mail.transform.localScale = mail_template.transform.parent.localScale;
            new_mail.transform.position = mail_template.transform.parent.position;
            new_mail.transform.position += new Vector3(0, i * -150, 0);
            new_mail.GetComponentInChildren<TextMeshProUGUI>().text = gs.greyPackageTypes.interesting_types_teaser[i];
            string content_string = gs.greyPackageTypes.interesting_types_body[i];
            new_mail.GetComponent<Button>().onClick.AddListener(delegate {ReadMail(content_string); });
        }
        mail_template.gameObject.SetActive(false);



        //trainee_text.gameObject.SetActive(false);
        //trainer_text.gameObject.SetActive(false);
    }

    
    // Update is called once per frame
    void Update()
    {
        Update_Text();
    }

   
    void Update_Text()
    {
        
        //packages_collected_text.text = gs.packages_collected.ToString();
        grey_packages_count_text.text = "x"+ gs.grey_packages_collected.ToString();
        red_packages_count_text.text = "x" + gs.red_packages_collected.ToString();

        // this is hard coded to save time, but will later be made dynamic
        // currently package data are stored in package scripts, not gamestate script, which makes this difficult
        grey_packages_total_value_text.text = (gs.grey_packages_collected * 1).ToString();
        red_packages_total_value_text.text = (gs.red_packages_collected * 2).ToString();

        money_display_endgame.text = gs.money.ToString();
        money_display_shop.text = gs.money.ToString();
       




    }

    public void ContinueToShopMenu(){
        postRoundMenu.SetActive(false);
        shop_menu.SetActive(true);

        // if (gs.money < gs.phase_beam_cost){
        //     buy_upgrade.interactable = false;
        // }

    }
   
   public void startNextRound(){

        gs.status = "collecting packages";
        gs.StartCoroutine(gs.spawn_box());
        postRoundMenu.SetActive(false);


   }


    public void CancelDialogue()
    {
        StopAllCoroutines();
        trainee_text.gameObject.SetActive(false);
        trainer_text.gameObject.SetActive(false);
    }

    public IEnumerator TriggerFlareDialogue()
    {
        List<string> dialogue = gs.postFlareDialogue[Random.Range(0, gs.postFlareDialogue.Count)];

        foreach (string s in dialogue)
        {



            if (s.Substring(0, 8) == "Trainer:")
            {
                trainer_text.gameObject.SetActive(true);
                trainee_text.gameObject.SetActive(false);
                trainer_text.text = s.Substring(8);
            }
            else if (s.Substring(0, 8) == "Trainee:")
            {
                trainee_text.gameObject.SetActive(true);
                trainer_text.gameObject.SetActive(false);
                trainee_text.text = s.Substring(8);
            }
            //yield return new WaitForSeconds(3);
            yield return new WaitForSeconds(0);
        }

        trainee_text.gameObject.SetActive(false);
        trainer_text.gameObject.SetActive(false);

        //yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(0);
        gs.status = "post-round menu";
        SetActivePostRoundMenu(true);
    }

    public IEnumerator update_dialogue()
    {


        List<string> dialogue = gs.dialogue[Random.Range(0, gs.dialogue.Count)];
        
        foreach (string s in dialogue)
        {



            if (s.Substring(0,8) == "Trainer:")
            {
                trainer_text.gameObject.SetActive(true);
                trainee_text.gameObject.SetActive(false);
                trainer_text.text = s.Substring(8);
            }
            else if (s.Substring(0, 8) == "Trainee:")
            {
                trainee_text.gameObject.SetActive(true);
                trainer_text.gameObject.SetActive(false);
                trainee_text.text = s.Substring(8);
            }
            yield return new WaitForSeconds(3);


        }

        trainee_text.gameObject.SetActive(false);
        trainer_text.gameObject.SetActive(false);

        yield return new WaitForSeconds(6);
        StartCoroutine(update_dialogue());

    }

    public void SetActivePostRoundMenu(bool active)
    {
        postRoundMenu.SetActive(active);
        PopulatePackageTypes();
    }

    public void PopulatePackageTypes()
    {

        for(int i=1; i < 5; i++){

            //this code disabled until we integrate it with the new menu

            
            //int RandomNumber = Random.Range(0,gs.greyPackageTypes.boring_types.Count);

            /*
            GameObject Menu = Instantiate(grey_package_text);
            Menu.transform.parent = grey_package_text.transform.parent;
            Menu.transform.position = grey_package_text.transform.position + new Vector3(0,i* -55,0);
            Menu.GetComponent<TextMeshProUGUI>().text = gs.greyPackageTypes.boring_types[RandomNumber];
            */
        }        
    }

    public void ReadMail(string content)
    {
        inbox.SetActive(false);
        message_content_menu.SetActive(true);
        message_content_text.text = content;
        makePriorityButton.GetComponentInChildren<TextMeshProUGUI>().text = "Make Express Mail for +" + Random.Range(1,9)+ " money";
    }
    public void GoBackToInbox()
    {
        inbox.SetActive(true);
        message_content_menu.SetActive(false);
    }


    static int ExtractNumber(string input)
    {
        Match match = Regex.Match(input, @"\d+"); // Find the first sequence of digits
        return match.Success ? int.Parse(match.Value) : 0; // Convert to int or return 0 if no match
    }
    public void MakePriority()
    {
        makePriorityButton.interactable = false;
        int money_amount = ExtractNumber(makePriorityButton.GetComponentInChildren<TextMeshProUGUI>().text);
        Debug.Log(money_amount);
        gs.money += money_amount;
    }

}
