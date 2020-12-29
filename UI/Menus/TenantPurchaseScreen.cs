using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TenantPurchaseScreen : MonoBehaviour
{
    public Canvas purchaseScreen;
    public TextMeshProUGUI purchaseText;
    private int cost;
    private Tenant tenant;
    private GameController gc;
    public Button yesButton;
    public Button noButton;
    public AudioSource moveInAudio;
    public AudioClip errorSound, buttonPress;


    public void Awake()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void UpdateText(int cost)
    {
        purchaseText.text = "Would you like to purchase this tenant for " + cost + "?";
        this.cost = cost;

        if (cost > gc.currencyManager.GetMoney())
        {
            ColorBlock tempColor = yesButton.colors;
            tempColor.pressedColor = Color.red;
            yesButton.colors = tempColor;
            yesButton.GetComponent<OnClickSFX>().UpdateClip(errorSound);
            //yesButton.enabled = false;

        }
        else
        {
            ColorBlock tempColor = yesButton.colors;
            tempColor.pressedColor = new Color32(200, 200, 200, 255);
            yesButton.colors = tempColor;
            yesButton.GetComponent<OnClickSFX>().UpdateClip(buttonPress);
            //yesButton.enabled = true;
        }

    }

    public void AttachTenant(Tenant currentTenant)
    {
        tenant = currentTenant;
    }

    public void YesButton()
    {
        if (cost <= gc.currencyManager.GetMoney())
        {
            gc.currencyManager.AddMoney(-cost);
            tenant.SetIsUnlocked(true);
            moveInAudio.volume = PlayerPrefHandler.GetSFX();
            moveInAudio.Play();
            gc.touchScreen.scrollLock = false;
            gc.savedData.saveData();
            yesButton.enabled = false;
            Destroy(gameObject, 0.1f);
        }
    }

    public void NoButton()
    {
        gc.touchScreen.scrollLock = false;
        Destroy(gameObject, 0.1f);
    }

    private void OnDestroy()
    {
        Tenant.openUI = false;
    }


}