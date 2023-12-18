using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourManager : MonoBehaviour
{
    [SerializeField] GameObject milk;
    [SerializeField] GameObject water;
    private float threshold = -0.17f;

    [SerializeField] private Transform liquidOutPos;
    [SerializeField] private Material waterFallMat;
    private float min_pourAmount = 0;
    private float max_pourAmount = 1f;

    private bool isPouring = false;

    private void Update()
    {
        if (TycoonGameManager.instance.currentGameState != GameState.Playing ||
            TycoonGameManager.instance.currentStage != GameStage.FillCup)
            return;

        // 가속도계 및 자이로스코프 값을 얻기
        
        //Vector3 rotationRate = Input.gyro.rotationRate;
        Vector3 acceleration = Input.acceleration;
        //float yaw = rotationRate.z;
        float rot = acceleration.x;

        
        if (TycoonGameManager.instance.curliquidType == LiquidType.Milk)
        {
            MilkOn();
            if (rot < threshold)
            {
                Cup _cup = TycoonGameManager.instance.curCup;
                waterFallMat.SetFloat("_Split_Value", _cup.transform.position.y + _cup.getRealLiquidHeight());
                float amount = Mathf.Lerp(min_pourAmount, max_pourAmount, Mathf.Min((threshold - rot), 1));
                liquidOutPos.localScale = new Vector3(amount, 1, 1);
                if(!liquidOutPos.gameObject.activeSelf) liquidOutPos.gameObject.SetActive(true);

                TycoonGameManager.instance.curCup.PourLiquid(amount * Time.deltaTime);
                if (!isPouring) isPouring = true;
            }
            else
            {
                if (liquidOutPos.gameObject.activeSelf) liquidOutPos.gameObject.SetActive(false);

                if (isPouring && rot >= 0)
                {
                    isPouring = false;
                    TycoonGameManager.instance.MoveNextStage();
                }
            }
        }
        else
        {
            WaterOn();
            if (rot < threshold)
            {
                Cup _cup = TycoonGameManager.instance.curCup;
                waterFallMat.SetFloat("_Split_Value", _cup.transform.position.y + _cup.getRealLiquidHeight());
                float amount = Mathf.Lerp(min_pourAmount, max_pourAmount, Mathf.Min((threshold - rot), 1));
                liquidOutPos.localScale = new Vector3(amount, 1, 1);
                if (!liquidOutPos.gameObject.activeSelf) liquidOutPos.gameObject.SetActive(true);

                TycoonGameManager.instance.curCup.PourLiquid(amount * Time.deltaTime);
                if (!isPouring) isPouring = true; ;
            }
            else 
            {
                if (liquidOutPos.gameObject.activeSelf) liquidOutPos.gameObject.SetActive(false);

                if (isPouring && rot >= 0)
                {
                    isPouring = false;
                    TycoonGameManager.instance.MoveNextStage();
                }
            }
        }
    }

    public void MilkOn()
    {
        if(!milk.activeSelf) milk.SetActive(true);
        if(water.activeSelf) water.SetActive(false);
    }

    public void WaterOn()
    {
        if(milk.activeSelf) milk.SetActive(false);
        if(!water.activeSelf) water.SetActive(true);
    }
}
