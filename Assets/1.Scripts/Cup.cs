using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cup : MonoBehaviour
{
    public CupType cupType; // 컵 타입
    float liquidAmount = 0; // 액체의 양
    float milkTemp = 0; // 우유 온도
    private int shotNum = 0; // 샷 개수



    LiquidType liquidType;
    bool[] toppings = new bool[Enum.GetValues(typeof(Topping)).Length]; // 해당 topping이 있으면 true, 없으면 false
    LatteArt latteArt;

    [Header("Materaials for pour scene")]
    [SerializeField] Material cup_mat;
    [SerializeField] Material liquid_mat;

    [Header("Cup Info")]
    [SerializeField] float cupTransValue = 0.085f;
    [SerializeField] float maxAmount = 0.075f;
    [SerializeField] float minAmount = -0.066f;
    [SerializeField] float lineHeight = 0f;
    float maxShotAmount = -0.03f;


    public float getLiquid()
    {
        return liquidAmount;
    }

    public float getRealLiquidHeight()
    {
        return minAmount + liquidAmount;
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

    public void SetLine(bool _bool)
    {
        cup_mat.SetFloat("_Fill_Amount", _bool ? cupTransValue : 1.0f) ;

        cup_mat.SetFloat("_Show_Line", _bool ? 1.0f : 0.0f);

        cup_mat.SetFloat("_Line_Height", _bool ? lineHeight : 0f);
        Debug.Log("_bool : " + _bool);
    }

    public void AddShot()
    {
        shotNum++;
        liquidAmount = Mathf.Min(liquidAmount + (maxShotAmount - minAmount)/4 , maxShotAmount - minAmount);
        liquid_mat.SetFloat("_Split_Value", minAmount + liquidAmount);
        liquid_mat.SetFloat("_ShotRatio", Mathf.Sin(shotNum/4 * Mathf.PI/2));
    }

    public void PourLiquid(float amount)
    {
        if (cupType == CupType.Hot)
            amount *= 0.2f;
        else amount *= 1f;

        liquidAmount = Mathf.Min(liquidAmount + amount, maxAmount - minAmount);
        liquid_mat.SetFloat("_Split_Value", minAmount + liquidAmount);
    }

    public void CupMatReset()
    {
        cup_mat.SetFloat("_Fill_Amount", 1.0f);

        cup_mat.SetFloat("_Show_Line",  0.0f);

        float _zero = liquid_mat.GetFloat("_zero_value");

        liquid_mat.SetFloat("_Split_Value", _zero);
        liquidAmount = 0;
    }
}
