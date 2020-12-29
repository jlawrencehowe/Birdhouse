using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour
{

    private GameController gc;
    //private List<UIInfo> tier1UIObjects;
    //private List<UIInfo> tier2UIObjects;
    //private List<UIInfo> tier3UIObjects;
    private List<UIInfo> uiObjects;
    public GameObject UIReference;
    public int MAXCOLUMN;
    private Canvas inventoryMenuCanvas;
    public Canvas rawMatCanvas;
    public bool isTenantActive = false;
    public GameObject GiftNameRef;
    public GameObject activeGiftName;
    public Tenant activeTenant;
    public Canvas confirmationCanvas;
    public TextMeshProUGUI confirmationText;
    private UIInfo confirmationUIInfo;

    public class UIInfo
    {
        public GameObject UIObject;
        public Image giftImage;
        public string giftName;
        public TextMeshProUGUI quantity;
        public int actualQuantity;
        public int ID;

        public UIInfo(GameObject gameObject, Image image, string name, TextMeshProUGUI quantity, int actualQuantity, int id)
        {
            UIObject = gameObject;
            giftImage = image;
            giftName = name;
            this.quantity = quantity;
            this.actualQuantity = actualQuantity;
            ID = id;
        }
    }

    private void Awake()
    {
        inventoryMenuCanvas = GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();

    }

    void Update()
    {
       
    }

    //creates UI objects based on reference
    public void InstantiateUIObject(List<ShopsMenu.shopGift> shopGifts)
    {
        //List<UIInfo> currentTierObjects = new List<UIInfo>();
        uiObjects = new List<UIInfo>();
        //tier1UIObjects = new List<UIInfo>();
        //tier2UIObjects = new List<UIInfo>();
        //tier3UIObjects = new List<UIInfo>();
        Vector3 startPos = new Vector3(-90, 134, 0);
        float[] listOfInts = new float[5];
        listOfInts[0] = 134;
        listOfInts[1] = 60;
        listOfInts[2] = -14.4f;
        listOfInts[3] = -89;
        listOfInts[4] = -163.4f;
        /*
        for (int i = 0; i < shopGifts.Count; i++)
        {
            if (shopGifts[i].tier == 1)
            {
                currentTierObjects = tier1UIObjects;
            }
            else if (shopGifts[i].tier == 2)
            {
                currentTierObjects = tier2UIObjects;
            }
            else
            {
                currentTierObjects = tier3UIObjects;
            }

            var tempObject = Instantiate(UIReference, startPos * (currentTierObjects.Count + 1), this.transform.rotation) as GameObject;
            var tempUIInfo = new UIInfo(tempObject, tempObject.transform.Find("ItemImage").GetComponent<Image>(), shopGifts[i].name,
                tempObject.transform.Find("QuantityText").GetComponent<Text>(), shopGifts[i].id);
            tempObject.transform.SetParent(this.transform, false);
            currentTierObjects.Add(tempUIInfo);
            tempObject.transform.Find("ItemImage").GetComponent<Button>().onClick.AddListener(delegate { PressGiftImage(tempUIInfo); });
            tempUIInfo.quantity.text = shopGifts[i].quantity.ToString();
            //set object image using file path/
        }
        */
        for (int i = 0; i < shopGifts.Count; i++)
        {
            int colCounter = 0;
            int rowCounter = 0;
            Vector3 pos = startPos;
            pos.x += (colCounter * 90);
            pos.y -= (listOfInts[rowCounter]);
            var tempObject = Instantiate(UIReference, pos, this.transform.rotation) as GameObject;
            var tempUIInfo = new UIInfo(tempObject, tempObject.transform.Find("ItemImage").GetComponent<Image>(), shopGifts[i].name,
                tempObject.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>(), shopGifts[i].quantity, shopGifts[i].id);
            tempObject.transform.SetParent(this.transform, false);
            //currentTierObjects.Add(tempUIInfo);
            uiObjects.Add(tempUIInfo);
            tempObject.transform.Find("ItemImage").GetComponent<Button>().onClick.AddListener(delegate { PressGiftImage(tempUIInfo); });

            tempUIInfo.giftImage.sprite = Resources.Load<Sprite>(shopGifts[i].imageFilePath);
            tempUIInfo.quantity.text = "x" + shopGifts[i].quantity.ToString();

            //set object image using file path*/

            colCounter++;
            if(colCounter >= 3)
            {
                rowCounter++;
                colCounter = 0;
            }

        }
        //organize each list alphabetically
        /*
        OrganizeAlphabetically(tier1UIObjects);
        OrganizeAlphabetically(tier2UIObjects);
        OrganizeAlphabetically(tier3UIObjects);
        */
        OrganizeAlphabetically(uiObjects);
        OrganizeText();

    }

    private void OrganizeAlphabetically(List<UIInfo> uiList)
    {

        quickSort(uiList, 0, uiList.Count - 1);


    }

    public void UpdateTextQuantity(int giftId, int quantity)
    {
        //bool foundGift = false;
        /*
        for (int i = 0; i < tier1UIObjects.Count; i++)
        {
            if (tier1UIObjects[i].ID == giftId)
            {
                tier1UIObjects[i].quantity.text = quantity.ToString();
                foundGift = true;
                break;
            }
        }
        if (!foundGift)
        {
            for (int i = 0; i < tier2UIObjects.Count; i++)
            {
                if (tier2UIObjects[i].ID == giftId)
                {
                    tier2UIObjects[i].quantity.text = quantity.ToString();
                    foundGift = true;
                    break;
                }
            }
        }
        if (!foundGift)
        {
            for (int i = 0; i < tier3UIObjects.Count; i++)
            {
                if (tier3UIObjects[i].ID == giftId)
                {
                    tier3UIObjects[i].quantity.text = quantity.ToString();
                    foundGift = true;
                    break;
                }
            }
        }
        */

        for (int i = 0; i < uiObjects.Count; i++)
        {
            if (uiObjects[i].ID == giftId)
            {
                uiObjects[i].quantity.text = "x" + quantity.ToString();
                uiObjects[i].actualQuantity = quantity;
                break;
            }
        }

    }

    public void OrganizeText()
    {
        Vector3 startPos = new Vector3(-90, 134, 0);
        float[] listOfInts = new float[5];
        listOfInts[0] = 134;
        listOfInts[1] = 60;
        listOfInts[2] = -14.4f;
        listOfInts[3] = -89;
        listOfInts[4] = -163.4f;
        int column = 0;
        int row = 0;
        /*
        for (int i = 0; i < tier1UIObjects.Count; i++)
        {
            if (int.Parse(tier1UIObjects[i].quantity.text) == 0)
            {
                tier1UIObjects[i].UIObject.SetActive(false);
            }
            else
            {
                tier1UIObjects[i].UIObject.SetActive(true);
                Vector3 temp = new Vector3(startPos.x * column, startPos.y * row, startPos.z);
                tier1UIObjects[i].UIObject.GetComponent<RectTransform>().localPosition = temp;
                column++;
                if (column == MAXCOLUMN)
                {
                    column = 1;
                    row++;
                }
            }
        }

        startPos = new Vector3(20, 80, 0);
        column = 1;
        row = 1;
        for (int i = 0; i < tier2UIObjects.Count; i++)
        {
            if (int.Parse(tier2UIObjects[i].quantity.text) == 0)
            {
                tier2UIObjects[i].UIObject.SetActive(false);
            }
            else
            {
                tier2UIObjects[i].UIObject.SetActive(true);
                Vector3 temp = new Vector3(startPos.x * column, startPos.y * row, startPos.z);
                tier2UIObjects[i].UIObject.GetComponent<RectTransform>().localPosition = temp;
                column++;
                if (column == MAXCOLUMN)
                {
                    column = 1;
                    row++;
                }
            }
        }

        startPos = new Vector3(20, 140, 0);
        column = 1;
        row = 1;
        for (int i = 0; i < tier3UIObjects.Count; i++)
        {
            if (int.Parse(tier3UIObjects[i].quantity.text) == 0)
            {
                tier3UIObjects[i].UIObject.SetActive(false);
            }
            else
            {
                tier3UIObjects[i].UIObject.SetActive(true);
                Vector3 temp = new Vector3(startPos.x * column, startPos.y * row, startPos.z);
                tier3UIObjects[i].UIObject.GetComponent<RectTransform>().localPosition = temp;
                column++;
                if (column == MAXCOLUMN)
                {
                    column = 1;
                    row++;
                }
            }
        }
        */

        for (int i = 0; i < uiObjects.Count; i++)
        {
            if (uiObjects[i].actualQuantity == 0)
            {
                uiObjects[i].UIObject.SetActive(false);
            }
            else
            {
                uiObjects[i].UIObject.SetActive(true);
                Vector3 temp = new Vector3(startPos.x + (90 * column), listOfInts[row], startPos.z);
                uiObjects[i].UIObject.GetComponent<RectTransform>().localPosition = temp;
                column++;
                if (column == MAXCOLUMN)
                {
                    column = 0;
                    row++;
                }
            }
        }


    }




    static int partition(List<UIInfo> uiList, int low,
                                   int high)
    {
        string pivot = uiList[high].giftName;

        // index of smaller element 
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            // If current element is smaller  
            // than the pivot 

            if (string.Compare(uiList[j].giftName.ToLower(), pivot.ToLower()) == -1)
            {
                i++;

                // swap uiList[i] and uiList[j] 
                var temp = uiList[i];
                uiList[i] = uiList[j];
                uiList[j] = temp;
            }
        }

        // swap arr[i+1] and arr[high] (or pivot) 
        var temp1 = uiList[i + 1];
        uiList[i + 1] = uiList[high];
        uiList[high] = temp1;

        return i + 1;
    }


    //The main function that implements QuickSort() 
    //arr[] --> Array to be sorted, 
    //low --> Starting index, 
    //high --> Ending index 
    static void quickSort(List<UIInfo> uiList, int low, int high)
    {
        if (low < high)
        {

            // pi is partitioning index, arr[pi] is  
            //now at right place 
            int pi = partition(uiList, low, high);

            // Recursively sort elements before 
            // partition and after partition 
            quickSort(uiList, low, pi - 1);
            quickSort(uiList, pi + 1, high);
        }
    }



    public void OpenRawMatMenu()
    {
        if (!isTenantActive)
        {
            rawMatCanvas.enabled = true;
            CloseMenu();
            gc.gameUI.SetActiveMenu(GameUI.ActiveMenu.inventory);
            Tenant.openUI = true;
            gc.touchScreen.scrollLock = true;
        }
    }

    public void CloseMenu()
    {
        gc.gameUI.SetActiveMenu(GameUI.ActiveMenu.none);
        gc.touchScreen.scrollLock = false;
        inventoryMenuCanvas.enabled = false;
        if (activeGiftName != null)
        {
            Destroy(activeGiftName);
        }
        isTenantActive = false;
        Tenant.openUI = false;
    }

    public void OpenMenu()
    {
        OrganizeText();
        inventoryMenuCanvas.enabled = true;
        Tenant.openUI = true;
    }

    public void OpenMenu(Tenant tenant)
    {
        OrganizeText();
        inventoryMenuCanvas.enabled = true;
        isTenantActive = true;
        activeTenant = tenant;
        Tenant.openUI = true;
    }


    public void PressGiftImage(UIInfo uiParent)
    {
        if (activeGiftName != null)
        {
            Destroy(activeGiftName);
        }
        if (isTenantActive && activeTenant.GetFavorLevel() < 3)
        {
            confirmationCanvas.transform.localScale = new Vector3(1, 1, 1);
            confirmationUIInfo = uiParent;
            confirmationText.text = "Are you sure you want to give them a " + uiParent.giftName + "?";
            confirmationCanvas.enabled = true;
        }
        else
        {


            activeGiftName = Instantiate(GiftNameRef, uiParent.UIObject.transform.position, this.transform.rotation) as GameObject;
            activeGiftName.GetComponent<GiftNameUI>().SetName(uiParent.giftName);
            activeGiftName.transform.SetParent(inventoryMenuCanvas.transform, false);
            activeGiftName.GetComponent<RectTransform>().localPosition = uiParent.UIObject.GetComponent<RectTransform>().localPosition;
        }
    }

    public void ConfirmationCanvasYes()
    {
        activeTenant.GiveGift(confirmationUIInfo.ID);
        gc.inventory.UpdateItemQuantity(confirmationUIInfo.ID, -1);
        confirmationCanvas.enabled = false;
        CloseMenu();
    }

    public void ConfirmationCanvasNo()
    {
        confirmationCanvas.enabled = false;

    }




}





