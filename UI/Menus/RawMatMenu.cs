using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RawMatMenu : MonoBehaviour
{

    private List<UIInfo> rawMatUIObjects;

    public GameObject UIReference;

    private Canvas rawMatCanvas;
    public InventoryMenu inventoryMenu;
    public List<Sprite> itemSprites;
    private GameController gc;
    public GameObject GiftNameRef;
    public GameObject activeGiftName;

    public class UIInfo
    {
        public GameObject UIObject;
        public Image matImage;
        public string matName;
        public TextMeshProUGUI quantity;

        public UIInfo(GameObject gameObject, Image image, string name, TextMeshProUGUI quantity)
        {
            UIObject = gameObject;
            matImage = image;
            matName = name;
            this.quantity = quantity;
        }
    }


    private void Start()
    {
        rawMatCanvas = GetComponent<Canvas>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        rawMatUIObjects = new List<UIInfo>();
        Vector3 startPos = new Vector3(-90, 134, 0);
        float[] listOfInts = new float[5];
        listOfInts[0] = 134;
        listOfInts[1] = 60;
        listOfInts[2] = -14.4f;
        listOfInts[3] = -89;
        listOfInts[4] = -163.4f;
        int row = 0;
        int col = 0;
        for (int i = 0; i < itemSprites.Count; i++)
        {
            Vector3 tempPos = startPos;
            tempPos.x += (90 * col);
            tempPos.y = (listOfInts[row]);
            var tempUIRef = Instantiate(UIReference, tempPos, transform.rotation) as GameObject;
            //tempUIRef.GetComponent<RectTransform>().transform.position = startPos;
            var tempUIInfo = new UIInfo(tempUIRef, tempUIRef.transform.Find("ItemImage").GetComponent<Image>(),
                i.ToString(), tempUIRef.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>());
            tempUIRef.transform.SetParent(rawMatCanvas.transform, false);
            tempUIInfo.matImage.sprite = itemSprites[i];
            tempUIInfo.quantity.text = "x0";
            tempUIRef.transform.Find("ItemImage").GetComponent<Button>().onClick.AddListener(delegate { PressGiftImage(tempUIInfo); });
            
            rawMatUIObjects.Add(tempUIInfo);
            col++;
            if(col >= 3)
            {
                col = 0;
                row++;
            }
        }
        rawMatUIObjects[0].matName = "Wood Plank";
        rawMatUIObjects[1].matName = "Metal Ore";
        rawMatUIObjects[2].matName = "Cloth Square";
        rawMatUIObjects[3].matName = "Screws";
        rawMatUIObjects[4].matName = "Wires";
        rawMatUIObjects[5].matName = "Plastic Bottle";
        rawMatUIObjects[6].matName = "Glass Pane";
        rawMatUIObjects[7].matName = "Paint Can";
        rawMatUIObjects[8].matName = "Gold Bar";
        //rawMatUIObjects[9].matName = "RawMat10";
        //rawMatUIObjects[10].matName = "RawMat11";
        //rawMatUIObjects[11].matName = "RawMat12";
        //rawMatUIObjects[12].matName = "RawMat13";
        //rawMatUIObjects[13].matName = "RawMat14";

    }


    public void OpenInventoryMenu()
    {
        inventoryMenu.OpenMenu();
        CloseMenu();
        gc.gameUI.SetActiveMenu(GameUI.ActiveMenu.inventory);
        gc.touchScreen.scrollLock = true;
        Tenant.openUI = true;
    }

    public void CloseMenu()
    {
        rawMatCanvas.enabled = false;
        gc.touchScreen.scrollLock = false;
        Destroy(activeGiftName);
        Tenant.openUI = false;
        gc.gameUI.SetActiveMenu(GameUI.ActiveMenu.none);
    }

    public void OpenMenu()
    {
        UpdateText();
        rawMatCanvas.enabled = true;
        Tenant.openUI = true;

    }

    public void UpdateText()
    {
        rawMatUIObjects[0].quantity.text = "x" + gc.currencyManager.GetRawMat1().ToString();
        rawMatUIObjects[1].quantity.text = "x" + gc.currencyManager.GetRawMat2().ToString();
        rawMatUIObjects[2].quantity.text = "x" + gc.currencyManager.GetRawMat3().ToString();
        rawMatUIObjects[3].quantity.text = "x" + gc.currencyManager.GetRawMat4().ToString();
        rawMatUIObjects[4].quantity.text = "x" + gc.currencyManager.GetRawMat5().ToString();
        rawMatUIObjects[5].quantity.text = "x" + gc.currencyManager.GetRawMat6().ToString();
        rawMatUIObjects[6].quantity.text = "x" + gc.currencyManager.GetRawMat7().ToString();
        rawMatUIObjects[7].quantity.text = "x" + gc.currencyManager.GetRawMat8().ToString();
        rawMatUIObjects[8].quantity.text = "x" + gc.currencyManager.GetRawMat9().ToString();
        //rawMatUIObjects[9].quantity.text = gc.currencyManager.GetRawMat10().ToString();
        //rawMatUIObjects[10].quantity.text = gc.currencyManager.GetRawMat11().ToString();
        //rawMatUIObjects[11].quantity.text = gc.currencyManager.GetRawMat12().ToString();
        //rawMatUIObjects[12].quantity.text = gc.currencyManager.GetRawMat13().ToString();
        //rawMatUIObjects[13].quantity.text = gc.currencyManager.GetRawMat14().ToString();
    }

    public void PressGiftImage(UIInfo uiParent)
    {
        if (activeGiftName != null)
        {
            Destroy(activeGiftName);
        }
        


            activeGiftName = Instantiate(GiftNameRef, uiParent.UIObject.transform.position, this.transform.rotation) as GameObject;
            activeGiftName.GetComponent<GiftNameUI>().SetName(uiParent.matName);
            activeGiftName.transform.SetParent(rawMatCanvas.transform, false);
        activeGiftName.GetComponent<RectTransform>().localPosition = uiParent.UIObject.GetComponent<RectTransform>().localPosition;


    }

    public string GetMatNameByID(int id)
    {
        return rawMatUIObjects[id].matName;
    }

}
