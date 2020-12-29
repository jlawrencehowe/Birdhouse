using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using TMPro;

public class AdController : MonoBehaviour
{

    float moneyBoostTimer;
    float matBoostTimer;
    //test ID
    string testUnitID = "";
    //actual ID
    string rentUnitID = "";
    string matUnitID = "";

    private RewardedAd moneyAd, matAd;
    public Button moneyButton, matButton, exitButton;
    public TextMeshProUGUI moneyTimeText, matTimeText;
    public TextMeshProUGUI simpleTimerText;
    public GameObject moneyTimeObject, matTimeObject;
    public GameObject simpleTimer;
    public GameObject adConfimationCanvas, chooseAdMenu;
    private int chosenAd = -1;

    private GameController gc;

    void Start()
    {
        //MobileAds.Initialize(initStatus => { });
        MobileAds.Initialize("");
        //this.rewardedAd = new RewardedAd(testUnitID);
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        LoadNewAd();
    }

    private void Update()
    {
        if(moneyBoostTimer > 0)
        {
            moneyTimeObject.SetActive(true);
            int sec = (int)MoneyBoostTimer()%60;
            int min = (int)MoneyBoostTimer() / 60;
            int hour = min / 60;
            moneyTimeText.text = "" + hour.ToString("D2") + ":" + min.ToString("D2") + ":" + sec.ToString("D2");
            moneyButton.enabled = false;
        }
        else
        {
            moneyTimeObject.SetActive(false);
            moneyButton.enabled = true;
        }
        if(matBoostTimer > 0)
        {
            matTimeObject.SetActive(true);
            int sec = (int)MatBoostTimer() % 60;
            int min = (int)MatBoostTimer() / 60;
            int hour = min / 60;
            matTimeText.text = "" + hour.ToString("D2") + ":" + min.ToString("D2") + ":" + sec.ToString("D2");
            matButton.enabled = false;
        }
        else
        {
            matTimeObject.SetActive(false);
            matButton.enabled = true;
        }
        
        if(MoneyBoostTimer() != 0 || MatBoostTimer() != 0)
        {
            simpleTimer.SetActive(true);
            int time;
            if(MoneyBoostTimer() > MatBoostTimer() || MoneyBoostTimer() == 0)
            {
                time = (int)MatBoostTimer();
            }
            else
            {
                time = (int)MoneyBoostTimer();
            }
            string suf = "";
            int counter = 0;
            while (true)
            {
                if(time/60 <= 1)
                {

                    break;
                }
                else
                {
                    time = time / 60;
                    counter++;
                    if(counter == 1)
                    {
                        suf = "m";
                    }
                    else
                    {
                        suf = "h";
                    }
                }
            }
            simpleTimerText.text = time + suf;


        }
        else
        {
            simpleTimer.SetActive(false);
        }
    }

    public float MoneyBoostTimer()
    {
        return moneyBoostTimer;
    }

    public float MatBoostTimer()
    {
        return matBoostTimer;
    }

    public void UpdateMoneyBoostTimer(float time)
    {
        moneyBoostTimer -= time;
        if (moneyBoostTimer <= 0)
        {
            moneyBoostTimer = 0;
        }
    }

    public void UpdateMatBoostTimer(float time)
    {
        matBoostTimer -= time;
        if (matBoostTimer <= 0)
        {
            matBoostTimer = 0;
        }
    }

    public void SetMoneyBoostTimer(float time)
    {
        moneyBoostTimer = time;
        if (moneyBoostTimer <= 0)
        {
            moneyBoostTimer = 0;
        }
    }

    public void SetMatBoostTimer(float time)
    {
        matBoostTimer = time;
        if (matBoostTimer <= 0)
        {
            matBoostTimer = 0;
        }
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        if (args.Type == "money" && moneyBoostTimer <= 0)
        {
            SetMoneyBoostTimer(7200);
        }
        if (args.Type == "mat" && matBoostTimer <= 0)
        {
            SetMatBoostTimer(7200);
        }
    }


    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        LoadNewAd();
    }

    void LoadNewAd()
    {


        moneyAd = new RewardedAd(testUnitID);
        moneyAd.OnUserEarnedReward += HandleUserEarnedReward;
        moneyAd.OnAdClosed += HandleRewardedAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        moneyAd.LoadAd(request);

        matAd = new RewardedAd(testUnitID);
        //this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        matAd.OnUserEarnedReward += HandleUserEarnedReward;
        matAd.OnAdClosed += HandleRewardedAdClosed;
        request = new AdRequest.Builder().Build();
        matAd.LoadAd(request);
    }

    public void CreateAndLoadRewardedAd()
    {
        if (gc.tutorial == null)
        {

            Tenant.openUI = true;
            gc.gameUI.CloseAllMenus();
            //open ad canvas
            chooseAdMenu.SetActive(true);
        }

    }

    public void ChoseMoneyAd()
    {
        chosenAd = 1;
        adConfimationCanvas.SetActive(true);
        moneyButton.enabled = false;
        matButton.enabled = false;
        exitButton.enabled = false;
    }

    public void ChoseMatAd()
    {
        chosenAd = 2;
        adConfimationCanvas.SetActive(true);
        moneyButton.enabled = false;
        matButton.enabled = false;
        exitButton.enabled = false;
    }



    public void UserChoseToWatchAd()
    {
        if (chosenAd == 1 && moneyAd.IsLoaded())
        {
            moneyAd.Show();
        }
        else if (chosenAd == 2 && matAd.IsLoaded())
        {
            matAd.Show();
        }
        chooseAdMenu.SetActive(false);
        adConfimationCanvas.SetActive(false);
        moneyButton.enabled = true;
        matButton.enabled = true;
        exitButton.enabled = true;
    }

    public void DontWatchAd()
    {
        adConfimationCanvas.SetActive(false);
        moneyButton.enabled = true;
        matButton.enabled = true;
        exitButton.enabled = true;

    }


    public void CloseChooseMenu()
    {
        chooseAdMenu.SetActive(false);
        Tenant.openUI = false;
        moneyButton.enabled = true;
        matButton.enabled = true;
        exitButton.enabled = true;
    }


    public void CloseAll()
    {
        chooseAdMenu.SetActive(false);
        adConfimationCanvas.SetActive(false);
    }

}
