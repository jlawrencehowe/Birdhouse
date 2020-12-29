using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private List<ShopsMenu.shopGift> shopGifts;
    private InventoryMenu inventoryMenu;


    // Start is called before the first frame update
    void Start()
    {
        
        int currentNum = 0;
        shopGifts = new List<ShopsMenu.shopGift>();
        ShopsMenu.shopGift previousGift = null;
        do
        {
            ShopsMenu.shopGift temp = ShopsMenu.AddGift(currentNum);
            temp.quantity = 0;
            if (previousGift == null || previousGift.name != temp.name)
            {
                shopGifts.Add(temp);
                currentNum++;
            }
            else
            {
                break;
            }
            previousGift = temp;
            
        } while (true);
        inventoryMenu = GameObject.Find("InventoryCanvas").GetComponent<InventoryMenu>();

        inventoryMenu.InstantiateUIObject(shopGifts);

    }




    public void UpdateItemQuantity(int id, int quantity)
    {
        if (shopGifts[id].quantity == 0)
        {
            inventoryMenu.OrganizeText();
        }
        shopGifts[id].quantity += quantity;
        inventoryMenu.UpdateTextQuantity(id, shopGifts[id].quantity);
        if (shopGifts[id].quantity == 0)
        {
            inventoryMenu.OrganizeText();
        }
    }

    public void LoadSaveData(List<ShopsMenu.shopGift> savedGifts)
    {
        if (savedGifts != null)
        {
            shopGifts = savedGifts;
            foreach (var gift in shopGifts)
            {
                inventoryMenu.UpdateTextQuantity(gift.id, shopGifts[gift.id].quantity);
            }

        }


    }

    public List<ShopsMenu.shopGift> GetShopGifts()
    {
        return shopGifts;
    }

}