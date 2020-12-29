using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class RequestTutorial : MonoBehaviour
{

    public GameObject DialogueRef;
    private GameController gc;
    private GameObject tenant;
    public GameObject arrowRef;
    private GameObject arrow;
    private GameObject requestButton;


    private List<string> requestTextStep1;
    private List<string> requestTextStep2;

    public GameObject fakeMenuBar;

    Tutorial tutorial;
    


    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        tutorial = GameObject.Find("Tutorial").GetComponent<Tutorial>();
        tenant = gc.tenants[0];
        arrow = Instantiate(arrowRef, new Vector3(0, 0, -3), this.transform.rotation) as GameObject;
        arrow.SetActive(false);
        SetupText();
        tenant.GetComponent<Tenant>().isDisabled = true;


    }

    private void SetupText()
    {

        //currencyCollectTextStep1[0] = currencyCollectTextStep1[0].Replace("\\n", "\n");

        requestTextStep1 = new List<string>();/*
        string path = "Assets/Resources/Dialogue/Request/requestDialogue1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                requestTextStep1.Add(line);
            }
        }
        */
        requestTextStep2 = new List<string>();/*
        path = "Assets/Resources/Dialogue/Request/requestDialogue2.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                requestTextStep2.Add(line);
            }
        }
        */

        TextAsset text = Resources.Load<TextAsset>("Dialogue/Request/requestDialogue1");
        requestTextStep1.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/Request/requestDialogue2");
        requestTextStep2.Add(text.text);


        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(requestTextStep1);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = tenant.GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(InitDialogueClose);
    }

    public void InitDialogueClose()
    {
        fakeMenuBar.gameObject.SetActive(true);
        tenant.GetComponent<Tenant>().CheckExclamationMark();
        if (arrow != null)
        {
            arrow.transform.position = new Vector3(-5.77f, 1.95f, 0);
            arrow.SetActive(true);
            //set arrow position based on tenant position
        }
        //activate request marker
        //tenant.GetComponent<Tenant>().isDisabled = false;
        tenant.AddComponent<ListenerScript>();
        tenant.GetComponent<ListenerScript>().SetOnClickMethod(OpenedTenantButtons);
        //tenant.GetComponent<Tenant>().isDisabled = false;
    }


    public void OpenedTenantButtons()
    {
        gc.tenantButtonHandler.DisableGiftButton();
        gc.tenantButtonHandler.DisabledExitButton();
        gc.tenantButtonHandler.requestButton.SetDisableClick(true);
        tenant.GetComponent<Tenant>().currencyCollectButton.SetDisableClick(true);
        gc.tenantButtonHandler.InitText("Penny Parakeet", "90/min");
        gc.tenantButtonHandler.transform.position = new Vector3(tenant.transform.position.x, tenant.transform.position.y, gc.tenantButtonHandler.transform.position.z); ;
        Destroy(tenant.GetComponent<ListenerScript>());
        if (arrow != null)
        {
            arrow.transform.position = new Vector3(-1.91f, 1.68f, -5);
            arrow.SetActive(true);
            //set arrow position again based on tenant buttons location
        }
        gc.tenantButtonHandler.requestButton.gameObject.AddComponent<ListenerScript>();
        gc.tenantButtonHandler.requestButton.gameObject.GetComponent<ListenerScript>().SetOnClickMethod(StartRequest);

        //tenant.GetComponent<Tenant>().isDisabled = false;
    }

    public void StartRequest()
    {
        if (arrow != null)
        {
            arrow.SetActive(false);
        }
        var tenantRef = tenant.GetComponent<Tenant>();
        tenantRef.OpenRequests();
        tenantRef.GetActiveRequest().GetComponent<RequestMenu>().cancelButton.enabled = false;
        Destroy(gc.tenantButtonHandler.requestButton.gameObject.GetComponent<ListenerScript>());
        //disable exit button on request
    }

    public void FinishRequest()
    {
        //Destroy(gc.tenantButtonHandler.requestButton.gameObject.GetComponent<ListenerScript>());
        //finish dialogue
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(requestTextStep2);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = tenant.GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(FinishTutorial);
    }

    public void FinishTutorial()
    {
        if (arrow != null)
        {
            Destroy(arrow);
        }
        gc.tenantButtonHandler.EnableButtons();
        gc.tenantButtonHandler.EnableExitButton();
        tutorial.NextStep();
        Destroy(gameObject);
    }

}
