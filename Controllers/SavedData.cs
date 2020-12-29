using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class SavedData : MonoBehaviour
{


    private int rawMat1UpgradeLvl;
    private int rawMat2UpgradeLvl;
    private int rawMat3UpgradeLvl;
    private int rawMat4UpgradeLvl;
    private int rawMat5UpgradeLvl;
    private int rawMat6UpgradeLvl;
    private int rawMat7UpgradeLvl;
    private int rawMat8UpgradeLvl;
    private int rawMat9UpgradeLvl;

    private GameController gc;
    private int activeSave;

    Data save;


    void Start()
    {

        gc = GameObject.Find("GameController").GetComponent<GameController>();
        save = new Data();
        activeSave = 1;
        Load("save1");

    }


    public void GatherData()
    {
        #region legacy
        //save.currentTime = DateTime.UtcNow;
        /*
        save.giftShopTimer = gc.giftShopTimer;
        save.shopGifts = gc.gameUI.giftsMenu.CurrentShopGifts();
        save.reputation = gc.reputation.GetReputation();

        save.rawMat1UpgradeLvl = rawMat1UpgradeLvl;
        save.rawMat2UpgradeLvl = rawMat2UpgradeLvl;
        save.rawMat3UpgradeLvl = rawMat3UpgradeLvl;
        save.rawMat4UpgradeLvl = rawMat4UpgradeLvl;
        save.rawMat5UpgradeLvl = rawMat5UpgradeLvl;
        save.rawMat6UpgradeLvl = rawMat6UpgradeLvl;
        save.rawMat7UpgradeLvl = rawMat7UpgradeLvl;
        save.rawMat8UpgradeLvl = rawMat8UpgradeLvl;
        save.rawMat9UpgradeLvl = rawMat9UpgradeLvl;

        save.money = gc.currencyManager.GetMoney();
        save.rawMat1 = gc.currencyManager.GetRawMat1();
        save.rawMat2 = gc.currencyManager.GetRawMat2();
        save.rawMat3 = gc.currencyManager.GetRawMat3();
        save.rawMat4 = gc.currencyManager.GetRawMat4();
        save.rawMat5 = gc.currencyManager.GetRawMat5();
        save.rawMat6 = gc.currencyManager.GetRawMat6();
        save.rawMat7 = gc.currencyManager.GetRawMat7();
        save.rawMat8 = gc.currencyManager.GetRawMat8();
        save.rawMat9 = gc.currencyManager.GetRawMat9();
        save.rawMat10 = gc.currencyManager.GetRawMat10();
        save.rawMat11 = gc.currencyManager.GetRawMat11();
        save.rawMat12 = gc.currencyManager.GetRawMat12();
        save.rawMat13 = gc.currencyManager.GetRawMat13();
        save.rawMat14 = gc.currencyManager.GetRawMat14();

        save.invGifts = gc.inventory.GetShopGifts();

        save.tenantDatas = new List<Data.TenantData>();
        for (int i = 0; i < gc.tenants.Count; i++)
        {
            var tenant = gc.tenants[i].GetComponent<Tenant>();
            save.tenantDatas.Add(new Data.TenantData(tenant.gameObject.gameObject.activeSelf, tenant.GetIsUnlocked(), tenant.heldMoney, tenant.maxMoney, tenant.moneyPerSec,
                tenant.GetRequestLevel(), tenant.GetFavor(), tenant.isGenMat, tenant.heldMat, tenant.maxMat, tenant.matPerSec));
        }

        save.roofLoc = gc.reputation.roof.transform.position.y;
        save.maxTop = gc.touchScreen.GetTopMax();

        save.heldRawMat1 = gc.deliveryBird.heldRawMat1;
        save.maxRawMat1 = gc.deliveryBird.maxRawMat1;
        save.rawMat1PerSec = gc.deliveryBird.rawMat1PerSec;

        save.heldRawMat2 = gc.deliveryBird.heldRawMat2;
        save.maxRawMat2 = gc.deliveryBird.maxRawMat2;
        save.rawMat2PerSec = gc.deliveryBird.rawMat2PerSec;

        save.heldRawMat3 = gc.deliveryBird.heldRawMat3;
        save.maxRawMat3 = gc.deliveryBird.maxRawMat3;
        save.rawMat3PerSec = gc.deliveryBird.rawMat3PerSec;

        save.heldRawMat4 = gc.deliveryBird.heldRawMat4;
        save.maxRawMat4 = gc.deliveryBird.maxRawMat4;
        save.rawMat4PerSec = gc.deliveryBird.rawMat4PerSec;

        save.heldRawMat5 = gc.deliveryBird.heldRawMat5;
        save.maxRawMat5 = gc.deliveryBird.maxRawMat5;
        save.rawMat5PerSec = gc.deliveryBird.rawMat5PerSec;

        save.heldRawMat6 = gc.deliveryBird.heldRawMat6;
        save.maxRawMat6 = gc.deliveryBird.maxRawMat6;
        save.rawMat6PerSec = gc.deliveryBird.rawMat6PerSec;

        save.heldRawMat7 = gc.deliveryBird.heldRawMat7;
        save.maxRawMat7 = gc.deliveryBird.maxRawMat7;
        save.rawMat7PerSec = gc.deliveryBird.rawMat7PerSec;

        save.heldRawMat8 = gc.deliveryBird.heldRawMat8;
        save.maxRawMat8 = gc.deliveryBird.maxRawMat8;
        save.rawMat8PerSec = gc.deliveryBird.rawMat8PerSec;

        save.heldRawMat9 = gc.deliveryBird.heldRawMat9;
        save.maxRawMat9 = gc.deliveryBird.maxRawMat9;
        save.rawMat9PerSec = gc.deliveryBird.rawMat9PerSec;
        */
        #endregion

        //Tenants
        save.tenantDatas = new List<Data.TenantData>();
        for (int i = 0; i < gc.tenants.Count; i++)
        {
            var tenant = gc.tenants[i].GetComponent<Tenant>();
            save.tenantDatas.Add(new Data.TenantData(tenant.GetIsUnlocked(), tenant.heldMoney, tenant.GetRequestLevel(), tenant.GetFavor(), tenant.heldMat,
                tenant.GetIsTenantRequestActive(), tenant.GetRequestCompletionTime()));
        }

        //Delivery Bird
        save.heldRawMat1 = gc.deliveryBird.heldRawMat1;
        save.maxRawMat1 = gc.deliveryBird.maxRawMat1;
        save.rawMat1PerSec = gc.deliveryBird.rawMat1PerSec;

        save.heldRawMat2 = gc.deliveryBird.heldRawMat2;
        save.maxRawMat2 = gc.deliveryBird.maxRawMat2;
        save.rawMat2PerSec = gc.deliveryBird.rawMat2PerSec;

        save.heldRawMat3 = gc.deliveryBird.heldRawMat3;
        save.maxRawMat3 = gc.deliveryBird.maxRawMat3;
        save.rawMat3PerSec = gc.deliveryBird.rawMat3PerSec;

        save.heldRawMat4 = gc.deliveryBird.heldRawMat4;
        save.maxRawMat4 = gc.deliveryBird.maxRawMat4;
        save.rawMat4PerSec = gc.deliveryBird.rawMat4PerSec;

        save.heldRawMat5 = gc.deliveryBird.heldRawMat5;
        save.maxRawMat5 = gc.deliveryBird.maxRawMat5;
        save.rawMat5PerSec = gc.deliveryBird.rawMat5PerSec;

        save.heldRawMat6 = gc.deliveryBird.heldRawMat6;
        save.maxRawMat6 = gc.deliveryBird.maxRawMat6;
        save.rawMat6PerSec = gc.deliveryBird.rawMat6PerSec;

        save.heldRawMat7 = gc.deliveryBird.heldRawMat7;
        save.maxRawMat7 = gc.deliveryBird.maxRawMat7;
        save.rawMat7PerSec = gc.deliveryBird.rawMat7PerSec;

        save.heldRawMat8 = gc.deliveryBird.heldRawMat8;
        save.maxRawMat8 = gc.deliveryBird.maxRawMat8;
        save.rawMat8PerSec = gc.deliveryBird.rawMat8PerSec;

        save.heldRawMat9 = gc.deliveryBird.heldRawMat9;
        save.maxRawMat9 = gc.deliveryBird.maxRawMat9;
        save.rawMat9PerSec = gc.deliveryBird.rawMat9PerSec;

        //Currency Manager
        save.money = gc.currencyManager.GetMoney();
        save.rawMat1 = gc.currencyManager.GetRawMat1();
        save.rawMat2 = gc.currencyManager.GetRawMat2();
        save.rawMat3 = gc.currencyManager.GetRawMat3();
        save.rawMat4 = gc.currencyManager.GetRawMat4();
        save.rawMat5 = gc.currencyManager.GetRawMat5();
        save.rawMat6 = gc.currencyManager.GetRawMat6();
        save.rawMat7 = gc.currencyManager.GetRawMat7();
        save.rawMat8 = gc.currencyManager.GetRawMat8();
        save.rawMat9 = gc.currencyManager.GetRawMat9();
        save.rawMat10 = gc.currencyManager.GetRawMat10();
        save.rawMat11 = gc.currencyManager.GetRawMat11();
        save.rawMat12 = gc.currencyManager.GetRawMat12();
        save.rawMat13 = gc.currencyManager.GetRawMat13();
        save.rawMat14 = gc.currencyManager.GetRawMat14();

        //GC
        save.currentTime = DateTime.UtcNow.ToString();
        save.giftShopTimer = gc.giftShopTimer;
        save.moneyBoostTimer = gc.adController.MoneyBoostTimer();
        save.matBoostTimer = gc.adController.MatBoostTimer();

        //Saved Data
        save.rawMat1UpgradeLvl = rawMat1UpgradeLvl;
        save.rawMat2UpgradeLvl = rawMat2UpgradeLvl;
        save.rawMat3UpgradeLvl = rawMat3UpgradeLvl;
        save.rawMat4UpgradeLvl = rawMat4UpgradeLvl;
        save.rawMat5UpgradeLvl = rawMat5UpgradeLvl;
        save.rawMat6UpgradeLvl = rawMat6UpgradeLvl;
        save.rawMat7UpgradeLvl = rawMat7UpgradeLvl;
        save.rawMat8UpgradeLvl = rawMat8UpgradeLvl;
        save.rawMat9UpgradeLvl = rawMat9UpgradeLvl;

        //Inventory
        save.invGifts = gc.inventory.GetShopGifts();

        //Reputation
        save.reputation = gc.reputation.reputation;

        //Tutorial
        if (gc.tutorial == null)
        {
            save.tutorialState = Tutorial.TutorialState.finished;
        }
        else
        {
            save.tutorialState = gc.tutorial.GetTutorialState();
        }

        //Shop Menu
        save.shopGifts = gc.gameUI.giftsMenu.CurrentShopGifts();




    }

    public void saveData()
    {
        string dataFileName;
        if(activeSave == 1)
        {
            dataFileName = "save2";
            activeSave = 2;
        }
        else
        {
            dataFileName = "save1";
            activeSave = 1;
        }
        if (gc == null)
        {
            gc = GameObject.Find("GameController").GetComponent<GameController>();
        }
        if (gc.tutorial == null)
        {
            GatherData();
            string tempPath = Path.Combine(Application.persistentDataPath, "data");
            tempPath = Path.Combine(tempPath, dataFileName + ".txt");

            //Convert To Json then to bytes
            string jsonData = JsonUtility.ToJson(save, true);
            byte[] jsonByte = Encoding.ASCII.GetBytes(jsonData);

            //Create Directory if it does not exist
            if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
            }
            //Debug.Log(path);

            try
            {
                File.WriteAllBytes(tempPath, jsonByte);
                Debug.Log("Saved Data to: " + tempPath.Replace("/", "\\"));
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed To PlayerInfo Data to: " + tempPath.Replace("/", "\\"));
                Debug.LogWarning("Error: " + e.Message);
            }
        }
    }

    public void Load(string dataFileName)
    {
        DateTime currentTime = DateTime.UtcNow;

        //save = loadData<Data>(dataFileName);
        SavedData.Data save1 = loadData<Data>("save1");
        SavedData.Data save2 = loadData<Data>("save2");
        if(save1 == null && save2 != null)
        {
            save = save2;
            activeSave = 2;
        }
        else if(save1 != null && save2 == null)
        {
            save = save1;
            activeSave = 1;
        }
        else if(save1 != null && save2 != null)
        {
            if(DateTime.Parse(save1.currentTime) > DateTime.Parse(save2.currentTime))
            {
                save = save1;
                activeSave = 1;
            }
            else
            {
                save = save2;
                activeSave = 2;
            }
        }
        else
        {
            save = new Data();
            gc.tutorial.InitialState(Tutorial.TutorialState.ready);
            Debug.Log("save is null");
            return;
        }

        Debug.Log("Active Save: " + activeSave);


        #region Tenants
        var timeDifference = currentTime - DateTime.Parse(save.currentTime);
        for (int i = 0; i < save.tenantDatas.Count; i++)
        {
            var tempTenant = gc.tenants[i].GetComponent<Tenant>();
            tempTenant.LoadFavor(save.tenantDatas[i].favor);
            tempTenant.LoadReputationLevel(save.tenantDatas[i].requestLvl);
            tempTenant.heldMoney = save.tenantDatas[i].heldMoney;
            tempTenant.SetIsUnlocked(save.tenantDatas[i].tenantUnlocked);
            //tempTenant.currentRequest = save.tenantDatas[i].requestLvl;
            if (save.tenantDatas[i].isActiveRequest)
            {
                tempTenant.LoadRequestTimer(save.tenantDatas[i].completionTime - (float)timeDifference.TotalSeconds);
            }

            //tempTenant.heldMat = save.tenantDatas[i].heldMat;
            /*
            bool loop = true;
            int breakLimit = 0;
            while (loop)
            {
                breakLimit++;
                if (breakLimit > 10)
                {
                    break;
                }
                loop = tempTenant.CheckFavorLevel();
            }
            */
        }
        #endregion

        #region SavedData
        rawMat1UpgradeLvl = save.rawMat1UpgradeLvl;
        rawMat2UpgradeLvl = save.rawMat2UpgradeLvl;
        rawMat3UpgradeLvl = save.rawMat3UpgradeLvl;
        rawMat4UpgradeLvl = save.rawMat4UpgradeLvl;
        rawMat5UpgradeLvl = save.rawMat5UpgradeLvl;
        rawMat6UpgradeLvl = save.rawMat6UpgradeLvl;
        rawMat7UpgradeLvl = save.rawMat7UpgradeLvl;
        rawMat8UpgradeLvl = save.rawMat8UpgradeLvl;
        rawMat9UpgradeLvl = save.rawMat9UpgradeLvl;
        #endregion

        #region DeliveryBird
        gc.deliveryBird.heldRawMat1 = save.heldRawMat1;
        gc.deliveryBird.heldRawMat2 = save.heldRawMat2;
        gc.deliveryBird.heldRawMat3 = save.heldRawMat3;
        gc.deliveryBird.heldRawMat4 = save.heldRawMat4;
        gc.deliveryBird.heldRawMat5 = save.heldRawMat5;
        gc.deliveryBird.heldRawMat6 = save.heldRawMat6;
        gc.deliveryBird.heldRawMat7 = save.heldRawMat7;
        gc.deliveryBird.heldRawMat8 = save.heldRawMat8;
        gc.deliveryBird.heldRawMat9 = save.heldRawMat9;
        gc.deliveryBird.FullUpdate();
        #endregion

        #region CurrencyManager
        gc.currencyManager.QuickAddMoney(save.money);
        gc.currencyManager.AddRawMat1(save.rawMat1);
        gc.currencyManager.AddRawMat2(save.rawMat2);
        gc.currencyManager.AddRawMat3(save.rawMat3);
        gc.currencyManager.AddRawMat4(save.rawMat4);
        gc.currencyManager.AddRawMat5(save.rawMat5);
        gc.currencyManager.AddRawMat6(save.rawMat6);
        gc.currencyManager.AddRawMat7(save.rawMat7);
        gc.currencyManager.AddRawMat8(save.rawMat8);
        gc.currencyManager.AddRawMat9(save.rawMat9);
        #endregion

        #region Ad Controller
        gc.adController.SetMoneyBoostTimer(save.moneyBoostTimer - (float)timeDifference.TotalSeconds);
        gc.adController.SetMatBoostTimer(save.matBoostTimer - (float)timeDifference.TotalSeconds);

        #endregion


        #region ShopMenu
        gc.gameUI.giftsMenu.LoadGifts(save.shopGifts);
        #endregion

        #region GC
        gc.UpdateTenantMoney((int)timeDifference.TotalSeconds);
        gc.UpdateDeliveryBird((int)timeDifference.TotalSeconds);
        gc.giftShopTimer = 300 - (float)timeDifference.TotalSeconds;


        #endregion



        #region Inventory
        gc.inventory.LoadSaveData(save.invGifts);
        #endregion

        #region Reputation
        gc.reputation.QuickAddReputation(save.reputation);
        #endregion

        #region Tutorial
        gc.tutorial.InitialState(save.tutorialState);
        #endregion




    }

    //Load Data
    private static T loadData<T>(string dataFileName)
    {
        string tempPath = Path.Combine(Application.persistentDataPath, "data");
        tempPath = Path.Combine(tempPath, dataFileName + ".txt");

        //Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
        {
            Debug.LogWarning("Directory does not exist");
            return default(T);
        }

        if (!File.Exists(tempPath))
        {
 
            return default(T);
        }

        //Load saved Json
        byte[] jsonByte = null;
        try
        {
            jsonByte = File.ReadAllBytes(tempPath);
            Debug.Log("Loaded Data from: " + tempPath.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Load Data from: " + tempPath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }

        //Convert to json string
        string jsonData = Encoding.ASCII.GetString(jsonByte);

        try
        {
            //Convert to Object
            object resultValue = JsonUtility.FromJson<T>(jsonData);
            return (T)Convert.ChangeType(resultValue, typeof(T));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Load Data from: " + tempPath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
            return default(T);
        }
    }

    public static bool deleteData(string dataFileName)
    {
        bool success = false;

        //Load Data
        string tempPath = Path.Combine(Application.persistentDataPath, "data");
        tempPath = Path.Combine(tempPath, dataFileName + ".txt");

        //Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
        {
            Debug.LogWarning("Directory does not exist");
            return false;
        }

        if (!File.Exists(tempPath))
        {
            Debug.Log("File does not exist");
            return false;
        }

        try
        {
            File.Delete(tempPath);
            Debug.Log("Data deleted from: " + tempPath.Replace("/", "\\"));
            success = true;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Delete Data: " + e.Message);
        }

        return success;
    }



    #region rawMatUpgradeLvls
    public int GetRawMat1UpgradeLvl()
    {
        return rawMat1UpgradeLvl;
    }

    public void SetRawMat1UpgradeLvl(int upgradeLvl)
    {
        rawMat1UpgradeLvl = upgradeLvl;
    }

    public int GetRawMat2UpgradeLvl()
    {
        return rawMat2UpgradeLvl;
    }

    public void SetRawMat2UpgradeLvl(int upgradeLvl)
    {
        rawMat2UpgradeLvl = upgradeLvl;
    }

    public int GetRawMat3UpgradeLvl()
    {
        return rawMat3UpgradeLvl;
    }

    public void SetRawMat3UpgradeLvl(int upgradeLvl)
    {
        rawMat3UpgradeLvl = upgradeLvl;
    }

    public int GetRawMat4UpgradeLvl()
    {
        return rawMat4UpgradeLvl;
    }

    public void SetRawMat4UpgradeLvl(int upgradeLvl)
    {
        rawMat4UpgradeLvl = upgradeLvl;
    }

    public int GetRawMat5UpgradeLvl()
    {
        return rawMat5UpgradeLvl;
    }

    public void SetRawMat5UpgradeLvl(int upgradeLvl)
    {
        rawMat5UpgradeLvl = upgradeLvl;
    }

    public int GetRawMat6UpgradeLvl()
    {
        return rawMat6UpgradeLvl;
    }

    public void SetRawMat6UpgradeLvl(int upgradeLvl)
    {
        rawMat6UpgradeLvl = upgradeLvl;
    }

    public int GetRawMat7UpgradeLvl()
    {
        return rawMat7UpgradeLvl;
    }

    public void SetRawMat7UpgradeLvl(int upgradeLvl)
    {
        rawMat7UpgradeLvl = upgradeLvl;
    }

    public int GetRawMat8UpgradeLvl()
    {
        return rawMat8UpgradeLvl;
    }

    public void SetRawMat8UpgradeLvl(int upgradeLvl)
    {
        rawMat8UpgradeLvl = upgradeLvl;
    }

    public int GetRawMat9UpgradeLvl()
    {
        return rawMat9UpgradeLvl;
    }

    public void SetRawMat9UpgradeLvl(int upgradeLvl)
    {
        rawMat9UpgradeLvl = upgradeLvl;
    }


    public int GetRawMatLvlByID(int id)
    {
        if(id == 1)
        {
            return rawMat1UpgradeLvl;
        }
        if (id == 2)
        {
            return rawMat2UpgradeLvl;
        }
        if (id == 3)
        {
            return rawMat3UpgradeLvl;
        }
        if (id == 4)
        {
            return rawMat4UpgradeLvl;
        }
        if (id == 5)
        {
            return rawMat5UpgradeLvl;
        }
        if (id == 6)
        {
            return rawMat6UpgradeLvl;
        }
        if (id == 7)
        {
            return rawMat7UpgradeLvl;
        }
        if (id == 8)
        {
            return rawMat8UpgradeLvl;
        }
        if (id == 9)
        {
            return rawMat9UpgradeLvl;
        }
        return 0;
    }

    #endregion




    [Serializable]
    public class Data
    {


        //Tenants
        public List<TenantData> tenantDatas = new List<TenantData>();
        [Serializable]
        public struct TenantData
        {
            public bool tenantUnlocked;
            public float heldMoney;
            public int requestLvl;
            public int favor;
            public int heldMat;
            public bool isActiveRequest;
            public float completionTime;

            public TenantData(bool tenantUnlocked, float heldMoney, int requestLvl,
                int favor, int heldMat, bool isActiveRequest, float completionTime)
            {
                this.tenantUnlocked = tenantUnlocked;
                this.heldMoney = heldMoney;
                this.requestLvl = requestLvl;
                this.favor = favor;
                this.heldMat = heldMat;
                this.isActiveRequest = isActiveRequest;
                this.completionTime = completionTime;
            }

        }

        //Delivery Bird
        public int heldRawMat1;
        public int maxRawMat1;
        public int rawMat1PerSec;

        public int heldRawMat2;
        public int maxRawMat2;
        public int rawMat2PerSec;

        public int heldRawMat3;
        public int maxRawMat3;
        public int rawMat3PerSec;

        public int heldRawMat4;
        public int maxRawMat4;
        public int rawMat4PerSec;

        public int heldRawMat5;
        public int maxRawMat5;
        public int rawMat5PerSec;

        public int heldRawMat6;
        public int maxRawMat6;
        public int rawMat6PerSec;

        public int heldRawMat7;
        public int maxRawMat7;
        public int rawMat7PerSec;

        public int heldRawMat8;
        public int maxRawMat8;
        public int rawMat8PerSec;

        public int heldRawMat9;
        public int maxRawMat9;
        public int rawMat9PerSec;

        //currency manager
        public int money;
        public int rawMat1;
        public int rawMat2;
        public int rawMat3;
        public int rawMat4;
        public int rawMat5;
        public int rawMat6;
        public int rawMat7;
        public int rawMat8;
        public int rawMat9;
        public int rawMat10;
        public int rawMat11;
        public int rawMat12;
        public int rawMat13;
        public int rawMat14;

        //gc
        public float giftShopTimer;
        public string currentTime;

        //SavedData
        public int rawMat1UpgradeLvl;
        public int rawMat2UpgradeLvl;
        public int rawMat3UpgradeLvl;
        public int rawMat4UpgradeLvl;
        public int rawMat5UpgradeLvl;
        public int rawMat6UpgradeLvl;
        public int rawMat7UpgradeLvl;
        public int rawMat8UpgradeLvl;
        public int rawMat9UpgradeLvl;

        //inventory
        public List<ShopsMenu.shopGift> invGifts;

        //repuation
        public int reputation = 0;

        //shop menu
        public List<ShopsMenu.shopGift> shopGifts;

        

        //ad timers
        public float moneyBoostTimer = 0;
        public float matBoostTimer = 0;


        //Things left to save

        //available contracts

        //tutorial state
        public Tutorial.TutorialState tutorialState = Tutorial.TutorialState.ready;


    }



}

