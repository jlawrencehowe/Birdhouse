using System;

public class GlobalMultipliers
{

    static float moneyGen = 1;
    static float matGen = 1;
    static float moneyHeld = 1;
    static float matHeld = 1;
    static int maxGiftIncrease = 0;
    static float tenantCostRedu = 1;
    static float contractCostRedu = 1;
    static float giftCostRedu = 1;
    static float giftEffeIncre = 1;
    static float mat1Mult = 1;
    static float mat2Mult = 1;
    static float mat3Mult = 1;
    static float mat4Mult = 1;
    static float mat5Mult = 1;
    static float mat6Mult = 1;
    static float mat7Mult = 1;
    static float mat8Mult = 1;
    static float mat9Mult = 1;
    static float mat10Mult = 1;
    static float mat11Mult = 1;
    static float mat12Mult = 1;

    static public float MoneyGen { get => moneyGen; set => moneyGen = value; }
    static public float MatGen { get => matGen; set => matGen = value; }
    static public float MoneyHeld { get => moneyHeld; set => moneyHeld = value; }
    static public float MatHeld { get => matHeld; set => matHeld = value; }
    static public int MaxGiftIncrease { get => maxGiftIncrease; set => maxGiftIncrease = value; }
    static public float TenantCostRedu { get => tenantCostRedu; set => tenantCostRedu = value; }
    static public float ContractCostRedu { get => contractCostRedu; set => contractCostRedu = value; }
    static public float GiftCostRedu { get => giftCostRedu; set => giftCostRedu = value; }
    static public float GiftEffeIncre { get => giftEffeIncre; set => giftEffeIncre = value; }
    static public float Mat1Mult { get => mat1Mult; set => mat1Mult = value; }
    static public float Mat2Mult { get => mat2Mult; set => mat2Mult = value; }
    static public float Mat3Mult { get => mat3Mult; set => mat3Mult = value; }
    static public float Mat4Mult { get => mat4Mult; set => mat4Mult = value; }
    static public float Mat5Mult { get => mat5Mult; set => mat5Mult = value; }
    static public float Mat6Mult { get => mat6Mult; set => mat6Mult = value; }
    static public float Mat7Mult { get => mat7Mult; set => mat7Mult = value; }
    static public float Mat8Mult { get => mat8Mult; set => mat8Mult = value; }
    static public float Mat9Mult { get => mat9Mult; set => mat9Mult = value; }
    static public float Mat10Mult { get => mat10Mult; set => mat10Mult = value; }
    static public float Mat11Mult { get => mat11Mult; set => mat11Mult = value; }
    static public float Mat12Mult { get => mat12Mult; set => mat12Mult = value; }

    static public void IncreaseMatMultiByID(int id, float multiplier)
    {
        if (id == 1)
        {
            Mat1Mult = Mat1Mult + multiplier;
        }
        else if (id == 2)
        {
            Mat2Mult = Mat2Mult + multiplier;
        }
        else if (id == 3)
        {
            Mat3Mult = Mat3Mult + multiplier;
        }
        else if (id == 4)
        {
            Mat4Mult = Mat4Mult + multiplier;
        }
        else if (id == 5)
        {
            Mat5Mult = Mat5Mult + multiplier;
        }
        else if (id == 6)
        {
            Mat6Mult = Mat6Mult + multiplier;
        }
        else if (id == 7)
        {
            Mat7Mult = Mat7Mult + multiplier;
        }
        else if (id == 8)
        {
            Mat8Mult = Mat8Mult + multiplier;
        }
        else if (id == 9)
        {
            Mat9Mult = Mat9Mult + multiplier;
        }
        else if (id == 10)
        {
            Mat10Mult = Mat10Mult + multiplier;
        }
        else if (id == 11)
        {
            Mat11Mult = Mat11Mult + multiplier;
        }
        else if (id == 12)
        {
            Mat12Mult = Mat12Mult + multiplier;
        }
    }

    static public void ResetAll()
    {
        moneyGen = 1;
        matGen = 1;
        moneyHeld = 1;
        matHeld = 1;
        maxGiftIncrease = 0;
        tenantCostRedu = 1;
        contractCostRedu = 1;
        giftCostRedu = 1;
        giftEffeIncre = 1;
        mat1Mult = 1;
        mat2Mult = 1;
        mat3Mult = 1;
        mat4Mult = 1;
        mat5Mult = 1;
        mat6Mult = 1;
        mat7Mult = 1;
        mat8Mult = 1;
        mat9Mult = 1;
        mat10Mult = 1;
        mat11Mult = 1;
        mat12Mult = 1;
    }
}
