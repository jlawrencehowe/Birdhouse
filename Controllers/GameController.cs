using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;

public class GameController : MonoBehaviour
{

    public List<GameObject> tenants;
    private List<Tenant> tenantsScripts;
    public DeliveryBird deliveryBird;
    public CurrencyManager currencyManager;
    public SavedData savedData;
    public Inventory inventory;
    public GameUI gameUI;
    public Reputation reputation;
    public TouchScreen touchScreen;
    public TenantButtonHandler tenantButtonHandler;
    public float giftShopTimer;
    public Camera mainCam;
    public Image collectButton;
    private Animator collectAnim;
    private GlobalMultipliers globalMultipliers;
    public AdController adController;
    public Tutorial tutorial;
    public MusicPlayer musicPlayer;
    public NotificationHandler notificationHandler;

    private int tenantNotifID;
    private int adNotifID;
    // Start is called before the first frame update
    void Start()
    {

        currencyManager = GameObject.Find("CurrencyManager").GetComponent<CurrencyManager>();
        gameUI = GameObject.Find("UI").GetComponent<GameUI>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        deliveryBird = GameObject.Find("DeliveryBird").GetComponent<DeliveryBird>();
        savedData = GameObject.Find("SavedData").GetComponent<SavedData>();
        touchScreen = GameObject.Find("TouchScreen").GetComponent<TouchScreen>();
        tenantButtonHandler = GameObject.Find("TenantButtons").GetComponent<TenantButtonHandler>();
        collectButton = GameObject.Find("CollectAllButton").GetComponent<Image>();
        adController = GameObject.Find("AdController").GetComponent<AdController>();
        tutorial = GameObject.Find("Tutorial").GetComponent<Tutorial>();
        musicPlayer = GameObject.Find("MusicPlayer").GetComponent<MusicPlayer>();
        InvokeRepeating("UpdateTenantMoney", 1f, 1f);
        InvokeRepeating("UpdateDeliveryBird", 60f, 60f);
        reputation = GameObject.Find("Reputation").GetComponent<Reputation>();
        notificationHandler = GameObject.Find("NotificationHandler").GetComponent<NotificationHandler>();
        giftShopTimer = 300;
        collectAnim = collectButton.GetComponent<Animator>();
        //tenants = new List<GameObject>();
        InvokeRepeating("UpdateCollectAll", 10, 10);
        tenantsScripts = new List<Tenant>();
        globalMultipliers = new GlobalMultipliers();
        for (int i = 0; i < tenants.Count; i++)
        {
            tenantsScripts.Add(tenants[i].GetComponent<Tenant>());
        }

        //InvokeRepeating("SaveGame", 60f, 60f);
        //Invoke("DelayedLoad", 0.5f);
        /*
        var c = new AndroidNotificationChannel()
        {
            Id = "1",
            Name = "TenantCollectReady",
            Importance = Importance.High,
            Description = "Tenant Rent is ready to collect",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);

        var d = new AndroidNotificationChannel()
        {
            Id = "2",
            Name = "AdCountDown",
            Importance = Importance.High,
            Description = "Ad timer has runout",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(d);

        var t = new AndroidNotificationChannel()
        {
            Id = "3",
            Name = "Test",
            Importance = Importance.High,
            Description = "DescrTest",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(t);

        */
    }

    // Update is called once per frame
    void Update()
    {


        giftShopTimer -= Time.deltaTime;
        if (giftShopTimer <= 0)
        {
            gameUI.giftsMenu.SetNewItems();
            giftShopTimer = 300;
        }

       
    }

    
    void OnApplicationPause(bool pauseStatus)
    {
        /*
        if (pauseStatus)
        {
            int maxTime = 0;
            for (int i = 0; i < tenants.Count; i++)
            {
                var tempTen = tenants[i].GetComponent<Tenant>();
                int moneyNeeded = (int)(tempTen.maxMoney - tempTen.heldMoney);
                int time = moneyNeeded / (int)(tempTen.moneyPerSec * GlobalMultipliers.MoneyGen * 60);
                if (time > maxTime)
                {
                    maxTime = time;
                }



            }
            
            var notification = new AndroidNotification();
            notification.Title = "Rent is ready!";
            notification.Text = "Tenant rent is ready for collection. Come collect it so you can generate more!";
            notification.FireTime = System.DateTime.Now.AddMinutes(maxTime);
            tenantNotifID = AndroidNotificationCenter.SendNotification(notification, "1");
            

           
        }
        else
        {
            //AndroidNotificationCenter.CancelAllNotifications();

        }*/
        if(pauseStatus)
            savedData.saveData();
    }


