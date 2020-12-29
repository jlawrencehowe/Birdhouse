using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reputation : MonoBehaviour
{


    public int reputation;
    public float shownReputation;
    public GameObject roof;
    private GameController gc;
    public Image reputationBar;
    public int nextRepLevel;
    public int prevRepLevel;
    public List<int> repLevels = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 10, 11, 13, 15, 17, 19, 21, 23, 25, 27, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 55, 60, 63 };
    public int currentRepLevel;
    private Dictionary<string, int> contractLvls;
    public Image sparkles;
    public int giftSetLevel;
    public TextMeshProUGUI reputationLevelText;

    public GameObject reputationNotificationRef;
    public GameObject dialgoueRef;

    public GameObject trophyRef, confettiRef, activeConfetti, finaleHandlerRef, eggRef;

    private float roofDistance;


    void Awake()
    {
        giftSetLevel = 1;
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        //reputationBar = GameObject.Find("ReputationBar").GetComponent<Image>();
        //repLevels
        nextRepLevel = repLevels[0];
        AddReputation(0);
        contractLvls = new Dictionary<string, int>();
        contractLvls.Add("rawMat1", 0);
        contractLvls.Add("rawMat2", -1);
        contractLvls.Add("rawMat3", -1);
        contractLvls.Add("rawMat4", -1);
        contractLvls.Add("rawMat5", -1);
        contractLvls.Add("rawMat6", -1);
        contractLvls.Add("rawMat7", -1);
        contractLvls.Add("rawMat8", -1);
        contractLvls.Add("rawMat9", -1);
        reputationLevelText.text = "" + currentRepLevel;
        UpdateReputationBar();
    }

    void Update()
    {
        if (shownReputation != reputation)
        {
            shownReputation += Time.deltaTime * 0.5f;
            if (shownReputation >= reputation)
            {
                shownReputation = reputation;
            }
            UpdateReputationBar();
            sparkles.enabled = true;
        }
        else
        {
            sparkles.enabled = false;
        }
    }



    public void AddReputation(int reputation)
    {
        this.reputation += reputation;

    }

    public void QuickAddReputation(int reputation)
    {
        this.reputation += reputation;
        shownReputation = reputation;
        QuickUpdateReputationBar();
    }

    private void UpdateReputationBar()
    {
        reputationBar.fillAmount = (shownReputation - prevRepLevel) / (nextRepLevel - prevRepLevel);
        if (reputationBar.fillAmount >= 1)
        {
            CheckReputation(false);
        }

    }

    private void QuickUpdateReputationBar()
    {
        reputationBar.fillAmount = (shownReputation - prevRepLevel) / (nextRepLevel - prevRepLevel);
        if (reputationBar.fillAmount >= 1)
        {
            CheckReputation(true);
        }

    }


    private void CheckReputation(bool quickLoad)
    {
        bool stopLoop = true;
        if (currentRepLevel <= repLevels.Count && reputation >= repLevels[currentRepLevel])
        {
            currentRepLevel++;
            reputationLevelText.text = "" + currentRepLevel;
            string text = "";
            if (currentRepLevel == 1)
            {
                //unlock tenant 2
                UnlockNextTenant(1);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 2)
            {
                //unlock metal contract
                contractLvls["rawMat2"] = 0;
                text = "Metal contract Unlocked!";
            }
            else if (currentRepLevel == 3)
            {
                //unlock tenant 3
                UnlockNextTenant(2);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 4)
            {
                //unlock tenant 4 
                UnlockNextTenant(3);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 5)
            {
                //unlock Cloth
                contractLvls["rawMat3"] = 0;
                text = "Cloth contract Unlocked!";
            }
            else if (currentRepLevel == 6)
            {
                //unlock tenant 5
                UnlockNextTenant(4);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 7)
            {
                //unlock screws
                contractLvls["rawMat4"] = 0;
                text = "Screws contract Unlocked!";
            }
            else if (currentRepLevel == 8)
            {
                //unlock tenant 6
                UnlockNextTenant(5);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 9)
            {
                //unlock wires
                contractLvls["rawMat5"] = 0;
                text = "Wires contract Unlocked!";
            }
            else if (currentRepLevel == 10)
            {
                //unlock plastic
                contractLvls["rawMat6"] = 0;
                text = "Plastic contract Unlocked!";
            }
            else if (currentRepLevel == 11)
            {
                //unlock tenant 7
                UnlockNextTenant(6);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 12)
            {
                //unlock tenant 8
                UnlockNextTenant(7);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 13)
            {
                //unlock glass
                contractLvls["rawMat7"] = 0;
                text = "Glass contract Unlocked!";
            }
            else if (currentRepLevel == 14)
            {
                //unlock tenant 9
                UnlockNextTenant(8);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 15)
            {
                //unlock 2nd gift set
                giftSetLevel = 2;
                if (!quickLoad)
                {
                    if (gc == null)
                    {
                        gc = GameObject.Find("GameController").GetComponent<GameController>();
                    }
                    gc.gameUI.giftsMenu.SetNewItems();
                    gc.giftShopTimer = 300;
                }
                text = "New gifts in the shop!";
            }
            else if (currentRepLevel == 16)
            {
                //unlock tenant 10
                UnlockNextTenant(9);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 17)
            {
                //unlock tenant 11
                UnlockNextTenant(10);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 18)
            {
                //unlock paint
                contractLvls["rawMat8"] = 0;
                text = "Paint contract Unlocked!";
            }
            else if (currentRepLevel == 19)
            {
                //unlock tenant 12
                UnlockNextTenant(11);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 20)
            {
                //unlock tenant 13
                UnlockNextTenant(12);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 21)
            {
                //unlock tenant 14
                UnlockNextTenant(13);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 22)
            {
                //unlock tenant 15
                UnlockNextTenant(14);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 23)
            {
                //unlock tenant 16
                UnlockNextTenant(15);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 24)
            {
                //unlock 3rd gift set
                giftSetLevel = 3;
                if (!quickLoad)
                {
                    if (gc == null)
                    {
                        gc = GameObject.Find("GameController").GetComponent<GameController>();
                    }
                    gc.gameUI.giftsMenu.SetNewItems();
                    gc.giftShopTimer = 300;
                }
                text = "New gifts in the shop!";
            }
            else if (currentRepLevel == 25)
            {
                //unlock tenant 17
                UnlockNextTenant(16);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 26)
            {
                //unlock tenant 18
                UnlockNextTenant(17);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 27)
            {
                //unlock tenant 19
                UnlockNextTenant(18);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 28)
            {
                //unlock gold 
                contractLvls["rawMat9"] = 0;
                text = "Gold contract Unlocked!";
            }
            else if (currentRepLevel == 29)
            {
                //unlock tenant 20
                UnlockNextTenant(19);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 30)
            {
                //unlock tenant 21
                UnlockNextTenant(20);
                text = "New Tenant unlocked!";
            }
            else if (currentRepLevel == 31)
            {
                if (Tenant.totalFavor >= 63)
                {
                    ActivateFinale(quickLoad);
                }
                else if(!quickLoad)
                {
                    ReputationComplete();
                }
                
            }
            stopLoop = false;
            if (!quickLoad && currentRepLevel != 31)
            {
                GameObject tempRepNot = Instantiate(reputationNotificationRef, this.transform.position, this.transform.rotation) as GameObject;
                ReputationUnlockCanvas tempUnlockCan = tempRepNot.GetComponent<ReputationUnlockCanvas>();
                //tempUnlockCan.canvas.ca
                tempUnlockCan.UpdateRepText(text);
                if(currentRepLevel == 1 && gc.tutorial != null)
                {
                    tempUnlockCan.yesButton.onClick.AddListener(gc.tutorial.requestTutRef.GetComponent<RequestTutorial>().FinishRequest);
                    
                }
            }
            
        }
        if (currentRepLevel != 0)
        {
            prevRepLevel = repLevels[currentRepLevel - 1];
        }
        else
        {
            prevRepLevel = 0;
        }
        if(currentRepLevel < repLevels.Count)
            nextRepLevel = repLevels[currentRepLevel];
        else
        {
            nextRepLevel = 999;
        }
        if (!stopLoop)
        {
            if (quickLoad)
            {
                QuickUpdateReputationBar();
            }
            else
            {
                UpdateReputationBar();
            }
        }

    }

    void ReputationComplete()
    {
        Tenant.openUI = true;
        gc.gameUI.CloseAllMenus();
        GameObject dialogue = Instantiate(dialgoueRef, gc.tenants[0].GetComponent<Tenant>().dialogueSpawnSpot.transform.position, this.transform.rotation) as GameObject;
        DialogueBox tempDialogue = dialogue.GetComponent<DialogueBox>();
        List<string> loadedDialogue = new List<string>();
        TextAsset text = Resources.Load<TextAsset>("Dialogue/Finale/ReputationCompleteDialogue");
        var pennySprite = Resources.Load<Sprite>("Sprites/Characters/parakeet/pennyPortrait");
        loadedDialogue.Add(text.text);
        tempDialogue.AddText(loadedDialogue);
        tempDialogue.SetAudioClipType(0);
        tempDialogue.SetAutoText(false);
        tempDialogue.SetUsesButtons(false);

        tempDialogue.portrait.sprite = pennySprite;
        dialogue.AddComponent<ListenerScript>();
        dialogue.GetComponent<ListenerScript>().SetOnDestroyMethod(ReputationCompleteFinish);
        gc.mainCam.transform.position = new Vector3(gc.mainCam.transform.position.x,
            gc.tenants[0].transform.position.y, gc.mainCam.transform.position.z);
        gc.touchScreen.scrollLock = true;
    }

    void ReputationCompleteFinish()
    {
        gc.touchScreen.scrollLock = false;
        Tenant.openUI = false;
    }

    public void ActivateFinale(bool quickLoad) {
        if (!quickLoad)
        {
            activeConfetti = Instantiate(confettiRef, new Vector3(0, gc.mainCam.orthographicSize, 30), Quaternion.Euler(-90, 0, 0)) as GameObject;
            activeConfetti.transform.SetParent(gc.mainCam.transform, false);
            var tempHandler = Instantiate(finaleHandlerRef, Vector3.zero, transform.rotation) as GameObject;
            tempHandler.GetComponent<FinaleHandler>().confetti = activeConfetti;
        }
        else
            Instantiate(trophyRef, new Vector3(-0.45f, -10.5f, -1), transform.rotation);
        Instantiate(eggRef, new Vector3(-3.2f, 153, -1), transform.rotation);
    }


    public void UnlockNextTenant(int nextTenant)
    {
        if (gc != null)
        {
            gc.tenants[nextTenant].SetActive(true);
        }
        else
        {
            gc = GameObject.Find("GameController").GetComponent<GameController>();
            gc.tenants[nextTenant].SetActive(true);
        }

        roofDistance = roof.transform.position.y - gc.tenants[0].transform.position.y;
        var tempPos = gc.tenants[nextTenant].transform.position;
        tempPos.z = roof.transform.position.z;
        tempPos.y = gc.tenants[nextTenant].transform.position.y + roofDistance;
        roof.transform.position = tempPos;
        gc.touchScreen.UpdateTopMax(gc.tenants[nextTenant].transform.position.y);
    }

    private void UnlockNextContract(string contractMat, int level)
    {
        contractLvls[contractMat] = level;
    }

    public int GetAvailableContractLevel(string contractMat)
    {
        return contractLvls[contractMat];
    }

    public int GetReputation()
    {
        return (int)reputation;
    }
}