using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FirstTenantTutorial : MonoBehaviour
{

    public GameObject DialogueRef;
    private List<string> firstTenantTextStep1;
    private List<string> firstTenantTextStep2;
    private List<string> firstTenantTextStep3;
    private List<string> firstTenantTextStep4;
    private List<string> firstTenantTextStep5;
    private List<string> firstTenantTextStep6;
    private GameController gc;
    private Tenant tenant;
    public GameObject arrowRef;
    private GameObject arrow;
    public GameObject arrowImageRef;
    private GameObject arrowImage;
    public GameObject purchaseScreenRef;
    private bool dialogueClosed;
    public GameObject adArrow;

    private bool startClickedTenant = false;

    Tutorial tutorial;
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        tutorial = GameObject.Find("Tutorial").GetComponent<Tutorial>();
        tenant = gc.tenants[1].GetComponent<Tenant>();
        SetupText();
        arrow = Instantiate(arrowRef, Vector3.zero, this.transform.rotation) as GameObject;
        arrow.SetActive(false);

    }

    private void SetupText()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();

        firstTenantTextStep1 = new List<string>();/*
        string path = "Assets/Resources/Dialogue/FirstTenant/firstTenantTextStep1.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                firstTenantTextStep1.Add(line);
            }
        }
        */
        firstTenantTextStep2 = new List<string>();/*
        path = "Assets/Resources/Dialogue/FirstTenant/firstTenantTextStep2.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                firstTenantTextStep2.Add(line);
            }
        }
        */
        firstTenantTextStep3 = new List<string>();/*
        path = "Assets/Resources/Dialogue/FirstTenant/firstTenantTextStep3.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                firstTenantTextStep3.Add(line);
            }
        }
        */
        firstTenantTextStep4 = new List<string>();/*
            path = "Assets/Resources/Dialogue/FirstTenant/firstTenantTextStep4.txt";

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {

                    line = line.Replace("\\n", "\n");
                    firstTenantTextStep4.Add(line);
                }
            }
            */
        firstTenantTextStep5 = new List<string>();/*
        path = "Assets/Resources/Dialogue/FirstTenant/firstTenantTextStep5.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                firstTenantTextStep5.Add(line);
            }
        }
        */
        firstTenantTextStep6 = new List<string>();/*
        path = "Assets/Resources/Dialogue/FirstTenant/firstTenantTextStep6.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                firstTenantTextStep6.Add(line);
            }
        }
        */
        TextAsset text = Resources.Load<TextAsset>("Dialogue/FirstTenant/firstTenantTextStep1");
        firstTenantTextStep1.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/FirstTenant/firstTenantTextStep2");
        firstTenantTextStep2.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/FirstTenant/firstTenantTextStep3");
        firstTenantTextStep3.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/FirstTenant/firstTenantTextStep4");
        firstTenantTextStep4.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/FirstTenant/firstTenantTextStep5");
        firstTenantTextStep5.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/FirstTenant/firstTenantTextStep6");
        firstTenantTextStep6.Add(text.text);



        tempDialogue.AddText(firstTenantTextStep1);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(InitDialogueClose);
    }

    public void Update()
    {
        if (dialogueClosed && tenant.GetIsUnlocked())
        {
            dialogueClosed = false;
            PurchaseCompletePT1();
        }

        if (startClickedTenant && tenant.GetActiveRequest() != null)
        {
            ClickedTenant();
            startClickedTenant = false;
        }
    }

    public void InitDialogueClose()
    {
        dialogueClosed = true;

        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x , tenant.gameObject.transform.position.y, gc.mainCam.transform.position.z);

        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(firstTenantTextStep2);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(RenavateDialogueClose);

        /*
        if (arrow != null)
        {
            arrow.SetActive(true);
            arrow.transform.position = gc.tenants[1].GetComponent<Tenant>().dialogueSpawnSpot.transform.position;
            //set arrow position based on tenant position
        }
        tenant.gameObject.AddComponent<ListenerScript>();
        tenant.GetComponent<ListenerScript>().SetOnClickMethod(ClickedTenant);

        tenant.isDisabled = false;
        */
    }

    public void RenavateDialogueClose() {
         if (arrow != null)
        {
            arrow.SetActive(true);
            Vector3 tempPos = tenant.transform.position;
            tempPos.z = -1;
            tempPos.y += 2;
            arrow.transform.position = tempPos;
            //set arrow position based on tenant position
        }
        tenant.gameObject.AddComponent<ListenerScript>();
        //tenant.GetComponent<ListenerScript>().SetOnClickMethod(ClickedTenant);
        tenant.isDisabled = false;
        startClickedTenant = true;
    }

    public void ClickedTenant()
    {
        if (arrow != null)
        {
            arrow.SetActive(false);

        }
        //gc.tenants[1].GetComponent<Tenant>().GetActiveRequest();
        Debug.Log("active request: " + tenant.GetActiveRequest());
        Destroy(tenant.GetComponent<ListenerScript>());
        arrowImage = Instantiate(arrowImageRef, tenant.GetActiveRequest().transform) as GameObject;
        arrowImage.GetComponent<RectTransform>().localPosition = new Vector3(-55, -55, 0);
        tenant.GetActiveRequest().GetComponent<TenantPurchaseScreen>().noButton.enabled = false;

        /*
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        var activeScreen = Instantiate(purchaseScreenRef, screenCenter, transform.rotation) as GameObject;
        TenantPurchaseScreen purchaseScreen = activeScreen.GetComponent<TenantPurchaseScreen>();
        purchaseScreen.UpdateText(tenant.GetComponent<Tenant>().unlockCost);
        purchaseScreen.AttachTenant(tenant.GetComponent<Tenant>());
        purchaseScreen.yesButton.gameObject.AddComponent<ListenerScript>();
        purchaseScreen.yesButton.GetComponent<ListenerScript>().SetOnClickMethod(PurchaseComplete);
        gc.tenantButtonHandler.transform.position =
            new Vector3(10000, 10000, gc.tenantButtonHandler.transform.position.z);

    */
    }

    public void PurchaseCompletePT1()
    {
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x, gc.tenants[0].gameObject.transform.position.y, gc.mainCam.transform.position.z);
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(firstTenantTextStep3);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(PurchaseCompletePT2);
        tenant.isDisabled = true;

    }

    public void PurchaseCompletePT2()
    {
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x, tenant.gameObject.transform.position.y, gc.mainCam.transform.position.z);
        GameObject dialogue = Instantiate(DialogueRef, tenant.dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(firstTenantTextStep4);
        tempDialogue.SetAudioClipType(2);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = tenant.GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(PurchaseCompletePT3);

    }


    public void PurchaseCompletePT3()
    {
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x, gc.tenants[0].gameObject.transform.position.y, gc.mainCam.transform.position.z);
        Destroy(tenant.GetComponent<ListenerScript>());
        tenant.isDisabled = true;
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(firstTenantTextStep5);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(StartAdSection);
        gc.collectButton.enabled = true;
    }

    public void StartAdSection()
    {
        /*
        if (arrow != null)
        {
            arrow.transform.position = new Vector3(-12.45f, 2, 0);
            arrow.SetActive(true);
            //set arrow position based on tenant position
        }*/
        adArrow.SetActive(true);
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(firstTenantTextStep6);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(FinishTutorial);
    }

    public void FinishTutorial()
    {
        if (arrow != null)
        {
            Destroy(arrow);
        }
        Destroy(adArrow);
        gc.tenants[0].GetComponent<Tenant>().currencyCollectButton.disableClick = false;
        gc.tenantButtonHandler.EnableButtons();
        gc.tenantButtonHandler.EnableExitButton();
        tutorial.NextStep();
        Destroy(gameObject);
    }



}
