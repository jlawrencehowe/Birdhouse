using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RawMatUpgradeMenu : MonoBehaviour
{

    public GameController gc;
    public GameUI gameUI;

    public ShopsMenu giftShopMenu;
    public Canvas upgradeShopCanvas;

    public GameObject examineObject;
    public TextMeshProUGUI examineText;


    #region upgrade variables
    //the actual button, when pressed activates the upgrade
    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;
    public Button upgradeButton4;
    public Button upgradeButton5;
    public Button upgradeButton6;
    public Button upgradeButton7;
    public Button upgradeButton8;
    public Button upgradeButton9;

    //text on the button saying Wood Generator
    public TextMeshProUGUI upgrade1Text;
    public TextMeshProUGUI upgrade2Text;
    public TextMeshProUGUI upgrade3Text;
    public TextMeshProUGUI upgrade4Text;
    public TextMeshProUGUI upgrade5Text;
    public TextMeshProUGUI upgrade6Text;
    public TextMeshProUGUI upgrade7Text;
    public TextMeshProUGUI upgrade8Text;
    public TextMeshProUGUI upgrade9Text;

    //list of the cost to upgrade
    //level //money, raw1, raw2, raw3, raw4, raw5
    public int upgrade1BaseCosts;
    public int upgrade2BaseCosts;
    public int upgrade3BaseCosts;
    public int upgrade4BaseCosts;
    public int upgrade5BaseCosts;
    public int upgrade6BaseCosts;
    public int upgrade7BaseCosts;
    public int upgrade8BaseCosts;
    public int upgrade9BaseCosts;


    public int cost1PerLevel;
    public int cost2PerLevel;
    public int cost3PerLevel;
    public int cost4PerLevel;
    public int cost5PerLevel;
    public int cost6PerLevel;
    public int cost7PerLevel;
    public int cost8PerLevel;
    public int cost9PerLevel;

    //the displayed pictures for the icons/money and the text for the cost
    public TextMeshProUGUI upgrade1CostText;
    public TextMeshProUGUI upgrade2CostText;
    public TextMeshProUGUI upgrade3CostText;
    public TextMeshProUGUI upgrade4CostText;
    public TextMeshProUGUI upgrade5CostText;
    public TextMeshProUGUI upgrade6CostText;
    public TextMeshProUGUI upgrade7CostText;
    public TextMeshProUGUI upgrade8CostText;
    public TextMeshProUGUI upgrade9CostText;

    //contract level

    public TextMeshProUGUI upgrade1ContractLvl;
    public TextMeshProUGUI upgrade2ContractLvl;
    public TextMeshProUGUI upgrade3ContractLvl;
    public TextMeshProUGUI upgrade4ContractLvl;
    public TextMeshProUGUI upgrade5ContractLvl;
    public TextMeshProUGUI upgrade6ContractLvl;
    public TextMeshProUGUI upgrade7ContractLvl;
    public TextMeshProUGUI upgrade8ContractLvl;
    public TextMeshProUGUI upgrade9ContractLvl;

    public Canvas confirmationCanvas;
    private int activeUpgrade;
    #endregion

    public Scrollbar scrollbar;

    public AudioClip buttonPress, errorSound;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void CloseMenu()
    {
        gc.gameUI.SetActiveMenu(GameUI.ActiveMenu.none);
        upgradeShopCanvas.enabled = false;
        gc.touchScreen.scrollLock = false;
        Tenant.openUI = false;
    }

    public void OpenMenu()
    {
        scrollbar.value = 1;
        UpdateUpgradeButtons();
        upgradeShopCanvas.enabled = true;
        Tenant.openUI = true;
    }

    public void ExamineContract(int num)
    {

        examineObject.SetActive(true);
    }



    #region upgradeMenu

    public void PressUpgradeButton1(int buttonNum)
    {
        //check material player has vs the cost
        bool canPurchase = false;
        if (buttonNum == 0 && ((upgrade1BaseCosts + (gc.savedData.GetRawMat1UpgradeLvl() * cost1PerLevel))/GlobalMultipliers.ContractCostRedu) <= gc.currencyManager.GetMoney())
        {
            canPurchase = true;
        }
        else if (buttonNum == 1 && ((upgrade2BaseCosts + (gc.savedData.GetRawMat2UpgradeLvl() * cost2PerLevel)) / GlobalMultipliers.ContractCostRedu) <= gc.currencyManager.GetMoney())
        {
            canPurchase = true;
        }
        else if (buttonNum == 2 && ((upgrade3BaseCosts + (gc.savedData.GetRawMat3UpgradeLvl() * cost3PerLevel)) / GlobalMultipliers.ContractCostRedu) <= gc.currencyManager.GetMoney())
        {
            canPurchase = true;
        }
        else if (buttonNum == 3 && ((upgrade4BaseCosts + (gc.savedData.GetRawMat4UpgradeLvl() * cost4PerLevel)) / GlobalMultipliers.ContractCostRedu) <= gc.currencyManager.GetMoney())
        {
            canPurchase = true;
        }
        else if (buttonNum == 4 && ((upgrade5BaseCosts + (gc.savedData.GetRawMat5UpgradeLvl() * cost5PerLevel)) / GlobalMultipliers.ContractCostRedu) <= gc.currencyManager.GetMoney())
        {
            canPurchase = true;
        }
        else if (buttonNum == 5 && ((upgrade6BaseCosts + (gc.savedData.GetRawMat6UpgradeLvl() * cost6PerLevel)) / GlobalMultipliers.ContractCostRedu) <= gc.currencyManager.GetMoney())
        {
            canPurchase = true;
        }
        else if (buttonNum == 6 && ((upgrade7BaseCosts + (gc.savedData.GetRawMat7UpgradeLvl() * cost7PerLevel)) / GlobalMultipliers.ContractCostRedu) <= gc.currencyManager.GetMoney())
        {
            canPurchase = true;
        }
        else if (buttonNum == 7 && ((upgrade8BaseCosts + (gc.savedData.GetRawMat8UpgradeLvl() * cost8PerLevel)) / GlobalMultipliers.ContractCostRedu) <= gc.currencyManager.GetMoney())
        {
            canPurchase = true;
        }
        else if (buttonNum == 8 && ((upgrade9BaseCosts + (gc.savedData.GetRawMat9UpgradeLvl() * cost9PerLevel)) / GlobalMultipliers.ContractCostRedu) <= gc.currencyManager.GetMoney())
        {
            canPurchase = true;
        }
        if (canPurchase == true)
        {
            activeUpgrade = buttonNum;
            confirmationCanvas.enabled = true;
        }

    }
    #region finish upgrades
    public void FinishUpgrade1()
    {
        //if yes, take the material (probably have a yes or no prompt)
        gc.currencyManager.AddMoney(-(int)((upgrade1BaseCosts + (gc.savedData.GetRawMat1UpgradeLvl() * cost1PerLevel)) / GlobalMultipliers.ContractCostRedu));
        int level = gc.savedData.GetRawMat1UpgradeLvl();
        gc.savedData.SetRawMat1UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat1PerSec();
        confirmationCanvas.enabled = false;
        UpdateUpgradeButtons();
    }

    public void FinishUpgrade2()
    {
        //if yes, take the material (probably have a yes or no prompt)
        gc.currencyManager.AddMoney(-(int)((upgrade2BaseCosts + (gc.savedData.GetRawMat2UpgradeLvl() * cost2PerLevel)) / GlobalMultipliers.ContractCostRedu));
        int level = gc.savedData.GetRawMat2UpgradeLvl();
        gc.savedData.SetRawMat2UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat2PerSec();
        confirmationCanvas.enabled = false;
        UpdateUpgradeButtons();
    }

    public void FinishUpgrade3()
    {
        //if yes, take the material (probably have a yes or no prompt)
        gc.currencyManager.AddMoney(-(int)((upgrade3BaseCosts + (gc.savedData.GetRawMat3UpgradeLvl() * cost3PerLevel)) / GlobalMultipliers.ContractCostRedu));
        int level = gc.savedData.GetRawMat3UpgradeLvl();
        gc.savedData.SetRawMat3UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat3PerSec();
        confirmationCanvas.enabled = false;
        UpdateUpgradeButtons();
    }

    public void FinishUpgrade4()
    {
        //if yes, take the material (probably have a yes or no prompt)
        gc.currencyManager.AddMoney(-(int)((upgrade4BaseCosts + (gc.savedData.GetRawMat4UpgradeLvl() * cost4PerLevel)) / GlobalMultipliers.ContractCostRedu));
        int level = gc.savedData.GetRawMat4UpgradeLvl();
        gc.savedData.SetRawMat4UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat4PerSec();
        confirmationCanvas.enabled = false;
        UpdateUpgradeButtons();
    }

    public void FinishUpgrade5()
    {
        //if yes, take the material (probably have a yes or no prompt)
        gc.currencyManager.AddMoney(-(int)((upgrade5BaseCosts + (gc.savedData.GetRawMat5UpgradeLvl() * cost5PerLevel)) / GlobalMultipliers.ContractCostRedu));
        int level = gc.savedData.GetRawMat5UpgradeLvl();
        gc.savedData.SetRawMat5UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat5PerSec();
        confirmationCanvas.enabled = false;
        UpdateUpgradeButtons();
    }

    public void FinishUpgrade6()
    {
        //if yes, take the material (probably have a yes or no prompt)
        gc.currencyManager.AddMoney(-(int)((upgrade6BaseCosts + (gc.savedData.GetRawMat6UpgradeLvl() * cost6PerLevel)) / GlobalMultipliers.ContractCostRedu));
        int level = gc.savedData.GetRawMat6UpgradeLvl();
        gc.savedData.SetRawMat6UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat6PerSec();
        confirmationCanvas.enabled = false;
        UpdateUpgradeButtons();
    }

    public void FinishUpgrade7()
    {
        //if yes, take the material (probably have a yes or no prompt)
        gc.currencyManager.AddMoney(-(int)((upgrade7BaseCosts + (gc.savedData.GetRawMat7UpgradeLvl() * cost7PerLevel)) / GlobalMultipliers.ContractCostRedu));
        int level = gc.savedData.GetRawMat7UpgradeLvl();
        gc.savedData.SetRawMat7UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat7PerSec();
        confirmationCanvas.enabled = false;
        UpdateUpgradeButtons();
    }

    public void FinishUpgrade8()
    {
        //if yes, take the material (probably have a yes or no prompt)
        gc.currencyManager.AddMoney(-(int)((upgrade8BaseCosts + (gc.savedData.GetRawMat8UpgradeLvl() * cost8PerLevel)) / GlobalMultipliers.ContractCostRedu));
        int level = gc.savedData.GetRawMat8UpgradeLvl();
        gc.savedData.SetRawMat8UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat8PerSec();
        confirmationCanvas.enabled = false;
        UpdateUpgradeButtons();
    }

    public void FinishUpgrade9()
    {
        //if yes, take the material (probably have a yes or no prompt)
        gc.currencyManager.AddMoney(-(int)((upgrade9BaseCosts + (gc.savedData.GetRawMat9UpgradeLvl() * cost9PerLevel)) / GlobalMultipliers.ContractCostRedu));
        int level = gc.savedData.GetRawMat9UpgradeLvl();
        gc.savedData.SetRawMat9UpgradeLvl((level + 1));
        gc.deliveryBird.UpdateRawMat9PerSec();
        confirmationCanvas.enabled = false;
        UpdateUpgradeButtons();
    }

    #endregion


    public void ConfirmUpgrade()
    {
        if (activeUpgrade == 0)
        {
            FinishUpgrade1();
        }
        else if (activeUpgrade == 1)
        {
            FinishUpgrade2();
        }
        else if (activeUpgrade == 2)
        {
            FinishUpgrade3();
        }
        else if (activeUpgrade == 3)
        {
            FinishUpgrade4();
        }
        else if (activeUpgrade == 4)
        {
            FinishUpgrade5();
        }
        else if (activeUpgrade == 5)
        {
            FinishUpgrade6();
        }
        else if (activeUpgrade == 6)
        {
            FinishUpgrade7();
        }
        else if (activeUpgrade == 7)
        {
            FinishUpgrade8();
        }
        else if (activeUpgrade == 8)
        {
            FinishUpgrade9();
        }
        gc.savedData.saveData();
    }

    public void DenyUpgrade()
    {
        confirmationCanvas.enabled = false;
    }

    public void UpdateUpgradeButtons()
    {

        //Get reputation level and compare it 
        if (gc.reputation.GetAvailableContractLevel("rawMat1") != -1)
        {
            upgradeButton1.enabled = true;
            upgradeButton1.transform.parent.gameObject.SetActive(true);
            var cost = ((int)(((upgrade1BaseCosts + (gc.savedData.GetRawMat1UpgradeLvl() * cost1PerLevel))) / GlobalMultipliers.ContractCostRedu));
            upgrade1CostText.text = ((int)cost).ToString();
            if (cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = upgradeButton1.colors;
                tempColor.pressedColor = Color.red;
                upgradeButton1.colors = tempColor;
                upgradeButton1.GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = upgradeButton1.colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                upgradeButton1.colors = tempColor;
                upgradeButton1.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            upgrade1ContractLvl.text = "x" + gc.savedData.GetRawMat1UpgradeLvl().ToString();

        }
        else
        {
            upgradeButton1.enabled = false;
            upgradeButton1.transform.parent.gameObject.SetActive(false);
        }
        //Get reputation level and compare it 
        if (gc.reputation.GetAvailableContractLevel("rawMat2") != -1)
        {
            upgradeButton2.enabled = true;
            upgradeButton2.transform.parent.gameObject.SetActive(true); 
            var cost = ((int)(((upgrade2BaseCosts + (gc.savedData.GetRawMat2UpgradeLvl() * cost2PerLevel))) / GlobalMultipliers.ContractCostRedu));
            upgrade2CostText.text = ((int)cost).ToString();
            if (cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = upgradeButton2.colors;
                tempColor.pressedColor = Color.red;
                upgradeButton2.colors = tempColor;
                upgradeButton2.GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = upgradeButton2.colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                upgradeButton2.colors = tempColor;
                upgradeButton2.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            upgrade2ContractLvl.text = "x" + gc.savedData.GetRawMat2UpgradeLvl().ToString();
        }
        else
        {
            upgradeButton2.enabled = false;
            upgradeButton2.transform.parent.gameObject.SetActive(false);
        }
        //Get reputation level and compare it 
        if (gc.reputation.GetAvailableContractLevel("rawMat3") != -1)
        {
            upgradeButton3.enabled = true;
            upgradeButton3.transform.parent.gameObject.SetActive(true);
            var cost = ((int)(((upgrade3BaseCosts + (gc.savedData.GetRawMat3UpgradeLvl() * cost3PerLevel))) / GlobalMultipliers.ContractCostRedu));
            upgrade3CostText.text = ((int)cost).ToString();
            if (cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = upgradeButton3.colors;
                tempColor.pressedColor = Color.red;
                upgradeButton3.colors = tempColor;
                upgradeButton3.GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = upgradeButton3.colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                upgradeButton3.colors = tempColor;
                upgradeButton3.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            upgrade3ContractLvl.text = "x" + gc.savedData.GetRawMat3UpgradeLvl().ToString();
        }
        else
        {
            upgradeButton3.enabled = false;
            upgradeButton3.transform.parent.gameObject.SetActive(false);
        }
        //Get reputation level and compare it 
        if (gc.reputation.GetAvailableContractLevel("rawMat4") != -1)
        {
            upgradeButton4.enabled = true;
            upgradeButton4.transform.parent.gameObject.SetActive(true);
            var cost = ((int)(((upgrade4BaseCosts + (gc.savedData.GetRawMat4UpgradeLvl() * cost4PerLevel))) / GlobalMultipliers.ContractCostRedu));
            upgrade4CostText.text = ((int)cost).ToString();
            if (cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = upgradeButton4.colors;
                tempColor.pressedColor = Color.red;
                upgradeButton4.colors = tempColor;
                upgradeButton4.GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = upgradeButton4.colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                upgradeButton4.colors = tempColor;
                upgradeButton4.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            upgrade4ContractLvl.text = "x" + gc.savedData.GetRawMat4UpgradeLvl().ToString();
        }
        else
        {
            upgradeButton4.enabled = false;
            upgradeButton4.transform.parent.gameObject.SetActive(false);
        }
        //Get reputation level and compare it 
        if (gc.reputation.GetAvailableContractLevel("rawMat5") != -1)
        {
            upgradeButton5.enabled = true;
            upgradeButton5.transform.parent.gameObject.SetActive(true);
            var cost = ((int)(((upgrade5BaseCosts + (gc.savedData.GetRawMat5UpgradeLvl() * cost5PerLevel))) / GlobalMultipliers.ContractCostRedu));
            upgrade5CostText.text = ((int)cost).ToString();
            if (cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = upgradeButton5.colors;
                tempColor.pressedColor = Color.red;
                upgradeButton5.colors = tempColor;
                upgradeButton5.GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = upgradeButton5.colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                upgradeButton5.colors = tempColor;
                upgradeButton5.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            upgrade5ContractLvl.text = "x" + gc.savedData.GetRawMat5UpgradeLvl().ToString();
        }
        else
        {
            upgradeButton5.enabled = false;
            upgradeButton5.transform.parent.gameObject.SetActive(false);
        }
        //Get reputation level and compare it 
        if (gc.reputation.GetAvailableContractLevel("rawMat6") != -1)
        {
            upgradeButton6.enabled = true;
            upgradeButton6.transform.parent.gameObject.SetActive(true);
            var cost = ((int)(((upgrade6BaseCosts + (gc.savedData.GetRawMat6UpgradeLvl() * cost6PerLevel))) / GlobalMultipliers.ContractCostRedu));
            upgrade6CostText.text = ((int)cost).ToString();
            if (cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = upgradeButton6.colors;
                tempColor.pressedColor = Color.red;
                upgradeButton6.colors = tempColor;
                upgradeButton6.GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = upgradeButton6.colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                upgradeButton6.colors = tempColor;
                upgradeButton6.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            upgrade6ContractLvl.text = "x" + gc.savedData.GetRawMat6UpgradeLvl().ToString();
        }
        else
        {
            upgradeButton6.enabled = false;
            upgradeButton6.transform.parent.gameObject.SetActive(false);
        }
        //Get reputation level and compare it 
        if (gc.reputation.GetAvailableContractLevel("rawMat7") != -1)
        {
            upgradeButton7.enabled = true;
            upgradeButton7.transform.parent.gameObject.SetActive(true);
            var cost = ((int)(((upgrade7BaseCosts + (gc.savedData.GetRawMat7UpgradeLvl() * cost7PerLevel))) / GlobalMultipliers.ContractCostRedu));
            upgrade7CostText.text = ((int)cost).ToString();
            if (cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = upgradeButton7.colors;
                tempColor.pressedColor = Color.red;
                upgradeButton7.colors = tempColor;
                upgradeButton7.GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = upgradeButton7.colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                upgradeButton7.colors = tempColor;
                upgradeButton7.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            upgrade7ContractLvl.text = "x" + gc.savedData.GetRawMat7UpgradeLvl().ToString();
        }
        else
        {
            upgradeButton7.enabled = false;
            upgradeButton7.transform.parent.gameObject.SetActive(false);
        }
        //Get reputation level and compare it 
        if (gc.reputation.GetAvailableContractLevel("rawMat8") != -1)
        {
            upgradeButton8.enabled = true;
            upgradeButton8.transform.parent.gameObject.SetActive(true);
            var cost = ((int)(((upgrade8BaseCosts + (gc.savedData.GetRawMat8UpgradeLvl() * cost8PerLevel))) / GlobalMultipliers.ContractCostRedu));
            upgrade8CostText.text = ((int)cost).ToString();
            if (cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = upgradeButton8.colors;
                tempColor.pressedColor = Color.red;
                upgradeButton8.colors = tempColor;
                upgradeButton8.GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = upgradeButton8.colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                upgradeButton8.colors = tempColor;
                upgradeButton8.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            upgrade8ContractLvl.text = "x" + gc.savedData.GetRawMat8UpgradeLvl().ToString();
        }
        else
        {
            upgradeButton8.enabled = false;
            upgradeButton8.transform.parent.gameObject.SetActive(false);
        }
        //Get reputation level and compare it 
        if (gc.reputation.GetAvailableContractLevel("rawMat9") != -1)
        {
            upgradeButton9.enabled = true;
            upgradeButton9.transform.parent.gameObject.SetActive(true);
            var cost = ((int)(((upgrade9BaseCosts + (gc.savedData.GetRawMat9UpgradeLvl() * cost9PerLevel))) / GlobalMultipliers.ContractCostRedu));
            upgrade9CostText.text = ((int)cost).ToString();
            if (cost > gc.currencyManager.GetMoney())
            {
                ColorBlock tempColor = upgradeButton9.colors;
                tempColor.pressedColor = Color.red;
                upgradeButton9.colors = tempColor;
                upgradeButton9.GetComponent<OnClickSFX>().UpdateClip(errorSound);

            }
            else
            {
                ColorBlock tempColor = upgradeButton9.colors;
                tempColor.pressedColor = new Color32(200, 200, 200, 255);
                upgradeButton9.colors = tempColor;
                upgradeButton9.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            }
            upgrade9ContractLvl.text = "x" + gc.savedData.GetRawMat9UpgradeLvl().ToString();
        }
        else
        {
            upgradeButton9.enabled = false;
            upgradeButton9.transform.parent.gameObject.SetActive(false);
        }


    }


    public void OpenGiftShop()
    {
        DenyUpgrade();
        giftShopMenu.OpenMenu();
        upgradeShopCanvas.enabled = false;
        confirmationCanvas.enabled = false;
        Tenant.openUI = true;
    }


    #endregion
}

