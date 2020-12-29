using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InitialStepTutorial : MonoBehaviour
{

    public GameObject DialogueRef;
    private List<string> currencyCollectTextStep1;
    private List<string> currencyCollectTextStep2;
    private GameController gc;
    private GameObject tenant;
    public GameObject arrowRef;
    private GameObject arrow;
    private GameObject collectButton;

    Tutorial tutorial;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        tutorial = GameObject.Find("Tutorial").GetComponent<Tutorial>();
        tenant = gc.tenants[0];
        //tenant.GetComponent<Tenant>().isDisabled = false;
        collectButton = tenant.GetComponent<Tenant>().currencyCollectButton.gameObject;
        SetupText();
        arrow = Instantiate(arrowRef, new Vector3(0, 0, -3), this.transform.rotation) as GameObject;
        arrow.SetActive(false);
        


    }

    private void SetupText()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        //currencyCollectTextStep1[0] = currencyCollectTextStep1[0].Replace("\\n", "\n");

        currencyCollectTextStep1 = new List<string>();
        string path = "Assets/Resources/Dialogue/InitStep/initDialogue1.txt";

        TextAsset text = Resources.Load<TextAsset>("Dialogue/InitStep/initDialogue1"); 
        /*
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                currencyCollectTextStep1.Add(line);
            }
        }*/

        currencyCollectTextStep1.Add(text.text);
        /*
        currencyCollectTextStep2 = new List<string>();
        path = "Assets/Resources/Dialogue/InitStep/initDialogue2.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                currencyCollectTextStep2.Add(line);
            }
        }*/
        tempDialogue.AddText(currencyCollectTextStep1);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = tenant.GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(InitDialogueClose);
    }

    public void InitDialogueClose()
    {
        if (arrow != null)
        {
            arrow.transform.position = new Vector3(-1.9f, 3, -3);
            arrow.SetActive(true);
            //set arrow position based on tenant position
        }
        collectButton.AddComponent<ListenerScript>();
        collectButton.GetComponent<ListenerScript>().SetOnClickMethod(CollectedCurrency);
        //collectButton.GetComponent<Renderer>().material.SetFloat("_Slide", 0.1f);
        tenant.GetComponent<Tenant>().heldMoney = 720;
    }

    public void OpenedTenantButtons()
    {
        Destroy(tenant.GetComponent<ListenerScript>());
        if (arrow != null)
        {
            //set arrow position again based on tenant buttons location
        }
        collectButton.AddComponent<ListenerScript>();
        collectButton.GetComponent<ListenerScript>().SetOnClickMethod(CollectedCurrency);
    }

    public void CollectedCurrency()
    {
        if (arrow != null)
        {
            Destroy(arrow);
        }
        gc.currencyManager.CollectMoney(tenant.GetComponent<Tenant>(), 720);
        tenant.GetComponent<Tenant>().heldMoney = 0;
        Destroy(collectButton.GetComponent<ListenerScript>());
        FinishTutorial();


    }

    public void FinishTutorial()
    {
        tenant.GetComponent<Tenant>().currencyCollectButton.enabled = false;

        gc.tenantButtonHandler.EnableGiftButton();
        gc.tenantButtonHandler.EnableRequestButton();
        tutorial.NextStep();
        Destroy(gameObject);
    }





}
