using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopsMenu : MonoBehaviour
{

    public GameController gc;
    public GameUI gameUI;

    public Canvas giftShopCanvas;
    public Canvas upgradeShopCanvas;
    private RawMatUpgradeMenu upgradeMenu;

    public TextMeshProUGUI timer;
    public static int maxGifts = 2;



    #region giftshop variables

    //list of current items in shop
    private List<shopGift> giftItems;
    private List<int> quantityOfGiftsSelected;
    //the amount of a specific gift selected
    public List<TextMeshProUGUI> quantitySelectedText;
    //amount of a specific gift available for purchase
    public List<TextMeshProUGUI> quantityAvailText;
    public List<TextMeshProUGUI> giftCosts;
    public List<TextMeshProUGUI> giftNames;
    public List<Image> giftSprites;
    public List<Button> buyButtons;
    private bool setNewItems;
    public Button openContractShopButton;

    public Canvas confirmationCanvas;
    public int selectedGift;

    [Serializable]
    public class shopGift
    {
        public int id;
        public int tier;
        public int quantity;
        public string name;
        public int cost;
        public string imageFilePath;
        public int baseCost;

    }

    public AudioClip buttonPress, errorSound;


    #endregion

    void Awake()
    {
        
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        if(upgradeShopCanvas != null)
            upgradeMenu = upgradeShopCanvas.gameObject.GetComponent<RawMatUpgradeMenu>();

    }

    private void Update()
    {
        if (!setNewItems)
        {
            var time = gc.giftShopTimer;
            int min = (int)(time / 60);
            int seconds = (int)(time % 60);
            timer.text = "" + min + ":" + seconds.ToString("00");
        }
        else
        {
            timer.text = "0:00";
        }
    }

    private void Start()
    {
        if(giftItems == null)
            setNewItems = true;
        quantityOfGiftsSelected = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    }


    #region gift shop


    public void PressGiftButton(int buttonNum)
    {
        //check if player has enough material
        if (giftItems[buttonNum].cost * quantityOfGiftsSelected[buttonNum] <= gc.currencyManager.GetMoney())
        {
            selectedGift = buttonNum;
            confirmationCanvas.enabled = true;
        }
    }
    #region increase/decrease purchase amount
    public void IncreaseGiftAmount(int buttonNum)
    {
        if (quantityOfGiftsSelected[buttonNum] < giftItems[buttonNum].quantity)
        {
            quantityOfGiftsSelected[buttonNum]++;
            Debug.Log(quantityOfGiftsSelected[buttonNum]); 
            UpdateGiftTextAmount(buttonNum);
            UpdateGiftCost(buttonNum);
        }
    }

    public void DecreaseGiftAmount(int buttonNum)
    {
        if (quantityOfGiftsSelected[buttonNum] > 1)
        {
            quantityOfGiftsSelected[buttonNum]--;
            UpdateGiftTextAmount(buttonNum);
            UpdateGiftCost(buttonNum);
        }
    }

    public void UpdateGiftTextAmount(int buttonNum)
    {
        quantitySelectedText[buttonNum].text = quantityOfGiftsSelected[buttonNum].ToString();
    }
    #endregion

    public void UpdateGiftCost(int buttonNum)
    {
        var cost = giftItems[buttonNum].cost * quantityOfGiftsSelected[buttonNum];
        giftCosts[buttonNum].text = (cost).ToString();
        if(cost > gc.currencyManager.GetMoney())
        {
            ColorBlock tempColor = buyButtons[buttonNum].colors;
            tempColor.pressedColor = Color.red;
            buyButtons[buttonNum].colors = tempColor;
            buyButtons[buttonNum].GetComponent<OnClickSFX>().UpdateClip(errorSound);

        }
        else
        {
            ColorBlock tempColor = buyButtons[buttonNum].colors;
            tempColor.pressedColor = new Color32(200, 200, 200, 255);
            buyButtons[buttonNum].colors = tempColor;
            buyButtons[buttonNum].GetComponent<OnClickSFX>().UpdateClip(buttonPress);
        }
        /*
        if(quantityOfGiftsSelected[buttonNum] == 0)
        {
            giftCosts[buttonNum].text = giftItems[buttonNum].cost.ToString();
        }
        */
    }

    public void UpdateGiftButtons()
    {
        /*
            public List<Text> quantityAvailText;
    //the amount of a specific gift selected
    public List<Text> quantitySelectedText;
    public List<Text> giftCosts;
    */
        if (GlobalMultipliers.MaxGiftIncrease == 1)
        {
            buyButtons[3].transform.parent.gameObject.SetActive(true);
        }
        else if (GlobalMultipliers.MaxGiftIncrease == 2)
        {
            buyButtons[3].transform.parent.gameObject.SetActive(true);
            buyButtons[4].transform.parent.gameObject.SetActive(true);
        }
        for (int i = 0; i < buyButtons.Count - 2 + GlobalMultipliers.MaxGiftIncrease; i++)
        {
            quantityAvailText[i].text = "x" + giftItems[i].quantity.ToString();
            giftNames[i].text = giftItems[i].name;
            quantitySelectedText[i].text = "1";
            quantityOfGiftsSelected[i] = 1;
            giftItems[i].cost = (int)(giftItems[i].baseCost / GlobalMultipliers.GiftCostRedu);
            giftCosts[i].text = giftItems[i].cost.ToString();
            if(giftItems[i].cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = buyButtons[i].colors;
                tempColor.pressedColor = Color.red;
                buyButtons[i].colors = tempColor;
                buyButtons[i].GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = buyButtons[i].colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                buyButtons[i].colors = tempColor;
                buyButtons[i].GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            giftNames[i].text = giftItems[i].name;
            giftSprites[i].sprite = Resources.Load<Sprite>(giftItems[i].imageFilePath);
            if (giftItems[i].quantity <= 0)
            {
                buyButtons[i].enabled = false;
            }
            else
            {
                buyButtons[i].enabled = true;
            }
        }
        

    }

    //reset values to the default when first opening the menu
    //also if true, will reset gifts
    public void OpenMenu()
    {
        Tenant.openUI = true;
        giftShopCanvas.enabled = true;
        if (setNewItems)
        {
            NewGifts();
        }
        UpdateGiftButtons();

    }

    public void CloseMenu()
    {
        gc.gameUI.SetActiveMenu(GameUI.ActiveMenu.none);
        giftShopCanvas.enabled = false;
        gc.touchScreen.scrollLock = false;
        Tenant.openUI = false;
    }

    public void SetNewItems()
    {
        setNewItems = true;
    }

    public void NewGifts()
    {
        giftItems = new List<shopGift>();
        setNewItems = false;
        int[] choseGifts = new int[buyButtons.Count - 2 + GlobalMultipliers.MaxGiftIncrease];
        for(int w = 0; w < choseGifts.Length; w++)
        {
            choseGifts[w] = -1;
        }
        for (int y = 0; y < buyButtons.Count - 2 + GlobalMultipliers.MaxGiftIncrease; y++)
        {
            int max = 5;
            if (gc.reputation.giftSetLevel == 2)
            {
                max = 9;
            }
            else if (gc.reputation.giftSetLevel == 3)
            {
                max = 14;
            }
            int x = UnityEngine.Random.Range(0, max);
            bool repeatGift = false;
            do
            {
                repeatGift = false;
                for (int i = 0; i < choseGifts.Length; i++)
                {
                    if (choseGifts[i] == x)
                    {
                        repeatGift = true;
                        break;
                    }
                }
                if (repeatGift)
                {
                    x = UnityEngine.Random.Range(0, max);
                }
            } while (repeatGift);
            choseGifts[y] = x;
            giftItems.Add(AddGift(x));
        }



    }

    public void AcceptGiftPurchase()
    {
        //take required materials
        //add gift to inventory
        //remove amount from shop
        gc.currencyManager.AddMoney(-(quantityOfGiftsSelected[selectedGift] * giftItems[selectedGift].cost));
        gc.inventory.UpdateItemQuantity(giftItems[selectedGift].id, quantityOfGiftsSelected[selectedGift]);
        giftItems[selectedGift].quantity = giftItems[selectedGift].quantity - quantityOfGiftsSelected[selectedGift];
        //Debug.Log("Test: " + (giftItems[selectedGift].quantity - quantityOfGiftsSelected[selectedGift]).ToString());
        //Debug.Log("Quantity of Gift " + selectedGift + ": " + giftItems[selectedGift].quantity);
        confirmationCanvas.enabled = false;
        UpdateGiftButtons();
        //Debug.Log(quantityOfGiftsSelected[selectedGift]);
        gc.savedData.saveData();
    }

    public void DenyGiftPurchase()
    {
        confirmationCanvas.enabled = false;
    }

    public void OpenUpgradeShop()
    {
        DenyGiftPurchase();
        //upgradeShopCanvas.enabled = true;
        //upgradeShopCanvas.GetComponent<RawMatUpgradeMenu>().UpdateUpgradeButtons();
        upgradeMenu.OpenMenu();
        giftShopCanvas.enabled = false;
        Tenant.openUI = true;
    }

    public static shopGift AddGift(int num)
    {

        shopGift newGift = new shopGift();
        int giftQuantity = UnityEngine.Random.Range(1, 1 + maxGifts);
        newGift.id = num;
        if (num == 0)
        {
            newGift.name = "Cookies";
            newGift.quantity = giftQuantity;
            newGift.tier = 1;
            newGift.baseCost = 600;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_cookieIcon";
        }
        else if (num == 1)
        {
            newGift.name = "Vase of Flowers";
            newGift.quantity = giftQuantity;
            newGift.tier = 1;
            newGift.baseCost = 900;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_vaseIcon";
        }
        else if (num == 2)
        {
            newGift.name = "Mirror";
            newGift.quantity = giftQuantity;
            newGift.tier = 2;
            newGift.baseCost = 1200;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_mirrorIcon";
        }
        else if (num == 3)
        {
            newGift.name = "Books";
            newGift.quantity = giftQuantity;
            newGift.tier = 2;
            newGift.baseCost = 1200;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_bookIcon";
        }
        else if (num == 4)
        {
            newGift.name = "Painting";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 1200;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_paintingIcon";
        }
        else if (num == 5)
        {
            newGift.name = "Fish Dinner";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 3600;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_fishDinnerIcon";
        }
        else if (num == 6)
        {
            newGift.name = "Gadgets";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 4800;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_gadgetIcon";
        }
        else if (num == 7)
        {
            newGift.name = "Scarf";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 5400;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_scarfIcon";
        }
        else if (num == 8)
        {
            newGift.name = "Sportswear";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 6000;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_sportWearIcon";
        }
        else if (num == 9)
        {
            newGift.name = "Vinyl Album";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 6000;
            newGift.cost = (int)((int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu));
            newGift.imageFilePath = "Sprites/ItemIcons/store_vinylIcon";
        }
        else if (num == 10)
        {
            newGift.name = "Jewlery";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 18000;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_jewelryIcon";
        }
        else if (num == 11)
        {
            newGift.name = "Nightmask";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 18000;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_nightMask";
        }
        else if (num == 12)
        {
            newGift.name = "Artifact";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 21000;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_artifactIcon";
        }
        else if (num == 13)
        {
            newGift.name = "Fireworks";
            newGift.quantity = giftQuantity;
            newGift.tier = 3;
            newGift.baseCost = 24000;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_fireworksIcon";
        }

        else
        {
            newGift.name = "Cookies";
            newGift.quantity = giftQuantity;
            newGift.tier = 1;
            newGift.baseCost = 600;
            newGift.cost = (int)(newGift.baseCost / GlobalMultipliers.GiftCostRedu);
            newGift.imageFilePath = "Sprites/ItemIcons/store_cookieIcon";
        }
        return newGift;
    }


    #endregion

    public List<shopGift> CurrentShopGifts()
    {
        return giftItems;
    }

    public void LoadGifts(List<shopGift> savedList)
    {
        giftItems = savedList;
        setNewItems = false;
    }

}
