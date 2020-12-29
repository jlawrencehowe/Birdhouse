using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TenantButtonHandler : MonoBehaviour
{
    public TenantButton currencyButton, requestButton, giftButton;
    public TenantExitButton exitButton;
    public TextMeshPro birdName, moneyPerSecText, matPerSecText;
    public SpriteRenderer matSpriteR;
    public GameObject requestMessage;
    private bool requestMessageIsActive;


    public void Start()
    {

    }

    public void HideTenantButtons()
    {
        this.transform.position = new Vector3(10000, 10000, this.transform.position.z);
    }

    public void InitText(string birdName, string moneyPerSec)
    {
        this.birdName.text = birdName;
        moneyPerSecText.text = moneyPerSec;
        //currencyButton.gameObject.SetActive(false);
        //requestButton.gameObject.SetActive(false);
        //giftButton.gameObject.SetActive(false);
        matPerSecText.gameObject.SetActive(false);
        matSpriteR.gameObject.SetActive(false);
    }

    public void InitText(string birdName, string moneyPerSec, string matPerSec, Sprite matSprite, bool isGenMat)
    {
        //currencyButton.gameObject.SetActive(true);
        requestButton.gameObject.SetActive(true);
        giftButton.gameObject.SetActive(true);
        //matPerSecText.gameObject.SetActive(true);
        matSpriteR.gameObject.SetActive(true);
        this.birdName.text = birdName;
        moneyPerSecText.text = moneyPerSec;
        //matPerSecText.text = matPerSec;
        matSpriteR.sprite = matSprite;

        if (isGenMat)
        {
            matSpriteR.gameObject.SetActive(true);
            //matPerSecText.gameObject.SetActive(true);
        }
        else
        {
            matSpriteR.gameObject.SetActive(false);
            //matPerSecText.gameObject.SetActive(false);
        }
    }

    public void DisableButtons()
    {
        //currencyButton.SetDisableClick(true);
        if (requestMessageIsActive)
            requestButton.SetDisableClick(true);
        giftButton.SetDisableClick(true);
    }

    public void EnableButtons()
    {
        //currencyButton.SetDisableClick(false);
        requestButton.SetDisableClick(false);
        giftButton.SetDisableClick(false);
    }

    public void DisableRequestButton()
    {
        requestButton.SetDisableClick(true);
        requestButton.GetComponent<Renderer>().material.SetFloat("_Slide", 0);
    }

    public void EnableRequestButton()
    {
        requestButton.SetDisableClick(false);
        requestButton.GetComponent<Renderer>().material.SetFloat("_Slide", 1);
    }

    public void DisableGiftButton()
    {
        giftButton.SetDisableClick(true);
    }

    public void EnableGiftButton()
    {
        giftButton.SetDisableClick(false);
    }

    public void DisableCurrencyButton()
    {
        currencyButton.SetDisableClick(true);
    }

    public void EnableCurrencyButton()
    {
        currencyButton.SetDisableClick(false);
    }

    public void DisabledExitButton()
    {
        exitButton.SetDisableClick(true);
    }

    public void EnableExitButton()
    {
        exitButton.SetDisableClick(false);
    }

    public void ActivateRequestMessage(bool isActive)
    {
        requestMessage.SetActive(isActive);
        requestButton.SetDisableClick(isActive);
        requestMessageIsActive = isActive;
    }

}
