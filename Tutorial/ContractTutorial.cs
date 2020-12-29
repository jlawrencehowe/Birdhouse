using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ContractTutorial : MonoBehaviour
{
    public GameObject fakeMenuBar;
    public Button openShopButton;
    public GameObject fakeShopMenu;
    public Button openContractsButton;
    public GameObject fakeContractMenu;
    public Button buyButton;
    public GameObject fakeBuyConfirmation;
    public Button confirmBuyButton;
    public GameObject collectButton;

    private List<string> contractDialogueInit;
    private List<string> openContractShopDialogue;
    private List<string> openContractShopDialogue2;
    private List<string> examineButtonDialogue;
    private List<string> openExamineDialogue;
    private List<string> contractCollectMatDialogue;
    private List<string> contractDialogueFinal;

    private GameController gc;
    public GameObject DialogueRef;
    public GameObject arrowRef;
    private GameObject arrow;
    public GameObject contractArrow;

    public GameObject contractDialogue;
    public GameObject contractDialogue2;
    public ExamineButton examineButton;
    public GameObject examineObject;
    private bool examineDialogueFinished;

    public TextMeshProUGUI contractLevelText;
    Tutorial tutorial;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        tutorial = GameObject.Find("Tutorial").GetComponent<Tutorial>();

        openShopButton.onClick.RemoveAllListeners();
        openContractsButton.onClick.RemoveAllListeners();
        buyButton.onClick.RemoveAllListeners();
        confirmBuyButton.onClick.RemoveAllListeners();
        SetupText();
        openShopButton.onClick.AddListener(OpenShopMenu);
        openContractsButton.onClick.AddListener(OpenContractMenu);
        buyButton.onClick.AddListener(ClickItem);
        buyButton.enabled = false;
        confirmBuyButton.onClick.AddListener(BuyItem);
        arrow = Instantiate(arrowRef, new Vector3(0, 0, -3), this.transform.rotation) as GameObject;
        arrow.SetActive(false);
        examineButton.enabled = false;
        contractArrow.SetActive(false);
    }

    private void SetupText()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();

        contractDialogueInit = new List<string>();/*
        string path = "Assets/Resources/Dialogue/Contract/contractDialogueInit.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                contractDialogueInit.Add(line);
            }
        }
        */

        examineButtonDialogue = new List<string>();/*
        path = "Assets/Resources/Dialogue/Contract/examineButtonDialogue.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                examineButtonDialogue.Add(line);
            }
        }
        */
        openExamineDialogue = new List<string>();
        /*
        path = "Assets/Resources/Dialogue/Contract/openExamineDialogue.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                openExamineDialogue.Add(line);
            }
        }
        */



        contractCollectMatDialogue = new List<string>();/*
        path = "Assets/Resources/Dialogue/Contract/contractCollectMatDialogue.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                contractCollectMatDialogue.Add(line);
            }
        }*/
        /*
        contractDialogueFinal = new List<string>();
        path = "Assets/Resources/Dialogue/Contract/contractDialogueFinal.txt";

        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                line = line.Replace("\\n", "\n");
                contractDialogueFinal.Add(line);
            }
        }
        */
        openContractShopDialogue = new List<string>();
        openContractShopDialogue2 = new List<string>();

        TextAsset text = Resources.Load<TextAsset>("Dialogue/Contract/contractDialogueInit");
        contractDialogueInit.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/Contract/examineButtonDialogue");
        examineButtonDialogue.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/Contract/openExamineDialogue");
        openExamineDialogue.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/Contract/contractCollectMatDialogue");
        contractCollectMatDialogue.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/Contract/openContractShopDialogue");
        openContractShopDialogue.Add(text.text);
        text = Resources.Load<TextAsset>("Dialogue/Contract/openContractShopDialogue2");
        openContractShopDialogue2.Add(text.text);
        tempDialogue.AddText(contractDialogueInit);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(InitDialogueClose);
    }

    void Update()
    {
        if (examineObject.activeSelf)
        {
            examineDialogueFinished = true;
        }
        if(!examineObject.activeSelf && examineDialogueFinished)
        {
            CloseExamineDialogueEnd();
            examineDialogueFinished = false;
        }
    }

    public void InitDialogueClose()
    {
        fakeMenuBar.SetActive(true);
        if (arrow != null)
        {
            arrow.transform.position = new Vector3(-5.75f, -12f, -3);
            arrow.SetActive(true);
            //set arrow position based on tenant position
        }
        //move arrow
    }


    public void OpenShopMenu()
    {
        fakeShopMenu.SetActive(true);
        arrow.SetActive(false);
        openShopButton.enabled = false;
        //move arrow
    }

    public void OpenContractMenu()
    {
        Debug.Log("Contract Test");
        fakeShopMenu.GetComponent<Canvas>().enabled = false;
        fakeContractMenu.SetActive(true);
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(openContractShopDialogue);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(ActivateBuyItem);
    }

    public void ActivateBuyItem()
    {
        contractArrow.SetActive(true);
        contractDialogue.SetActive(false);
        buyButton.enabled = true;
    }

    public void ClickItem()
    {

        contractArrow.SetActive(false);
        fakeBuyConfirmation.SetActive(true);
        buyButton.enabled = false;
    }

    public void BuyItem()
    {
        fakeBuyConfirmation.GetComponent<Canvas>().enabled = false;
        //contractDialogue2.SetActive(true);
        //fakeContractMenu.SetActive(false);
        //fakeMenuBar.SetActive(false);
        gc.currencyManager.AddMoney(-gc.gameUI.upgradeMenu.upgrade1BaseCosts);
        int level = gc.savedData.GetRawMat1UpgradeLvl();
        gc.savedData.SetRawMat1UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat1PerSec();
        //ClosingDialogue();
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(openContractShopDialogue2);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(CloseExamineDialogueStart);
        contractLevelText.text = "x1";
        openShopButton.onClick.RemoveAllListeners();
    }

    public void CloseExamineDialogueStart()
    {
        //enable examine button

        examineButton.enabled = true;
        contractDialogue2.SetActive(false);
        contractArrow.SetActive(true);
        contractArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3(-120.6f, 148, 0);


        //move arrow
    }

    public void ClickExamineButton()
    {
        //open examine object
        examineObject.SetActive(true);
        //enable dialogue
        contractArrow.SetActive(false);

        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(openExamineDialogue);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(CloseExamineDialogueEnd);
        
    }

    public void CloseExamineDialogueEnd()
    {
        //close dialogue and examine object
        examineObject.SetActive(false);
        //close canvas
        fakeContractMenu.SetActive(false);
        //start ready to collect mat
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(contractCollectMatDialogue);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        tempDialogue.portrait.sprite = gc.tenants[0].GetComponent<Tenant>().portraitSprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(ReadyToCollectMat);
    }

    public void ReadyToCollectMat()
    {
        gc.deliveryBird.heldRawMat1 = 1;
        gc.deliveryBird.maxEveryItem = 10;
        collectButton.AddComponent<ListenerScript>();
        collectButton.GetComponent<ListenerScript>().SetOnClickMethod(CollectRawMat);
        collectButton.GetComponent<Renderer>().material.SetFloat("_Slide", 1 / 10);
        arrow.SetActive(true);
        //move arrow
        arrow.transform.position = new Vector3(-1.93f, -5, -3);
    }

    //force player to collect raw material
    public void CollectRawMat()
    {
        gc.deliveryBird.UpdateRawMat1PerSec();
        Destroy(arrow);
        Destroy(collectButton.GetComponent<ListenerScript>());
        FinishTutorial();

    }

    public void ClosingDialogue()
    {
        GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        tempDialogue.AddText(contractDialogueFinal);
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
        fakeMenuBar.SetActive(false);
        fakeShopMenu.SetActive(false);
        fakeContractMenu.SetActive(false);
        fakeBuyConfirmation.SetActive(false);
        tutorial.NextStep();
        Destroy(gameObject);
    }
}
