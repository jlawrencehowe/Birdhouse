using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    #region Old Tutorial
    /*
    //needs to be activated after load happens
    //set arrow position to UI elements but converted to local space

    GameController gc;
    bool firstTime;
    bool hasCompleted;
    public GameObject DialogueRef;
    public GameObject pointingArrow;
    private GameObject activeArrow;
    private bool readyForNextTut;

    public List<string> currencyCollectTextStep1;
    public List<string> currencyCollectTextStep3;


    public List<string> firstContractTextStep1;
    public List<string> firstContractTextStep5;

    TutorialButton.ButtonMethod bm;

    public enum TutorialState
    {
        currencyCollect, firstContract, firstGift, firstRequest, buyTenant, finalNotes

    }
    public TutorialState tutorialState;
    public float tutorialStep;

    // Start is called before the first frame update
    void Start()
    {
        tutorialStep = 1;
        if (hasCompleted)
        {
            Destroy(gameObject);
        }
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        //TutorialButton tb = gc.tenants[0].GetComponent<Tenant>().currencyCollectButton.gameObject.AddComponent<TutorialButton>();
        //TutorialButton.ButtonMethod bm = StartNextTut;
        //tb.SetActiveMethod(bm);
        bm = StartNextTut;

    }

    public void LoadState(bool firstTime, bool hasCompleted, TutorialState currentTutorial)
    {
        tutorialState = currentTutorial;
        this.firstTime = firstTime;
        this.hasCompleted = hasCompleted;
        if (hasCompleted)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialState == TutorialState.currencyCollect && readyForNextTut)
        {
            TriggerNextPart();
        }
        else if(tutorialState == TutorialState.firstContract && readyForNextTut && gc.currencyManager.money >= 100)
        {
            TriggerNextPart();
        }
        else if (tutorialState == TutorialState.firstGift && readyForNextTut && gc.currencyManager.money >= 150)
        {
            TriggerNextPart();
        }
        else if (tutorialState == TutorialState.firstRequest && readyForNextTut && gc.currencyManager.GetWoodPlanks() >= 10)
        {
            TriggerNextPart();
        }
        else if (tutorialState == TutorialState.buyTenant && readyForNextTut && gc.reputation.currentRepLevel >= 2)
        {
            TriggerNextPart();
        }
        else if (tutorialState == TutorialState.finalNotes && readyForNextTut)
        {
            TriggerNextPart();
        }
    }


    public void StartNextTut()
    {
        tutorialStep++;
        TriggerNextPart();
    }

    private void TriggerNextPart()
    {
        readyForNextTut = false;
        if (tutorialState == TutorialState.currencyCollect)
        {
            //triggers at start of game if first time
            if (tutorialStep == 1)
            {
                //start dialogue on how to collect currency
                //disable everything but collect tenant 1 button
                DisableScrolling();
                DisableUI();
                gc.gameUI.CloseAllMenus();
                DisableTenants();
                gc.tenantButtonHandler.DisableButtons();
                GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].transform.position, this.transform.rotation) as GameObject;
                DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
                tempDialogue.AddText(currencyCollectTextStep1);
                tempDialogue.SetAutoText(false);
                tempDialogue.SetUsesButtons(false);

            }
            //triggers after dialogue finishes
            else if(tutorialStep == 2)
            {
                var currencyButton = gc.tenants[0].GetComponent<Tenant>().currencyCollectButton;
                activeArrow = Instantiate(pointingArrow, currencyButton.transform.position, this.transform.rotation) as GameObject;
                currencyButton.enabled = true;
                currencyButton.gameObject.AddComponent<TutorialButton>();
                currencyButton.GetComponent<TutorialButton>().SetActiveMethod(bm);
            }
            //triggers after player collects tutorial
            else if( tutorialStep == 3)
            {
                //reenable everything
                //start dialogue giving player next instructions (collect 100 dollars)
                if(activeArrow != null)
                {
                    Destroy(activeArrow);
                }
                GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].transform.position, this.transform.rotation) as GameObject;
                DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
                tempDialogue.AddText(currencyCollectTextStep3);
                tempDialogue.SetAutoText(false);
                tempDialogue.SetUsesButtons(false);
                Destroy(gc.tenants[0].GetComponent<Tenant>().currencyCollectButton.gameObject.GetComponent<TutorialButton>());
            }
            else
            {
                tutorialStep = 1;
                tutorialState = TutorialState.firstContract;
                EnableScolling();
                EnableUI();
                gc.gameUI.CloseAllMenus();
                EnableTenants();
                gc.tenantButtonHandler.EnableButtons();
                readyForNextTut = true;
            }
        }
        else if(tutorialState == TutorialState.firstContract)
        {
            //triggers once player has 100 dollars
            if (tutorialStep == 1)
            {
                //disable everything but shop
                //start dialogue telling player what a contract is
                //tell them to open shop with arrow
                DisableScrolling();
                DisableUI();
                gc.gameUI.CloseAllMenus();
                DisableTenants();
                gc.tenantButtonHandler.DisableButtons();
                GameObject dialogue = Instantiate(DialogueRef, gc.tenants[0].transform.position, this.transform.rotation) as GameObject;
                DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
                tempDialogue.AddText(currencyCollectTextStep1);
                tempDialogue.SetAutoText(false);
                tempDialogue.SetUsesButtons(false);
            }

            else if (tutorialStep == 2)
            {
                activeArrow = Instantiate(pointingArrow, gc.gameUI.inventoryImage.transform.position, this.transform.rotation) as GameObject;
                gc.gameUI.shopImage.GetComponent<Button>().enabled = true;
                gc.gameUI.shopImage.GetComponent<Button>().onClick.AddListener(StartNextTut);

            }
            //triggers once contract shop is opened
            else if (tutorialStep == 3)
            {
                //arrow to contract
                //disabled other buttons besides contract shop tab
                gc.gameUI.shopImage.GetComponent<Button>().onClick.RemoveListener(StartNextTut);
                gc.gameUI.giftsMenu.openContractShopButton.onClick.AddListener(StartNextTut);
            }
            //triggers once contract confirm is opened
            else if (tutorialStep == 4)
            {

                gc.gameUI.giftsMenu.openContractShopButton.onClick.RemoveListener(StartNextTut);
                //disable other buttons besides the contract
            }
            
            else if (tutorialStep == 5)
            {
                
                //disable other buttons besides the yes button
            }
            //triggers once contract is purchased
            else
            {
                //explain that the player is now collecting material and can collect from delivery bird and what to do next
                tutorialStep = 1;
                tutorialState = TutorialState.firstGift;
                readyForNextTut = true;
            }
        }
        else if (tutorialState == TutorialState.firstGift)
        {
            //
            if (tutorialStep == 1)
            {
                //
            }
            //
            else if (tutorialStep == 2)
            {
                //
            }
            //
            else
            {
                //
                tutorialStep = 1;
                tutorialState = TutorialState.buyTenant;
            }
        }
        else if (tutorialState == TutorialState.buyTenant)
        {
            //
            if (tutorialStep == 1)
            {
                //
            }
            //
            else if (tutorialStep == 2)
            {
                //
            }
            //
            else
            {
                //
                tutorialStep = 1;
                tutorialState = TutorialState.finalNotes;
            }
        }
        else if (tutorialState == TutorialState.finalNotes)
        {
            //
            if (tutorialStep == 1)
            {
                //
            }
            //
            else if (tutorialStep == 2)
            {
                //
            }
            //
            else
            {
                //
                tutorialStep = 1;
                hasCompleted = true;
            }
        }
    }

    private void DisableTenants()
    {
        foreach (var tenant in gc.tenants)
        {
            tenant.GetComponent<Tenant>().isDisabled = true;
            tenant.GetComponent<Tenant>().currencyCollectButton.enabled = false;
        }
        gc.deliveryBird.enabled = false;
    }

    private void EnableTenants()
    {
        foreach (var tenant in gc.tenants)
        {
            tenant.GetComponent<Tenant>().isDisabled = false;
            tenant.GetComponent<Tenant>().currencyCollectButton.enabled = true;
        }
        gc.deliveryBird.enabled = true;
    }

    private void DisableUI()
    {
        gc.gameUI.inventoryImage.GetComponent<Button>().enabled = false;
        gc.gameUI.shopImage.GetComponent<Button>().enabled = false;
        gc.gameUI.optionsImage.GetComponent<Button>().enabled = false;
        gc.gameUI.bonusImage.GetComponent<Button>().enabled = false;
        gc.collectButton.enabled = false;
    }

    private void EnableUI()
    {
        gc.gameUI.inventoryImage.GetComponent<Button>().enabled = true;
        gc.gameUI.shopImage.GetComponent<Button>().enabled = true;
        gc.gameUI.optionsImage.GetComponent<Button>().enabled = true;
        gc.gameUI.bonusImage.GetComponent<Button>().enabled = true;
        gc.collectButton.enabled = true;
    }

    private void DisableScrolling()
    {
        gc.touchScreen.scrollLock = true;
    }

    private void EnableScolling()
    {
        gc.touchScreen.scrollLock = false;
    }

    private void DisableAll()
    {
        DisableTenants();
        DisableUI();
        DisableScrolling();
    }


    private void EnableAll()
    {
        EnableTenants();
        EnableUI();
        EnableScrolling();
    }
    */
    #endregion

    public GameController gc;
    private bool inTutorial;
    public enum TutorialState
    {
        idle, ready, initialStep, firstContract, firstGift, firstRequest, firstTenant, finished
    }
    [SerializeField]
    private TutorialState tutorialState;
    public GameObject initStepRef;
    public GameObject contractTutRef;
    public GameObject giftTutRef;
    public GameObject firstTenentTutRef;
    public GameObject finalWordsRef;
    public GameObject requestTutRef;

    public GameObject tutorialObjects;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        inTutorial = false;
        //tutorialState = TutorialState.idle;
        //tutorialState = TutorialState.idle;
    }

    void Update()
    {
        if (tutorialState == TutorialState.ready && !inTutorial)
        {
            //initial step - Shows how to collect
            NextStep();
        }
        else if (tutorialState == TutorialState.initialStep && !inTutorial)
        {
            //first contract - Shows how to buy first contract
            NextStep();
        }
        else if (tutorialState == TutorialState.firstContract && !inTutorial)
        {
            //first gift - Shows how to buy first gift
            NextStep();
        }

        else if (tutorialState == TutorialState.firstGift && !inTutorial)
        {
            //first request - Shows how to buy complete first request
            NextStep();
        }

        else if (tutorialState == TutorialState.firstRequest && !inTutorial && gc.tenants[1].activeSelf)
        {
            //first tenant - Shows how to buy first teanant and has ending speach
            NextStep();
        }

        else if (tutorialState == TutorialState.firstTenant && !inTutorial)
        {
            //final one, destroys tutorial object
            NextStep();
        }
    }

    public void InitialState(TutorialState state)
    {
        tutorialState = state;
        //tutorialState = TutorialState.finished;
        if (tutorialState == TutorialState.finished)
        {
            if(gc == null)
            {
                gc = GameObject.Find("GameController").GetComponent<GameController>();
            }
            gc.collectButton.enabled = true;
            Destroy(tutorialObjects);
        }
        else if(tutorialState == TutorialState.idle)
        {
            tutorialState = TutorialState.ready;
        }
    }

    public void NextStep()
    {

        if (!inTutorial)
        {
            tutorialState++;
            inTutorial = true;
            if (tutorialState == TutorialState.initialStep)
            {
                initStepRef.SetActive(true);
            }
            else if (tutorialState == TutorialState.firstContract)
            {
                contractTutRef.SetActive(true);
            }
            else if (tutorialState == TutorialState.firstGift)
            {
                giftTutRef.SetActive(true);
            }
            else if (tutorialState == TutorialState.firstRequest)
            {
                requestTutRef.SetActive(true);
            }
            else if (tutorialState == TutorialState.firstTenant)
            {
                firstTenentTutRef.SetActive(true);
            }
            DisableAll();
        }
        else
        {
            inTutorial = false;
            EnableAll();
        }
        if (tutorialState == TutorialState.finished)
        {
            inTutorial = false;
            if(GameObject.Find("TutArrow") != null)
                Destroy(GameObject.Find("TutArrow"));
            EnableAll();
            gc.collectButton.enabled = true;
            gc.savedData.saveData();
            //gc.savedData.saveData("save1");
            Destroy(tutorialObjects);
        }
    }

    #region Disable/Enable UI

    private void DisableTenants()
    {
        foreach (var tenant in gc.tenants)
        {
            tenant.GetComponent<Tenant>().isDisabled = true;
            tenant.GetComponent<Tenant>().currencyCollectButton.enabled = false;
        }
        gc.deliveryBird.enabled = false;
    }

    private void EnableTenants()
    {
        foreach (var tenant in gc.tenants)
        {
            tenant.GetComponent<Tenant>().isDisabled = false;
            tenant.GetComponent<Tenant>().currencyCollectButton.enabled = true;
        }
        gc.deliveryBird.enabled = true;
    }

    private void DisableUI()
    {
        gc.gameUI.inventoryImage.GetComponent<Button>().enabled = false;
        gc.gameUI.shopImage.GetComponent<Button>().enabled = false;
        gc.gameUI.optionsImage.GetComponent<Button>().enabled = false;
        gc.gameUI.bonusImage.GetComponent<Button>().enabled = false;
        gc.collectButton.enabled = false;
    }

    private void EnableUI()
    {
        gc.gameUI.inventoryImage.GetComponent<Button>().enabled = true;
        gc.gameUI.shopImage.GetComponent<Button>().enabled = true;
        gc.gameUI.optionsImage.GetComponent<Button>().enabled = true;
        gc.gameUI.bonusImage.GetComponent<Button>().enabled = true;
    }

    private void DisableScrolling()
    {
        gc.touchScreen.scrollLock = true;
    }

    private void EnableScrolling()
    {
        gc.touchScreen.scrollLock = false;
    }

    private void DisableAll()
    {
        DisableTenants();
        DisableUI();
        DisableScrolling();
    }


    private void EnableAll()
    {
        EnableTenants();
        EnableUI();
        EnableScrolling();
    }

    #endregion

    public bool GetInTutorial()
    {
        return inTutorial;
    }

    public TutorialState GetTutorialState()
    {
        return tutorialState;
    }

}
