using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{

    public int money;
    private int rawMat1;
    private int rawMat2;
    private int rawMat3;
    private int rawMat4;
    private int rawMat5;
    private int rawMat6;
    private int rawMat7;
    private int rawMat8;
    private int rawMat9;
    private int rawMat10;
    private int rawMat11;
    private int rawMat12;
    private int rawMat13;
    private int rawMat14;
    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int addMoney)
    {
        money += addMoney;
        if(money >= 999999999)
        {
            money = 999999999;
        }
        gc.gameUI.UpdateMoneyText(addMoney);
    }


    public void QuickAddMoney(int addMoney)
    {
        money += addMoney;
        gc.gameUI.QuickUpdateMoney(addMoney);
    }

    public void CollectMoney(Tenant tenant, int heldMoney)
    {
        AddMoney(heldMoney);

    }


    public void CollectRawMaterials(DeliveryBird deliveryBird, int heldRawMat1, int heldRawMat2, int heldRawMat3, int heldRawMat4, int heldRawMat5, int heldRawMat6,
        int heldRawMat7, int heldRawMat8, int heldRawMat9)
    {
        AddRawMat1(heldRawMat1);
        AddRawMat2(heldRawMat2);
        AddRawMat3(heldRawMat3);
        AddRawMat4(heldRawMat4);
        AddRawMat5(heldRawMat5);
        AddRawMat6(heldRawMat6);
        AddRawMat7(heldRawMat7);
        AddRawMat8(heldRawMat8);
        AddRawMat9(heldRawMat9);

    }
    public void SpendCurrency(List<int> costs)
    {
        AddMoney(-costs[0]);
        AddRawMat1(-costs[1]);
        AddRawMat2(-costs[2]);
        AddRawMat3(-costs[3]);
        AddRawMat4(-costs[4]);
        AddRawMat5(-costs[5]);
        AddRawMat6(-costs[6]);
        AddRawMat7(-costs[7]);
        AddRawMat8(-costs[8]);
        AddRawMat9(-costs[9]);
    }

    public int GetCurrencyByID(int i)
    {
        if (i == 0)
        {
            return GetMoney();
        }
        else if (i == 1)
        {
            return GetRawMat1();
        }
        else if (i == 2)
        {
            return GetRawMat2();
        }
        else if (i == 3)
        {
            return GetRawMat3();
        }
        else if (i == 4)
        {
            return GetRawMat4();
        }
        else if (i == 5)
        {
            return GetRawMat5();
        }
        else if (i == 6)
        {
            return GetRawMat6();
        }
        else if (i == 7)
        {
            return GetRawMat7();
        }
        else if (i == 8)
        {
            return GetRawMat8();
        }
        else if (i == 9)
        {
            return GetRawMat9();
        }
        else if (i == 10)
        {
            return GetRawMat10();
        }
        else if (i == 11)
        {
            return GetRawMat11();
        }
        else if (i == 12)
        {
            return GetRawMat12();
        }
        else if (i == 13)
        {
            return GetRawMat13();
        }
        else
        {
            return GetRawMat14();
        }
    }

    public void CollectRawMatByID(int id, int amount)
    {
        if (id == 12)
        {
            AddRawMat12(amount);
        }
        else if (id == 13)
        {
            AddRawMat13(amount);
        }
        else if (id == 14)
        {
            AddRawMat14(amount);
        }
    }

    #region Getter/Setter
    public int GetRawMat1()
    {
        return rawMat1;
    }

    public void AddRawMat1(int addRawMat1)
    {
        rawMat1 += addRawMat1;
        if(rawMat1 >= 999999999)
        {
            rawMat1 = 999999999;
        }
    }

    public int GetRawMat2()
    {
        return rawMat2;
    }

    public void AddRawMat2(int addRawMat2)
    {
        rawMat2 += addRawMat2;
        if (rawMat2 >= 999999999)
        {
            rawMat2 = 999999999;
        }
    }

    public int GetRawMat3()
    {
        return rawMat3;
    }

    public void AddRawMat3(int addRawMat3)
    {
        rawMat3 += addRawMat3;
        if (rawMat3 >= 999999999)
        {
            rawMat3 = 999999999;
        }
    }

    public int GetRawMat4()
    {
        return rawMat4;
    }

    public void AddRawMat4(int addRawMat4)
    {
        rawMat4 += addRawMat4;
        if (rawMat4 >= 999999999)
        {
            rawMat4 = 999999999;
        }
    }

    public int GetRawMat5()
    {
        return rawMat5;
    }

    public void AddRawMat5(int addRawMat5)
    {
        rawMat5 += addRawMat5;
        if (rawMat5 >= 999999999)
        {
            rawMat5 = 999999999;
        }
    }

    public int GetRawMat6()
    {
        return rawMat6;
    }

    public void AddRawMat6(int addRawMat6)
    {
        rawMat6 += addRawMat6;
        if (rawMat6 >= 999999999)
        {
            rawMat6 = 999999999;
        }
    }

    public int GetRawMat7()
    {
        return rawMat7;
    }

    public void AddRawMat7(int addRawMat7)
    {
        rawMat7 += addRawMat7;
        if (rawMat7 >= 999999999)
        {
            rawMat7 = 999999999;
        }
    }

    public int GetRawMat8()
    {
        return rawMat8;
    }

    public void AddRawMat8(int addRawMat8)
    {
        rawMat8 += addRawMat8;
        if (rawMat8 >= 999999999)
        {
            rawMat8 = 999999999;
        }
    }

    public int GetRawMat9()
    {
        return rawMat9;
    }

    public void AddRawMat9(int addRawMat9)
    {
        rawMat9 += addRawMat9;
        if (rawMat9 >= 999999999)
        {
            rawMat9 = 999999999;
        }
    }

    public int GetRawMat10()
    {
        return rawMat10;
    }

    public void AddRawMat10(int addRawMat10)
    {
        rawMat10 += addRawMat10;
        if (rawMat10 >= 999999999)
        {
            rawMat10 = 999999999;
        }
    }

    public int GetRawMat11()
    {
        return rawMat11;
    }

    public void AddRawMat11(int addRawMat11)
    {
        rawMat11 += addRawMat11;
        if (rawMat11 >= 999999999)
        {
            rawMat11 = 999999999;
        }
    }

    public int GetRawMat12()
    {
        return rawMat12;
    }

    public void AddRawMat12(int addRawMat12)
    {
        rawMat12 += addRawMat12;
        if (rawMat12 >= 999999999)
        {
            rawMat12 = 999999999;
        }
    }

    public int GetRawMat13()
    {
        return rawMat13;
    }

    public void AddRawMat13(int addRawMat13)
    {
        rawMat13 += addRawMat13;
        if (rawMat13 >= 999999999)
        {
            rawMat13 = 999999999;
        }
    }

    public int GetRawMat14()
    {
        return rawMat14;
    }

    public void AddRawMat14(int addRawMat14)
    {
        rawMat14 += addRawMat14;
        if (rawMat14 >= 999999999)
        {
            rawMat14 = 999999999;
        }
    }
    #endregion
}