    public int GenerateMaxTime()
    {
        int maxTime = 0;
        for (int i = 0; i < tenants.Count; i++)
        {

            var tempTen = tenants[i].GetComponent<Tenant>();
            if (tempTen.GetIsUnlocked())
            {
                int moneyNeeded = (int)(tempTen.maxMoney - tempTen.heldMoney);
                int time = moneyNeeded / (int)(tempTen.moneyPerSec * GlobalMultipliers.MoneyGen * 60);
                if (time > maxTime)
                {
                    maxTime = time;
                }
            }



        }

        return maxTime;
    }
    /*
    public void RequestNotificationCreate(int time)
    {
        var notification = new AndroidNotification();
        notification.Title = "Request finished!";
        notification.Text = "A request has finished. See what the tenant has to say!";
        notification.FireTime = System.DateTime.Now.AddMinutes(time);

        AndroidNotificationCenter.SendNotification(notification, "2");
    }

    public void ClearRequestNotification()
    {
        AndroidNotificationCenter.CancelNotification(2);
    }
    */
    private void SaveGame()
    {
        savedData.saveData();
    }



    private void UpdateTenantMoney()
    {

        float time = 1;
        if (adController.MoneyBoostTimer() > 1)
        {
            adController.UpdateMoneyBoostTimer(1);
            time = 1.5f;
        }
        foreach (var tenant in tenants)
        {
            if (tenant.GetComponent<Tenant>().GetIsUnlocked())
            {

                tenant.GetComponent<Tenant>().UpdateMoney(time);
                //Debug.Log("UpdatedTenant");

            }
        }
    }

    public void UpdateTenantMoney(int time)
    {
        float adTimer;
        if (time > adController.MoneyBoostTimer())
        {
            adTimer = adController.MoneyBoostTimer();
            time -= (int)adController.MoneyBoostTimer();
            adController.UpdateMoneyBoostTimer(adController.MoneyBoostTimer());
        }
        else
        {
            adController.UpdateMoneyBoostTimer(time);
            adTimer = time;
            time = 0;
        }

        foreach (var tenant in tenants)
        {
            if (tenant.GetComponent<Tenant>().GetIsUnlocked())
            {
                tenant.GetComponent<Tenant>().UpdateMoney(time);
                tenant.GetComponent<Tenant>().UpdateMoney(adTimer * 1.5f);
                //Debug.Log("UpdatedTenant");

            }
        }
    }

    private void UpdateDeliveryBird()
    {

        float time = 1;
        if (adController.MatBoostTimer() > 1)
        {
            adController.UpdateMatBoostTimer(1);
            time = 1.5f;
        }
        if (deliveryBird != null && deliveryBird.GetIsUnlocked())
        {
            deliveryBird.UpdateRawMaterials(time);
        }
    }

    public void UpdateDeliveryBird(int time)
    {
        float adTimer;
        if (time > adController.MatBoostTimer())
        {
            adTimer = adController.MatBoostTimer();
            time -= (int)adController.MatBoostTimer();
            adController.UpdateMatBoostTimer(adController.MoneyBoostTimer());
        }
        else
        {
            adController.UpdateMatBoostTimer(time);
            adTimer = time;
            time = 0;
        }

        if (deliveryBird != null && deliveryBird.GetIsUnlocked())
        {
            deliveryBird.UpdateRawMaterials(time/60);
            deliveryBird.UpdateRawMaterials(adTimer * 1.5f/60);
        }
    }

    public void CollectAll()
    {
        foreach (var tenant in tenants)
        {
            if (tenant.GetComponent<Tenant>().GetIsUnlocked())
            {
                //tenant.GetComponent<Tenant>().CollectMoney(mainCam.ScreenToWorldPoint(collectButton.rectTransform.position));
                tenant.GetComponent<Tenant>().CollectMoney(collectButton.rectTransform.position);

            }
        }

        //deliveryBird.CollectRawMaterials(mainCam.ScreenToWorldPoint(collectButton.rectTransform.position));
        deliveryBird.CollectRawMaterials(collectButton.rectTransform.position);
        UpdateCollectAll();
        savedData.saveData();
    }

    public bool CheckAllTenantMax()
    {
        bool allFull = true;
        foreach (var tenant in tenantsScripts)
        {
            if (tenant.GetIsUnlocked() && tenant.heldMoney != tenant.maxMoney)
            {
                allFull = false;
                break;
            }
            if (tenant.isGenMat && tenant.heldMat != tenant.maxMat)
            {
                allFull = false;
                break;
            }
        }
        return allFull;
        //if allFull then bounce, else don't
    }

    private void UpdateCollectAll()
    {
        if (deliveryBird.currentEveryItem == deliveryBird.maxEveryItem && CheckAllTenantMax())
        {
            collectAnim.SetBool("bounce", true);
            //Debug.Log("true");

        }
        else
        {
            collectAnim.SetBool("bounce", false);
            //Debug.Log("false");
        }
    }

    public int GetUnlockedTenantAmount()
    {
        int counter = 0;
        foreach (var tenant in tenants)
        {
            if (tenant.GetComponent<Tenant>().GetIsUnlocked())
            {
                counter++;
            }
        }
        return counter;
    }

    private void DelayedLoad()
    {

    }

    public void ResetGiftTimer()
    {
        giftShopTimer = 0;
    }

}

