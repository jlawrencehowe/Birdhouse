using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI updateMoneyText;
    private float currentMoney;
    private float moneyChange;
    public RawMatMenu rawMatMenu;
    public InventoryMenu inventoryMenu;
    public ShopsMenu giftsMenu;
    public RawMatUpgradeMenu upgradeMenu;
    public BonusMenu bonusMenu;
    public AdController adController;
    public OptionsMenu optionsMenu;
    public Canvas giftConfirmationCanvas;
    public Canvas upgradeConfirmationCanvas;
    private GameController gc;
    public Image inventoryImage;
    public Image optionsImage;
    public Image shopImage;
    public Image bonusImage;
    float timeScaleAmount;
    bool lockTimeScaleUpdate = false;
    public enum ActiveMenu
    {
        shops, inventory, bonus, options, none
    }

    public ActiveMenu activeMenu;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        UpdateMoneyText(0);
        updateMoneyText.enabled = false;

    }

    void Update()
    {

        if (moneyChange != 0)
        {
            if (!lockTimeScaleUpdate)
            {
                timeScaleAmount = Mathf.Abs(moneyChange / 1.5f);
                if (timeScaleAmount < 50)
                {
                    timeScaleAmount = 50;
                }


                lockTimeScaleUpdate = true;
            }
            float localDeltaTime = Time.deltaTime * timeScaleAmount;
            if (moneyChange > 0)
            {
                moneyChange -= localDeltaTime;
                currentMoney += localDeltaTime;
                if (moneyChange <= 0)
                {
                    currentMoney = gc.currencyManager.GetMoney();
                    moneyChange = 0;
                    updateMoneyText.enabled = false;
                }
                else
                {

                    updateMoneyText.enabled = true;
                }
            }
            else if (moneyChange < 0)
            {
                moneyChange += localDeltaTime;
                currentMoney -= localDeltaTime;
                if (moneyChange >= 0)
                {
                    currentMoney = gc.currencyManager.GetMoney();
                    moneyChange = 0;
                    updateMoneyText.enabled = false;
                }
                else
                {

                    updateMoneyText.enabled = true;
                }
            }
            string tempString;
            if (moneyChange > 0)
            {
                tempString = "+";
            }
            else
            {
                tempString = "";
            }
            updateMoneyText.text = tempString + (int)moneyChange;
            moneyText.text = ((int)currentMoney).ToString("D9");
            MoneyPulse();
            if(currentMoney >= 999999999)
            {
                currentMoney = 999999999;
            }
        }
        else
        {
            moneyText.rectTransform.localScale = new Vector3(1, 1, 1);

            lockTimeScaleUpdate = false;
        }

        UpdateActiveMenuSize();


    }

    public void UpdateMoneyText(int amount)
    {
        moneyChange += amount;
        lockTimeScaleUpdate = false;

    }

    public void QuickUpdateMoney(int amount)
    {
        currentMoney = amount;
        moneyText.text = "" + ((int)currentMoney).ToString("D9"); ;
    }



    public void OpenRawMatMenu()
    {
        CloseAllMenus();
        activeMenu = ActiveMenu.inventory;
        Tenant.openUI = true;
        gc.touchScreen.scrollLock = true;
        rawMatMenu.OpenMenu();
    }

    public void OpenGiftMenu(Tenant tenant)
    {
        CloseAllMenus();
        activeMenu = ActiveMenu.inventory;
        Tenant.openUI = true;
        gc.touchScreen.scrollLock = true;
        inventoryMenu.OpenMenu(tenant);
    }

    public void OpenShops()
    {
        CloseAllMenus();
        activeMenu = ActiveMenu.shops;
        Tenant.openUI = true;
        gc.touchScreen.scrollLock = true;
        //update gift menu if time has passed while menu was open
        giftsMenu.OpenMenu();
    }

    public void OpenBonus()
    {
        CloseAllMenus();
        activeMenu = ActiveMenu.bonus;
        Tenant.openUI = true;
        gc.touchScreen.scrollLock = true;
        bonusMenu.enabled = true;
    }

    public void OpenOptions()
    {
        CloseAllMenus();
        activeMenu = ActiveMenu.options;
        Tenant.openUI = true;
        gc.touchScreen.scrollLock = true;
        optionsMenu.OpenMenu();
    }

    public void CloseAllMenus()
    {
        activeMenu = ActiveMenu.none;
        rawMatMenu.CloseMenu();
        inventoryMenu.CloseMenu();
        giftsMenu.CloseMenu();
        upgradeMenu.CloseMenu();
        bonusMenu.CloseMenu();
        optionsMenu.CloseMenu();
        adController.CloseAll();
        upgradeConfirmationCanvas.enabled = false;
        giftConfirmationCanvas.enabled = false;
        Tenant.openUI = false;
        gc.touchScreen.scrollLock = false;
        gc.tenantButtonHandler.HideTenantButtons();
    }

    private void MoneyPulse()
    {
        if (moneyText.rectTransform.localScale.x <= 1)
        {
            Vector3 tempScale = new Vector3(1.25f, 1.25f, 1);
            moneyText.rectTransform.localScale = tempScale;
        }
        else if (moneyText.rectTransform.localScale.x > 1)
        {
            Vector3 tempScale = moneyText.rectTransform.localScale;
            tempScale.x -= Time.deltaTime * 5;
            tempScale.y -= Time.deltaTime * 5;
            moneyText.rectTransform.localScale = tempScale;
        }
    }

    public void SetActiveMenu(ActiveMenu active)
    {
        activeMenu = active;
    }

    private void UpdateActiveMenuSize()
    {
        float scaleSpeed = 5;
        float maxScale = 1.25f;
        #region test
        /*
        if(activeMenu == ActiveMenu.inventory && inventoryImage.RectTransform.scale.x < maxScale)
        {
            var tempScale = inventoryImage.RectTransform.scale;
            tempScale.x += (2 * Time.deltaTime);
            tempScale.y += (2 * Time.deltaTime);
            if(tempScale.x >= maxScale)
            {
                tempScale.x = maxScale;
                tempScale.y = maxScale;
            }
            inventoryImage.RectTransform.scale = tempScale;
        }
        else if (inventoryImage.RectTransform.scale.x > 1)
        {
            var tempScale = inventoryImage.RectTransform.scale;
            tempScale.x -= (2 * Time.deltaTime);
            tempScale.y -= (2 * Time.deltaTime);
            if (tempScale.x <= 1)
            {
                tempScale.x = 1;
                tempScale.y = 1;
            }
            inventoryImage.RectTransform.scale = tempScale;
        }
        if (activeMenu == ActiveMenu.bonus && bonusImage.RectTransform.scale.x < maxScale)
        {
            var tempScale = bonusImage.RectTransform.scale;
            tempScale.x += (2 * Time.deltaTime);
            tempScale.y += (2 * Time.deltaTime);
            if (tempScale.x >= maxScale)
            {
                tempScale.x = maxScale;
                tempScale.y = maxScale;
            }
            bonusImage.RectTransform.scale = tempScale;
        }
        else if (bonusImage.RectTransform.scale.x > 1)
        {
            var tempScale = bonusImage.RectTransform.scale;
            tempScale.x -= (2 * Time.deltaTime);
            tempScale.y -= (2 * Time.deltaTime);
            if (tempScale.x <= 1)
            {
                tempScale.x = 1;
                tempScale.y = 1;
            }
            bonusImage.RectTransform.scale = tempScale;
        }
        if (activeMenu == ActiveMenu.options && optionsImage.RectTransform.scale.x < maxScale)
        {
            var tempScale = optionsImage.RectTransform.scale;
            tempScale.x += (2 * Time.deltaTime);
            tempScale.y += (2 * Time.deltaTime);
            if (tempScale.x >= maxScale)
            {
                tempScale.x = maxScale;
                tempScale.y = maxScale;
            }
            optionsImage.RectTransform.scale = tempScale;
        }
        else if (optionsImage.RectTransform.scale.x > 1)
        {
            var tempScale = optionsImage.RectTransform.scale;
            tempScale.x -= (2 * Time.deltaTime);
            tempScale.y -= (2 * Time.deltaTime);
            if (tempScale.x <= 1)
            {
                tempScale.x = 1;
                tempScale.y = 1;
            }
            optionsImage.RectTransform.scale = tempScale;
        }
        if (activeMenu == ActiveMenu.shops && shopImage.RectTransform.scale.x < maxScale)
        {
            var tempScale = shopImage.RectTransform.scale;
            tempScale.x += (2 * Time.deltaTime);
            tempScale.y += (2 * Time.deltaTime);
            if (tempScale.x >= maxScale)
            {
                tempScale.x = maxScale;
                tempScale.y = maxScale;
            }
            shopImage.RectTransform.scale = tempScale;
        }
        else if (shopImage.RectTransform.scale.x > 1)
        {
            var tempScale = shopImage.RectTransform.scale;
            tempScale.x -= (2 * Time.deltaTime);
            tempScale.y -= (2 * Time.deltaTime);
            if (tempScale.x <= 1)
            {
                tempScale.x = 1;
                tempScale.y = 1;
            }
            shopImage.RectTransform.scale = tempScale;
        }
        */
        #endregion
        Image activeObject;
        if (activeMenu == ActiveMenu.inventory)
        {
            activeObject = inventoryImage;
        }
        else if (activeMenu == ActiveMenu.bonus)
        {
            activeObject = bonusImage;
        }
        else if (activeMenu == ActiveMenu.options)
        {
            activeObject = optionsImage;
        }
        else if (activeMenu == ActiveMenu.shops)
        {
            activeObject = shopImage;
        }
        else
        {
            activeObject = null;
        }
        if (activeObject != null && activeObject.rectTransform.localScale.x < maxScale)
        {
            var tempScale = activeObject.rectTransform.localScale;
            tempScale.x += (2 * Time.deltaTime);
            tempScale.y += (2 * Time.deltaTime);
            if (tempScale.x >= maxScale)
            {
                tempScale.x = maxScale;
                tempScale.y = maxScale;
            }
            activeObject.rectTransform.localScale = tempScale;
        }

        if (activeObject != inventoryImage && inventoryImage.rectTransform.localScale.x > 1)
        {
            ReduceImageScale(scaleSpeed, inventoryImage);
        }
        if (activeObject != shopImage && shopImage.rectTransform.localScale.x > 1)
        {
            ReduceImageScale(scaleSpeed, shopImage);
        }
        if (activeObject != bonusImage && bonusImage.rectTransform.localScale.x > 1)
        {
            ReduceImageScale(scaleSpeed, bonusImage);
        }
        if (activeObject != optionsImage && optionsImage.rectTransform.localScale.x > 1)
        {
            ReduceImageScale(scaleSpeed, optionsImage);
        }
    }

    private void ReduceImageScale(float scaleSpeed, Image image)
    {
        var tempScale = image.rectTransform.localScale;
        tempScale.x -= (scaleSpeed * Time.deltaTime);
        tempScale.y -= (scaleSpeed * Time.deltaTime);
        if (tempScale.x <= 1)
        {
            tempScale.x = 1;
            tempScale.y = 1;
        }
        image.rectTransform.localScale = tempScale;
    }

    public void PulseInventoryImage()
    {
        var tempScale = new Vector3(1.25f, 1.25f, 1);
        inventoryImage.rectTransform.localScale = tempScale;
    }


}
