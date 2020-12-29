using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryBird : MonoBehaviour
{
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

    public List<Sprite> listOfMatImages;
    public GameController gc;
    public bool isUnlocked = true;

    public TenantButton.ButtonMethod passedButtonMethod;
    public TenantButton currencyCollectButton;
    public GameObject flyingMat;
    private Image inventoryImage;

    public GameObject currencyCollectFill;
    private Vector3 currencyCollectStartPos;
    public float maxEveryItem, currentEveryItem;

    public Renderer collectRenderer;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        listOfMatImages = gc.gameUI.rawMatMenu.itemSprites;
        passedButtonMethod = CollectRawMaterials;
        currencyCollectButton.SetFunction(passedButtonMethod);
        inventoryImage = GameObject.Find("InventoryButton").GetComponent<Image>();
        currencyCollectStartPos = currencyCollectFill.transform.localPosition;
        InvokeRepeating("UpdateCurrencyCollect", 0, 1);
        maxEveryItem = maxRawMat1 + maxRawMat2 + maxRawMat3 + maxRawMat4 + maxRawMat5 + maxRawMat6 + maxRawMat7 + maxRawMat8 + maxRawMat9;
    }

    private void Update()
    {
        currencyCollectButton.disableClick = Tenant.openUI;
    }


    private void UpdateCurrencyCollect()
    {
        currentEveryItem = heldRawMat1 + heldRawMat2 + heldRawMat3 + heldRawMat4 + heldRawMat5 + heldRawMat6 + heldRawMat7 + heldRawMat8 + heldRawMat9;
        if (currentEveryItem != 0 && maxEveryItem != 0)
            collectRenderer.material.SetFloat("_Slide", currentEveryItem / maxEveryItem);
        else
            collectRenderer.material.SetFloat("_Slide", 0);


        /*if (currentEveryItem != 0 && maxEveryItem != 0)
            currencyCollectFill.transform.localPosition = Vector3.Lerp(currencyCollectStartPos, new Vector3(0, 0, -1), currentEveryItem / maxEveryItem);
        else
        {
            currencyCollectFill.transform.localPosition = Vector3.Lerp(currencyCollectStartPos, new Vector3(0, 0, -1), 0);
        }*/
    }


    public void UpdateRawMaterials(float passedTime)
    {
        heldRawMat1 += (int)(rawMat1PerSec * passedTime * GlobalMultipliers.Mat1Mult * GlobalMultipliers.MatGen);
        int tempMax = (int)(maxRawMat1 * GlobalMultipliers.MatHeld);
        //Debug.Log("Raw Mat MAX: " + tempMax);
        if (heldRawMat1 > tempMax)
        {
            heldRawMat1 = tempMax;
        }
        heldRawMat2 += (int)(rawMat2PerSec * passedTime * GlobalMultipliers.Mat2Mult * GlobalMultipliers.MatGen);
        tempMax = (int)(maxRawMat2 * GlobalMultipliers.MatHeld);
        if (heldRawMat2 > tempMax)
        {
            heldRawMat2 = tempMax;
        }
        heldRawMat3 += (int)(rawMat3PerSec * passedTime * GlobalMultipliers.Mat3Mult * GlobalMultipliers.MatGen);
        tempMax = (int)(maxRawMat3 * GlobalMultipliers.MatHeld);
        if (heldRawMat3 > tempMax)
        {
            heldRawMat3 = tempMax;
        }
        heldRawMat4 += (int)(rawMat4PerSec * passedTime * GlobalMultipliers.Mat4Mult * GlobalMultipliers.MatGen);
        tempMax = (int)(maxRawMat4 * GlobalMultipliers.MatHeld);
        if (heldRawMat4 > tempMax)
        {
            heldRawMat4 = tempMax;
        }
        heldRawMat5 += (int)(rawMat5PerSec * passedTime * GlobalMultipliers.Mat5Mult * GlobalMultipliers.MatGen);
        tempMax = (int)(maxRawMat5 * GlobalMultipliers.MatHeld);
        if (heldRawMat5 > tempMax)
        {
            heldRawMat5 = tempMax;
        }
        heldRawMat6 += (int)(rawMat6PerSec * passedTime * GlobalMultipliers.Mat6Mult * GlobalMultipliers.MatGen);
        tempMax = (int)(maxRawMat6 * GlobalMultipliers.MatHeld);
        if (heldRawMat6 > tempMax)
        {
            heldRawMat6 = tempMax;
        }
        heldRawMat7 += (int)(rawMat7PerSec * passedTime * GlobalMultipliers.Mat7Mult * GlobalMultipliers.MatGen);
        tempMax = (int)(maxRawMat7 * GlobalMultipliers.MatHeld);
        if (heldRawMat7 > tempMax)
        {
            heldRawMat7 = tempMax;
        }
        heldRawMat8 += (int)(rawMat8PerSec * passedTime * GlobalMultipliers.Mat8Mult * GlobalMultipliers.MatGen);
        tempMax = (int)(maxRawMat8 * GlobalMultipliers.MatHeld);
        if (heldRawMat8 > tempMax)
        {
            heldRawMat8 = tempMax;
        }
        heldRawMat9 += (int)(rawMat9PerSec * passedTime * GlobalMultipliers.Mat9Mult * GlobalMultipliers.MatGen);
        tempMax = (int)(maxRawMat9 * GlobalMultipliers.MatHeld);
        if (heldRawMat9 > tempMax)
        {
            heldRawMat9 = tempMax;
        }
    }


    public void CollectRawMaterials()
    {
        gc.currencyManager.CollectRawMaterials(this, heldRawMat1, heldRawMat2, heldRawMat3, heldRawMat4, heldRawMat5, heldRawMat6,
            heldRawMat7, heldRawMat8, heldRawMat9);
        if (heldRawMat1 > 0)
        {
            GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
            temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[0], gc, (int)heldRawMat1);
            heldRawMat1 = 0;

        }
        if (heldRawMat2 > 0)
        {
            GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
            temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[1], gc, (int)heldRawMat2);
            heldRawMat2 = 0;
        }
        if (heldRawMat3 > 0)
        {
            GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
            temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[2], gc, (int)heldRawMat3);
            heldRawMat3 = 0;
        }
        if (heldRawMat4 > 0)
        {
            GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
            temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[3], gc, (int)heldRawMat4);
            heldRawMat4 = 0;
        }
        if (heldRawMat5 > 0)
        {
            GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
            temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[4], gc, (int)heldRawMat5);
            heldRawMat5 = 0;
        }
        if (heldRawMat6 > 0)
        {
            GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
            temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[5], gc, (int)heldRawMat6);
            heldRawMat6 = 0;
        }
        if (heldRawMat7 > 0)
        {
            GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
            temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[6], gc, (int)heldRawMat7);
            heldRawMat7 = 0;
        }
        if (heldRawMat8 > 0)
        {
            GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
            temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[7], gc, (int)heldRawMat8);
            heldRawMat8 = 0;
        }
        if (heldRawMat9 > 0)
        {
            GameObject temp = Instantiate(flyingMat, currencyCollectButton.transform.position, this.transform.rotation) as GameObject;
            temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[8], gc, (int)heldRawMat9);
            heldRawMat9 = 0;
        }
        UpdateCurrencyCollect();

        gc.savedData.saveData();

    }

    public void CollectRawMaterials(Vector3 spawnPos)
    {
        if (!Tenant.openUI)
        {
            gc.currencyManager.CollectRawMaterials(this, heldRawMat1, heldRawMat2, heldRawMat3, heldRawMat4, heldRawMat5, heldRawMat6,
                heldRawMat7, heldRawMat8, heldRawMat9);
            if (heldRawMat1 > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[0], gc, (int)heldRawMat1);
                heldRawMat1 = 0;

            }
            if (heldRawMat2 > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[1], gc, (int)heldRawMat2);
                heldRawMat2 = 0;
            }
            if (heldRawMat3 > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[2], gc, (int)heldRawMat3);
                heldRawMat3 = 0;
            }
            if (heldRawMat4 > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[3], gc, (int)heldRawMat4);
                heldRawMat4 = 0;
            }
            if (heldRawMat5 > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[4], gc, (int)heldRawMat5);
                heldRawMat5 = 0;
            }
            if (heldRawMat6 > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[5], gc, (int)heldRawMat6);
                heldRawMat6 = 0;
            }
            if (heldRawMat7 > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[6], gc, (int)heldRawMat7);
                heldRawMat7 = 0;
            }
            if (heldRawMat8 > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[7], gc, (int)heldRawMat8);
                heldRawMat8 = 0;
            }
            if (heldRawMat9 > 0)
            {
                GameObject temp = Instantiate(flyingMat, spawnPos, this.transform.rotation) as GameObject;
                temp.GetComponent<FlyingMat>().Init(inventoryImage.rectTransform.position, listOfMatImages[8], gc, (int)heldRawMat9);
                heldRawMat9 = 0;
            }
        }

    }

    public bool GetIsUnlocked()
    {
        return isUnlocked;
    }

    public void SetIsUnlocked(bool setVar)
    {
        isUnlocked = setVar;
    }

    public void UpdateRawMat1PerSec()
    {
        int tempPerSec = (int)gc.savedData.GetRawMat1UpgradeLvl();
        rawMat1PerSec = tempPerSec;
        maxRawMat1 = rawMat1PerSec * 120;
        UpdateAllMax();


    }

    public void UpdateRawMat2PerSec()
    {

        int tempPerSec = (int)((gc.savedData.GetRawMat2UpgradeLvl()));
        rawMat2PerSec = tempPerSec;
        maxRawMat2 = rawMat2PerSec * 120;
        UpdateAllMax();
    }

    public void UpdateRawMat3PerSec()
    {

        int tempPerSec = (int)((gc.savedData.GetRawMat3UpgradeLvl()));
        rawMat3PerSec = tempPerSec;
        maxRawMat3 = rawMat3PerSec * 120;
        UpdateAllMax();
    }

    public void UpdateRawMat4PerSec()
    {

        int tempPerSec = (int)((gc.savedData.GetRawMat4UpgradeLvl()));
        rawMat4PerSec = tempPerSec;
        maxRawMat4 = rawMat4PerSec * 120;
        UpdateAllMax();
    }

    public void UpdateRawMat5PerSec()
    {

        int tempPerSec = (int)((gc.savedData.GetRawMat5UpgradeLvl()));
        rawMat5PerSec = tempPerSec;
        maxRawMat5 = rawMat5PerSec * 120;
        UpdateAllMax();
    }

    public void UpdateRawMat6PerSec()
    {

        int tempPerSec = (int)((gc.savedData.GetRawMat6UpgradeLvl()));
        rawMat6PerSec = tempPerSec;
        maxRawMat6 = rawMat6PerSec * 120;
        UpdateAllMax();
    }

    public void UpdateRawMat7PerSec()
    {

        int tempPerSec = (int)((gc.savedData.GetRawMat7UpgradeLvl()));
        rawMat7PerSec = tempPerSec;
        maxRawMat7 = rawMat7PerSec * 120;
        UpdateAllMax();
    }

    public void UpdateRawMat8PerSec()
    {

        int tempPerSec = (int)((gc.savedData.GetRawMat8UpgradeLvl()));
        rawMat8PerSec = tempPerSec;
        maxRawMat8 = rawMat8PerSec * 120;
        UpdateAllMax();
    }

    public void UpdateRawMat9PerSec()
    {

        int tempPerSec = (int)((gc.savedData.GetRawMat9UpgradeLvl()));
        rawMat9PerSec = tempPerSec;
        maxRawMat9 = rawMat9PerSec * 120;
        UpdateAllMax();
    }

    public void FullUpdate()
    {
        UpdateRawMat1PerSec();
        UpdateRawMat2PerSec();
        UpdateRawMat3PerSec();
        UpdateRawMat4PerSec();
        UpdateRawMat5PerSec();
        UpdateRawMat6PerSec();
        UpdateRawMat7PerSec();
        UpdateRawMat8PerSec();
        UpdateRawMat9PerSec();
    }

    private void UpdateAllMax()
    {
        maxEveryItem = maxRawMat1 + maxRawMat2 + maxRawMat3 + maxRawMat4 + maxRawMat5 + maxRawMat6 + maxRawMat7 + maxRawMat8 + maxRawMat9;
    }
}
