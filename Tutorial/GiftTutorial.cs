using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GiftTutorial : MonoBehaviour
{
    private GameController gc;
    public GameObject DialogueRef;
    private List<string> giftTutorialText1;
    private List<string> giftTutorialText2;
    private List<string> giftTutorialText3;
    private List<string> giftTutorialText4;
    private GameObject tenant;

    public GameObject fakeMenuBar;
    public GameObject fakeShopMenu;
    public GameObject fakeConfirmationMenu;
    public GameObject fakeInventoryMenu;

    public GameObject giftButton;
    public GameObject tenantExitButton;
    public GameObject requestButton;
    public Button confirmationButton;
    public Button rawMatsButton;


    public GameObject arrowRef;
    private GameObject arrow;
    public GameObject arrowImageRef;
    public GameObject arrowImage;
    public GameObject arrowImage2;
    public GameObject giftShopArrow;
    public GameObject confirmationGameObject;
    private Canvas confirmationCanvas;
    public GameObject shopMenuDialogue;

    public Button buyGiftButton;
    Tutorial tutorial;


    //disable currency collect/request/exit buttons
    //disable this
    public Button giftGiveExitButton;


    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        tutorial = GameObject.Find("Tutorial").GetComponent<Tutorial>();
        tenant = gc.tenants[0];
        SetupText();
        arrow = Instantiate(arrowRef, Vector3.zero, this.transform.rotation) as GameObject;
        arrow.SetActive(false);
        confirmationCanvas = confirmationGameObject.GetComponent<Canvas>();
        giftShopArrow.SetActive(false);
    }

    private void Update()
    {
        if (arrowImage != null && confirmationCanvas.enabled)
        {
            arrowImage.SetActive(false);
        }
    }

    private void SetupText()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();

        giftTutorialText1 = new List<string>();/*
        string path = "Assets/Resources/Dialogue/Gift/giftTutorialText1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                giftTutorialText1.Add(line);
            }
        }*/

        giftTutorialText2 = new List<string>();/*
        path = "Assets/Resources/Dialogue/Gift/giftTutorialText2.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                giftTutorialText2.Add(line);
            }
        }
        */
        giftTutorialText3 = new List<string>();/*
        path = "Assets/Resources/Dialogue/Gift/giftTutorialText3.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                giftTutorialText3.Add(line);
            }
        }
        */
        giftTutorialText4 = new List<string>();

        TextAsset text = Resources.Load<TextAsset>("Dialogue/Gift/giftTutorialText1");
        giftTutorialText1.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/Gift/giftTutorialText2");
        giftTutorialText2.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/Gift/giftTutorialText3");
        giftTutorialText3.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/Gift/giftTutorialText4");
        giftTutorialText4.Add(text.text);


        tempDialogue.AddText(giftTutorialText1);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = tenant.GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();

        var gift = gc.inventory.GetShopGifts();
        if (gift[0].name == "Cookies")
        {
            dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(InitDialogueClose);
        }
        else
        {
            dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(ConfirmGift);
        }
    }


    public void InitDialogueClose()
    {
        Debug.Log("init dialogue close");
        fakeMenuBar.gameObject.SetActive(true);
        if (arrow != null)
        {
            arrow.transform.position = new Vector3(-5.75f, -12f, -3);
            arrow.SetActive(true);
            //set arrow position based on tenant position
        }

    }

    public void OpenShopMenu()
    {
        arrow.SetActive(false);
        fakeShopMenu.SetActive(true);
        fakeMenuBar.gameObject.SetActive(false);
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(giftTutorialText2);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = tenant.GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(ActivateBuyGift);
    }

    public void ActivateBuyGift()
    {
        giftShopArrow.SetActive(true);
        buyGiftButton.enabled = true;
        //activate button
        //shopMenuDialogue.SetActive(false);
    }

    public void ClickGift()
    {
        giftShopArrow.SetActive(false);
        fakeConfirmationMenu.SetActive(true);
    }

    public void ConfirmGift()
    {
        //fakeConfirmationMenu.SetActive(false);
        fakeConfirmationMenu.GetComponent<Canvas>().enabled = false;
        fakeShopMenu.SetActive(false);
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(giftTutorialText3);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = tenant.GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(MidDialogueClose);
        gc.currencyManager.AddMoney(-600);
        gc.inventory.UpdateItemQuantity(0, 1);
        //remove money from player and add gift to inventory
    }

    public void MidDialogueClose()
    {
        //tenant.GetComponent<Tenant>().isDisabled = false;
        //move arrow over tenant
        if (arrow != null)
        {
            arrow.transform.position = new Vector3(-5.77f, 1.95f, 0);
            arrow.SetActive(true);
            //set arrow position based on tenant position
        }
        tenant.AddComponent<ListenerScript>();
        tenant.GetComponent<ListenerScript>().SetOnClickMethod(OpenedTenantButtons);
    }

    public void OpenedTenantButtons()
    {
        gc.tenantButtonHandler.DisableButtons();
        gc.tenantButtonHandler.DisabledExitButton();
        tenant.GetComponent<Tenant>().currencyCollectButton.SetDisableClick(true);
        gc.tenantButtonHandler.InitText("Penny Parakeet", "60/min");
        gc.tenantButtonHandler.transform.position = new Vector3(tenant.transform.position.x, tenant.transform.position.y, gc.tenantButtonHandler.transform.position.z); ;
        Destroy(tenant.GetComponent<ListenerScript>());
        if (arrow != null)
        {
            arrow.transform.position = new Vector3(-1.91f, 0.42f, -5);
            arrow.SetActive(true);
            //set arrow position again based on tenant buttons location
        }
        giftButton.AddComponent<ListenerScript>();
        giftButton.GetComponent<ListenerScript>().SetOnClickMethod(GiveGiftButton);
    }

    public void GiveGiftButton()
    {
        if (arrow != null)
        {
            Destroy(arrow);
            //set arrow position again based on tenant buttons location
        }
        giftGiveExitButton.enabled = false;
        gc.gameUI.inventoryMenu.OpenMenu(tenant.GetComponent<Tenant>());
        Destroy(giftButton.GetComponent<ListenerScript>());

        arrowImage = Instantiate(arrowImageRef, gc.gameUI.inventoryMenu.transform) as GameObject;
        RectTransform tempRect = arrowImage.GetComponent<RectTransform>();
        tempRect.localPosition = new Vector3(-88.5f, 184.2f, 0);
        tempRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40);
        tempRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 30);
        arrowImage.SetActive(true);

        arrowImage2 = Instantiate(arrowImageRef, gc.gameUI.inventoryMenu.confirmationCanvas.gameObject.transform) as GameObject;
        tempRect = arrowImage2.GetComponent<RectTransform>();
        tempRect.localPosition = new Vector3(-55.1f, -50, 0);
        tempRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40);
        tempRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 30);

        //add ListenerScript to confirm gift button
        //confirmationButton.onClick.AddListener(FinishGivingGift);
        rawMatsButton.enabled = false;
        gc.tenantButtonHandler.HideTenantButtons();
    }

    public void FinishGivingGift()
    {

        confirmationButton.onClick.RemoveListener(FinishGivingGift);
        rawMatsButton.enabled = true;
        if (arrow != null)
        {
            Destroy(arrow);
        }
        if(arrowImage != null)
        {
            Destroy(arrowImage);
        }
        if (arrowImage2 != null)
        {
            Destroy(arrowImage2);
        }
        giftGiveExitButton.enabled = true;
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(giftTutorialText4);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = tenant.GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(FinishTutorial);
    }

    public void FinishTutorial()
    {
        gc.tenantButtonHandler.EnableButtons();
        gc.tenantButtonHandler.EnableExitButton();
        tutorial.NextStep();
        Destroy(gameObject);
    }











}
