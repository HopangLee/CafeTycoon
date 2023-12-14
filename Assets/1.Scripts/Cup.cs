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
    [SerializeField] Material opaque_mat;
    [SerializeField] Material tranparent_mat;
    [SerializeField] private GameObject line;

    [Header("Liquid, Shot, Bubble")]
    [SerializeField] Transform Liquid;
    [SerializeField] Transform Shot;
    //[SerializeField] Transform bubble;

    private float floorPosY = 0.01f;

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

    public void SetLine(bool _bool)
    {
        if(line!=null)
            line.SetActive(_bool);
    }

    public void AddShot()
    {
        shotNum++;
    }

    public void PourLiquid(float amount)
    {
        liquidAmount = Mathf.Min(liquidAmount + amount, 1.1f);
        Liquid.localScale = new Vector3(Liquid.localScale.x, liquidAmount,Liquid.localScale.z);
    }

    public void SetTransparent(bool _bool)
    {
        if (_bool)
        {
            this.GetComponentInChildren<MeshRenderer>().material = tranparent_mat;
        }
        else
        {
            this.GetComponentInChildren<MeshRenderer>().material = opaque_mat;
        }
    }
}
