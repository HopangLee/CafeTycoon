using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cup : MonoBehaviour
{
    public CupType cupType; // 컵 타입
    float liquidAmount; // 액체의 양
    float milkTemp; // 우유 온도
    [HideInInspector] public int shotNum; // 샷 개수
    LiquidType liquidType;
    bool[] toppings = new bool[Enum.GetValues(typeof(Topping)).Length]; // 해당 topping이 있으면 true, 없으면 false
    LatteArt latteArt;

    public float getLiquid()
    {
        return liquidAmount;
    }

    public float getTemp()
    {
        return milkTemp;
    }

    public void ReleaseCup()
    {
        liquidAmount = 0;
        milkTemp = 0;
        shotNum = 0;
        this.gameObject.SetActive(false);
    }

    public void ActiveCup()
    {
        this.gameObject.SetActive(true);
    }

    public void setLiquid(LiquidType liquidType)
    {
        this.liquidType = liquidType;
    }

    public void setType(CupType cupType)
    {
        this.cupType = cupType;
    }

    public void setTemp(float temp)
    {
        this.milkTemp = temp;
    }
}
