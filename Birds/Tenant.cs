using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tenant : MonoBehaviour
{

    public float heldMoney;
    public float maxMoney;
    public float moneyPerSec;
    public GameController gc;
    public bool isUnlocked = true;
    private static GameObject activeScreen;
    private GameObject myActiveScreen;
    public GameObject purchaseScreenRef;
    public GameObject requestScreenRef;
    public GameObject dialogueScreenRef;
    public int unlockCost;
    public Sprite unlockedSprite;
    private int currentRequest;
    public static bool openUI = false;
    public GameObject checkMark;
    public GameObject exclamationMark;
    public string birdName;
    public int birdVoiceType;
    private Animator birdAnim;
    public GameObject dialogueSpawnSpot;
    public GameObject timerSpawnSpot;

    public GameObject flyingMat;

    private Image inventoryImage;
    public SpriteRenderer birdRenderer;

    public List<Requests> listOfRequests;

    public bool isDisabled;

    public TenantButton.ButtonMethod passedButtonMethod;

    public int favor;
    public int favorLevel;
    public static int totalFavor = 0;

    public int heldMat;
    public int maxMat;
    public int matPerSec;
    public MatType matType;
    public enum MatType
    {
        rawMat1 = 1, rawMat2 = 2, rawMat3 = 3, rawMat4 = 4, rawMat5 = 5, rawMat6 = 6, rawMat7 = 7, rawMat8 = 8, rawMat9 = 9, rawMat10 = 10, rawMat11 = 11,
        rawMat12 = 12, rawMat13 = 13, rawMat14 = 14

    }
    public enum UpgradeType
    {
        moneyGen, matGen, maxHeld, maxGift, tenaCost, contrCost, giftCost, giftEffec,
        globMoneyGen, globMatGen, globMaxHeld
    }

    public List<UpgradeType> upgradeTypes;
    public List<float> upgradeAmounts;

    public Sprite matImage;
    public bool isGenMat;

    public List<int> favorCosts;
    public Animator[] hearts;

    public TenantButton currencyCollectButton;

    [System.Serializable]
    public struct Requests
    {
        public List<string> questDescription;
        public List<int> costs;
        public List<string> acceptedText;
        public List<string> completeMessage;
        public int repuationValue;
        public SpriteRenderer unlockedSprite;
        public float completionTime;
    }

    private float completionTime;
    public static bool activeRequest;
    bool isTenantRequestActive;
    public GameObject requestTimerRef;
    private GameObject requestTimerObject;
    private RequestTimer requestTimer;
    public List<SpriteRenderer> requestSprites;
    public List<string> requestItemNames;

    public GameObject currencyCollectFill;
    private Vector3 currencyCollectStartPos;

    public List<GameObject> heartFill;
    private List<Vector3> heartFillStartPos;

    public List<int> preferredGiftsByID;
    public List<int> dislikedGiftsByID;

    public GameObject favorUpCanvasRef;

    public AudioSource moveInAudio;

    private bool isCliked = false;


    public Sprite portraitSprite;

    public Renderer collectFillRenderer;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        inventoryImage = GameObject.Find("InventoryButton").GetComponent<Image>();
        passedButtonMethod = CollectMoney;
        currencyCollectButton.SetFunction(passedButtonMethod);
        InvokeRepeating("CheckExclamationMark", 0, 3);
        //birdNameText.text = birdName;
        currencyCollectStartPos = currencyCollectFill.transform.localPosition;
        birdAnim = GetComponentInChildren<Animator>();
        Invoke("TriggerBirdAnim", Random.Range(2f, 7f));

        if (heartFillStartPos == null)
        {
            heartFillStartPos = new List<Vector3>();
            heartFillStartPos.Add(heartFill[0].transform.localPosition);
            heartFillStartPos.Add(heartFill[1].transform.localPosition);
            heartFillStartPos.Add(heartFill[2].transform.localPosition);
        }
        /*foreach (var request in listOfRequests)
        {
            request.unlockedSprite.enabled = false;
        }*/

        for(int i = 0; i < 3; i++)
        {
            if(i >= GetRequestLevel())
            {
                listOfRequests[i].unlockedSprite.enabled = false;
            }
        }
        if (currentRequest < listOfRequests.Count || favorLevel < 3)
        {
            checkMark.SetActive(false);
        }
    }

    void Update()
    {

        //currencyCollectFill.transform.localPosition = Vector3.Lerp(currencyCollectStartPos, Vector3.zero, heldMoney / (maxMoney * GlobalMultipliers.MoneyHeld));

        if(heldMoney != 0 || maxMoney != 0)
            collectFillRenderer.material.SetFloat("_Slide", heldMoney / (maxMoney * GlobalMultipliers.MoneyHeld));
        else
            collectFillRenderer.material.SetFloat("_Slide", 0);

        if (isTenantRequestActive)
        {
            completionTime -= Time.deltaTime;
            if (requestTimer != null)
            {
                requestTimer.SetTime(completionTime, listOfRequests[currentRequest].completionTime);
            }
        }
    }


    public void OpenRequests()
    {
        if (!isTenantRequestActive && currentRequest < listOfRequests.Count)
        {
            //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            activeScreen = Instantiate(requestScreenRef, dialogueSpawnSpot.transform.position, transform.rotation) as GameObject;
            var request = listOfRequests[currentRequest];
            var requestMenu = activeScreen.GetComponent<RequestMenu>();
            requestMenu.SetInfo(request.questDescription, request.acceptedText, request.completeMessage,
                request.costs, this, request.repuationValue, request.unlockedSprite.sprite, requestItemNames[currentRequest]);
            requestMenu.portrait.sprite = portraitSprite;
            requestMenu.SetAudioClipType(birdVoiceType);
            myActiveScreen = activeScreen;
            //gc.touchScreen.scrollLock = true;
            gc.tenantButtonHandler.HideTenantButtons();
        }
        else
        {
            //
        }
    }

    public void OpenGifts()
    {
        gc.tenantButtonHandler.HideTenantButtons();
        gc.gameUI.OpenGiftMenu(this);
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCliked = true;
        }

        if (Input.GetMouseButtonUp(0) && !openUI && !isDisabled && activeScreen == null &&
                myActiveScreen == null && isCliked)
        {
            if (activeScreen != null && myActiveScreen == null)
            {
                Destroy(activeScreen);
            }
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            if (isUnlocked)
            {
                bool requestsDone;
                if (currentRequest < listOfRequests.Count)
                {
                    requestsDone = false;
                }
                else
                {
                    requestsDone = true;
                }
                gc.tenantButtonHandler.InitText(birdName, "$" + ((int)(moneyPerSec * 60 * GlobalMultipliers.MoneyGen)).ToString() + "/min", matPerSec.ToString(), matImage, isGenMat);
                //activate tenantButtonHandler
                passedButtonMethod = OpenRequests;
                gc.tenantButtonHandler.requestButton.SetFunction(passedButtonMethod);
                if (GetIsRequestActive() || requestsDone)
                {
                    gc.tenantButtonHandler.DisableRequestButton();
                    //Debug.Log("Disabled Request");
                }
                else
                {
                    gc.tenantButtonHandler.EnableRequestButton();
                    //Debug.Log("Enable Request");
                }
                if(GetFavorLevel() >= 3)
                {
                    gc.tenantButtonHandler.DisableGiftButton();
                }
                else
                {
                    gc.tenantButtonHandler.EnableGiftButton();
                }
                passedButtonMethod = OpenGifts;
                gc.tenantButtonHandler.giftButton.SetFunction(passedButtonMethod);
                gc.tenantButtonHandler.transform.position =
                    new Vector3(this.transform.position.x, this.transform.position.y, gc.tenantButtonHandler.transform.position.z);
            }
            else if (!isUnlocked)
            {
                gc.touchScreen.scrollLock = true;
                activeScreen = Instantiate(purchaseScreenRef, screenCenter, transform.rotation) as GameObject;
                activeScreen.GetComponent<TenantPurchaseScreen>().UpdateText((int)((float)unlockCost / GlobalMultipliers.TenantCostRedu));
                activeScreen.GetComponent<TenantPurchaseScreen>().AttachTenant(this);
                gc.tenantButtonHandler.transform.position =
                    new Vector3(10000, 10000, gc.tenantButtonHandler.transform.position.z);

                openUI = true;



            }
        }

        if (!Input.GetMouseButton(0))
        {
            isCliked = false;
        }
    }

    public void ClickedByPlayer()
    {

        if (!openUI && !isDisabled && activeScreen == null &&
                myActiveScreen == null && !gc.touchScreen.GetIsScrolling())
        {
            if (activeScreen != null && myActiveScreen == null)
            {
                Destroy(activeScreen);
            }
            Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            if (isUnlocked)
            {
                bool requestsDone;
                if (currentRequest < listOfRequests.Count)
                {
                    requestsDone = false;
                }
                else
                {
                    requestsDone = true;
                }
                gc.tenantButtonHandler.InitText(birdName, ((int)(moneyPerSec * 60 * GlobalMultipliers.MoneyGen)).ToString() + "/min", matPerSec.ToString(), matImage, isGenMat);
                //activate tenantButtonHandler
                passedButtonMethod = OpenRequests;
                gc.tenantButtonHandler.requestButton.SetFunction(passedButtonMethod);
                if (GetIsRequestActive() || requestsDone)
                {
                    gc.tenantButtonHandler.DisableRequestButton();
                    Debug.Log("Disabled Request");
                }
                else
                {
                    gc.tenantButtonHandler.EnableRequestButton();
                    Debug.Log("Enable Request");
                }
                passedButtonMethod = OpenGifts;
                gc.tenantButtonHandler.giftButton.SetFunction(passedButtonMethod);
                gc.tenantButtonHandler.transform.position =
                    new Vector3(this.transform.position.x, this.transform.position.y, gc.tenantButtonHandler.transform.position.z);
            }
            else if (!isUnlocked)
            {
                //gc.touchScreen.scrollLock = true;
                activeScreen = Instantiate(purchaseScreenRef, screenCenter, transform.rotation) as GameObject;
                activeScreen.GetComponent<TenantPurchaseScreen>().UpdateText((int)((float)unlockCost / GlobalMultipliers.TenantCostRedu));
                activeScreen.GetComponent<TenantPurchaseScreen>().AttachTenant(this);
                gc.tenantButtonHandler.transform.position =
                    new Vector3(10000, 10000, gc.tenantButtonHandler.transform.position.z);



            }
        }
    }



    public void UpdateMoney(float passedTime)
    {

        heldMoney += (moneyPerSec * passedTime * GlobalMultipliers.MoneyGen);
        if (heldMoney >= (maxMoney * GlobalMultipliers.MoneyHeld))
        {
            heldMoney = (maxMoney * GlobalMultipliers.MoneyHeld);
        }
        /*
        if (isGenMat)
        {
            heldMat += (matPerSec * passedTime);
            if (heldMat >= maxMat)
            {
                heldMat = maxMat;
            }
        }
        */
    }

    public void UpdateCompletionTime(float passedTime)
    {
        if (completionTime > 0)
        {
            completionTime -= passedTime;
            if (completionTime <= 0)
            {
                //do something
            }
        }

    }

    public void CollectMoney()
    {
        if ((!openUI && !isDisabled &&
              myActiveScreen == null))
        {
            gc.tenantButtonHandler.HideTenantButtons();
            int tempMoney = (int)heldMoney;
            gc.currencyManager.CollectMoney(this, tempMoney);
            heldMoney = heldMoney - tempMoney;
            gc.currencyManager.CollectRawMatByID((int)matType, heldMat);
            if (heldMat > 0)
            {

                GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, matImage, gc, heldMat);
            }
            heldMat = 0;
            UpdateMoney(0);

            gc.savedData.saveData();
        }

    }


    public void CollectMoney(Vector3 spawnPos)
    {
        if ((!openUI && !isDisabled &&
              myActiveScreen == null))
        {
            gc.tenantButtonHandler.HideTenantButtons();
            int tempMoney = (int)heldMoney;
            gc.currencyManager.CollectMoney(this, tempMoney);
            heldMoney = heldMoney - tempMoney;
            gc.currencyManager.CollectRawMatByID((int)matType, heldMat);
            if (heldMat > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, matImage, gc, heldMat);
            }
            heldMat = 0;
            UpdateMoney(0);
        }
    }

    public bool GetIsUnlocked()
    {
        return isUnlocked;
    }

    public void SetIsUnlocked(bool setVar)
    {
        isUnlocked = setVar;
        birdRenderer.enabled = setVar;
        //if true, remove fake dark room overlay
    }

    public void StartRequest()
    {
        activeRequest = true;
        isTenantRequestActive = true;
        gc.tenantButtonHandler.ActivateRequestMessage(true);
        completionTime = listOfRequests[currentRequest].completionTime;
        //gc.notificationHandler.RequestNotificationCreate((int)completionTime);
        requestTimerObject = Instantiate(requestTimerRef, timerSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        requestTimer = requestTimerObject.GetComponent<RequestTimer>();
        //requestTimer.sr.sprite = requestSprites[currentRequest].sprite;
        requestTimer.requestRenderer.material.SetTexture("_RequestTex", requestSprites[currentRequest].sprite.texture);
        Debug.Log(requestTimerObject);
        requestTimerObject.GetComponent<ListenerScript>().SetOnClickMethod(FinishRequestText);
        requestTimerObject.GetComponent<ListenerScript>().SetDeactivateAll(true);
        /*
        if (gc.tutorial.GetInTutorial() && gc.tutorial.GetTutorialState() == Tutorial.TutorialState.firstRequest)
        {
            requestTimerObject.GetComponent<ListenerScript>().SetOnDestroyMethod(gc.tutorial.requestTutRef.GetComponent<RequestTutorial>().FinishRequest);
        }*/
    }

    public void FinishRequestText()
    {
        //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        activeScreen = Instantiate(dialogueScreenRef, dialogueSpawnSpot.transform.position, transform.rotation) as GameObject;
        var request = listOfRequests[currentRequest];
        DialogueBox tempDialogue = activeScreen.GetComponent<DialogueBox>();
        tempDialogue.portrait.sprite = portraitSprite;
        tempDialogue.AddText(request.completeMessage);
        tempDialogue.SetAudioClipType(birdVoiceType);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);
        activeScreen.AddComponent<ListenerScript>();
        activeScreen.GetComponent<ListenerScript>().SetOnDestroyMethod(IncreaseRequest);

        myActiveScreen = activeScreen;
        //gc.touchScreen.scrollLock = true;
        gc.tenantButtonHandler.HideTenantButtons();
        Destroy(requestTimerObject);
    }

    public void IncreaseRequest()
    {
        Debug.Log("Request Finished");
        gc.notificationHandler.ClearRequestNotification();
        gc.reputation.AddReputation(listOfRequests[currentRequest].repuationValue);
        if (listOfRequests.Count >= currentRequest)
            listOfRequests[currentRequest].unlockedSprite.enabled = true;
        currentRequest++;
        gc.tenantButtonHandler.ActivateRequestMessage(false);
        activeRequest = false;
        isTenantRequestActive = false;
        /*
        if (currentRequest == 1)
        {
            
        }
        else if (currentRequest == 2)
        {
            isGenMat = true;
        }
        else if (currentRequest == 3)
        {

        }
        */
        CheckExclamationMark();
        if (currentRequest >= listOfRequests.Count && favorLevel >= 3)
        {
            checkMark.SetActive(true);
        }
        gc.savedData.saveData();
    }

    public void GiveGift(int ID)
    {
        float giftFavor;
        if (ID >= 0 && ID < 5)
        {
            giftFavor = 50;
        }
        else if (ID >= 5 && ID < 10)
        {
            giftFavor = 100;
        }
        else
        {
            giftFavor = 150;
        }
        bool skip = false;
        float giftLikeMult = 0;
        foreach (var giftID in preferredGiftsByID)
        {
            if (giftID == ID)
            {
                giftLikeMult = 0.5f;
                skip = true;
                break;
            }
        }
        if (!skip)
        {
            foreach (var giftID in dislikedGiftsByID)
            {
                if (giftID == ID)
                {
                    giftLikeMult = -0.9f;
                    break;
                }
            }
        }
        giftFavor = (int)(giftFavor * (GlobalMultipliers.GiftEffeIncre + giftLikeMult));

        favor += (int)giftFavor;
        if (favorLevel < heartFill.Count)
        {
            heartFill[favorLevel].transform.localPosition = Vector3.Lerp(heartFillStartPos[favorLevel], new Vector3(0, 0, -10), (float)favor / (float)favorCosts[favorLevel]);
        }
        CheckFavorLevel(false);
        if (currentRequest >= listOfRequests.Count && favorLevel >= 3)
        {
            checkMark.SetActive(true);
        }
        gc.savedData.saveData();
    }

    public void LoadFavor(int favor)
    {
        this.favor = favor;
        bool check = true;
        if (heartFillStartPos == null)
        {
            heartFillStartPos = new List<Vector3>();
            heartFillStartPos.Add(heartFill[0].transform.localPosition);
            heartFillStartPos.Add(heartFill[1].transform.localPosition);
            heartFillStartPos.Add(heartFill[2].transform.localPosition);
        }
        while (check)
        {
            check = CheckFavorLevel(true);
        }
        
        if (favorLevel < 3)
        {
            heartFill[favorLevel].transform.localPosition = Vector3.Lerp(heartFillStartPos[favorLevel], new Vector3(0, 0, -10), (float)this.favor / (float)favorCosts[favorLevel]);
            float tempFavor = favor;
            float tempFavorCost = favorCosts[favorLevel];
        }
    }

    public bool CheckFavorLevel(bool quickLoad)
    {
        if (favorLevel < favorCosts.Count && favor >= favorCosts[favorLevel])
        {
            favor -= favorCosts[favorLevel];
            heartFill[favorLevel].transform.localPosition = new Vector3(0, 0, -10);
            hearts[favorLevel].SetTrigger("Filled");
            string text = "You've gained favor with " + birdName + "\n";
            if (upgradeTypes[favorLevel] == UpgradeType.moneyGen)
            {

                moneyPerSec = (moneyPerSec * (1 + upgradeAmounts[favorLevel]));
                text = "Rent increased by " + upgradeAmounts[favorLevel].ToString("#0.##%");
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.matGen)
            {
                GlobalMultipliers.IncreaseMatMultiByID((int)matType, upgradeAmounts[favorLevel]);
                if(gc == null)
                {
                    gc = GameObject.Find("GameController").GetComponent<GameController>();
                }
                text = gc.gameUI.rawMatMenu.GetMatNameByID((int)matType) + " contract has improved by " + upgradeAmounts[favorLevel].ToString("#0.##%");
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.maxHeld)
            {
                maxMoney *= (1 + upgradeAmounts[favorLevel]);
                text = "Max rent has increased by " + upgradeAmounts[favorLevel].ToString("#0.##%");
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.maxGift)
            {
                GlobalMultipliers.MaxGiftIncrease += (int)upgradeAmounts[favorLevel];
                text = "The gift shop now can hold an extra gift!";
                if (!quickLoad)
                {
                    if (gc == null)
                    {
                        gc = GameObject.Find("GameController").GetComponent<GameController>();
                    }
                    gc.gameUI.giftsMenu.SetNewItems();
                    gc.giftShopTimer = 300;
                }
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.tenaCost)
            {
                GlobalMultipliers.TenantCostRedu += upgradeAmounts[favorLevel];
                text = "Renovation costs have been reduced by " + upgradeAmounts[favorLevel].ToString("#0.##%");
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.contrCost)
            {
                GlobalMultipliers.ContractCostRedu += upgradeAmounts[favorLevel];
                text = "Contract costs have been reduced by " + upgradeAmounts[favorLevel].ToString("#0.##%");
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.giftCost)
            {
                GlobalMultipliers.GiftCostRedu += upgradeAmounts[favorLevel];
                text = "Gift costs have been reduced by " + upgradeAmounts[favorLevel].ToString("#0.##%");
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.giftEffec)
            {
                GlobalMultipliers.GiftEffeIncre += upgradeAmounts[favorLevel];
                text = "Gift effectiveness has been increased by " + upgradeAmounts[favorLevel].ToString("#0.##%");
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.globMoneyGen)
            {
                GlobalMultipliers.MoneyGen += upgradeAmounts[favorLevel];
                text = "All tenant rent rates have been increased by " + upgradeAmounts[favorLevel].ToString("#0.##%");
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.globMatGen)
            {
                GlobalMultipliers.MatGen += upgradeAmounts[favorLevel];
                text = "All contract rates have been increased by " + upgradeAmounts[favorLevel].ToString("#0.##%"); ;
            }
            else if (upgradeTypes[favorLevel] == UpgradeType.globMaxHeld)
            {
                GlobalMultipliers.MoneyHeld += upgradeAmounts[favorLevel];
                text = "All max rent of tenants have been increased by " + upgradeAmounts[favorLevel].ToString("#0.##%"); ;
            }

            favorLevel++;
            totalFavor++;
            if (favorLevel < heartFill.Count)
            {
                heartFill[favorLevel].transform.localPosition = Vector3.Lerp(heartFillStartPos[favorLevel], new Vector3(0, 0, -10), (float)favor / (float)favorCosts[favorLevel]);
            }
            //Debug.Log("favor Level: " + favorLevel);
            if (!quickLoad)
            {
                GameObject tempRepNot = Instantiate(favorUpCanvasRef, this.transform.position, this.transform.rotation) as GameObject;
                ReputationUnlockCanvas tempUnlockCan = tempRepNot.GetComponent<ReputationUnlockCanvas>();
                //tempUnlockCan.canvas.ca
                tempUnlockCan.UpdateFavorText(text);
                if(totalFavor >= 63 && gc.reputation.currentRepLevel < 31)
                {
                    tempRepNot.AddComponent<ListenerScript>();
                    var tempListenerScript = tempRepNot.GetComponent<ListenerScript>();
                    tempListenerScript.SetOnDestroyMethod(FavorComplete);
                }
                else if(totalFavor >= 63 && gc.reputation.currentRepLevel >= 31)
                {
                    gc.reputation.ActivateFinale(quickLoad);
                }
                if (favorLevel == 1 && gc.tutorial != null)
                {
                    tempUnlockCan.yesButton.onClick.AddListener(gc.tutorial.giftTutRef.GetComponent<GiftTutorial>().FinishGivingGift);

                }

            }
            return true;
        }
        return false;
    }

    void FavorComplete()
    {
        Tenant.openUI = true;
        gc.gameUI.CloseAllMenus();
        GameObject dialogue = Instantiate(dialogueScreenRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        List<string> loadedDialogue = new List<string>();
        TextAsset text = Resources.Load<TextAsset>("Dialogue/Finale/FavorCompleteDialogue");
        var pennySprite = Resources.Load<Sprite>("Sprites/Characters/parakeet/pennyPortrait");
        loadedDialogue.Add(text.text);
        tempDialogue.AddText(loadedDialogue);
        tempDialogue.SetAudioClipType(birdVoiceType);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);

        tempDialogue.portrait.sprite = pennySprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(FavorCompleteFinished);
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x,
            gc.tenants[0].transform.position.y, gc.mainCam.transform.position.z);
        gc.touchScreen.scrollLock = true;
    }

    void FavorCompleteFinished()
    {
        gc.touchScreen.scrollLock = false;
        Tenant.openUI = false;
    }

    public int GetRequestLevel()
    {
        return currentRequest;
    }

    public int GetFavor()
    {
        int tempFav = favor;
        for(int i = 0; i < favorLevel; i++)
        {
            tempFav += favorCosts[i];
        }
        return tempFav;
    }

    public int GetFavorLevel()
    {
        return favorLevel;
    }


    public void CheckExclamationMark()
    {
        bool showMark = true;
        if (listOfRequests.Count > currentRequest)
        {
            for (int i = 0; i < listOfRequests[currentRequest].costs.Count; i++)
            {
                if (listOfRequests[currentRequest].costs[i] > gc.currencyManager.GetCurrencyByID(i))
                {
                    showMark = false;
                    break;
                }
            }
            if (showMark && gc.tutorial.GetTutorialState() > Tutorial.TutorialState.firstGift)
            {
                exclamationMark.SetActive(true);
            }
            else
            {
                exclamationMark.SetActive(false);
            }
        }
        else
        {
            exclamationMark.SetActive(false);
        }

        if (!GetIsUnlocked())
        {
            exclamationMark.SetActive(false);
        }
    }

    public static bool GetIsRequestActive()
    {
        return activeRequest;
    }

    public bool GetIsTenantRequestActive()
    {
        return isTenantRequestActive;
    }

    public void SetIsRequestActive(bool isActive)
    {
        activeRequest = isActive;
    }

    public void SetIsTenantRequestActive(bool isActive)
    {
        isTenantRequestActive = isActive;
    }

    public float GetRequestCompletionTime()
    {
        return completionTime;
    }

    public void LoadRequestTimer(float completionTime)
    {
        this.completionTime = completionTime;
        SetIsTenantRequestActive(true);
        SetIsRequestActive(true);
        if(gc == null)
            gc = GameObject.Find("GameController").GetComponent<GameController>();
        gc.tenantButtonHandler.ActivateRequestMessage(true);
        requestTimerObject = Instantiate(requestTimerRef, timerSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        requestTimer = requestTimerObject.GetComponent<RequestTimer>();
        //requestTimer.sr.sprite = requestSprites[currentRequest].sprite;
        requestTimer.requestRenderer.material.SetTexture("_RequestTex", requestSprites[currentRequest].sprite.texture);
        requestTimerObject.GetComponent<ListenerScript>().SetOnClickMethod(FinishRequestText);
        requestTimerObject.GetComponent<ListenerScript>().SetDeactivateAll(true);
    }

    public void LoadReputationLevel(int level)
    {

        for (int i = 0; i < level; i++)
        {
            //Debug.Log("load reputation: " + i);
            listOfRequests[i].unlockedSprite.enabled = true;
        }
        currentRequest = level;
        if (currentRequest >= listOfRequests.Count && favorLevel >= 3)
        {
            checkMark.SetActive(true);
        }

    }


    public void DebugReqeustTime(float time)
    {

        if (isTenantRequestActive)
        {
            completionTime -= time;
            if(requestTimer != null)
                requestTimer.SetTime(completionTime, listOfRequests[currentRequest].completionTime);
        }
    }

    private void TriggerBirdAnim()
    {
        if(GetIsUnlocked())
            birdAnim.SetTrigger("flap");
        Invoke("TriggerBirdAnim", Random.Range(2f, 7f));
    }

    public GameObject GetRequestTimer()
    {

        if (requestTimerObject != null)
            return requestTimerObject;
        return null;
    }

    public GameObject GetActiveRequest()
    {
        if (activeScreen != null)
            return activeScreen;
        return null;
    }
}
