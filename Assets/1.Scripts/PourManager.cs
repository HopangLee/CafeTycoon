using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourManager : MonoBehaviour
{
    [SerializeField] GameObject milk;
    [SerializeField] GameObject water;


    private void Update()
    {
        if (TycoonGameManager.instance.currentGameState != GameState.Playing ||
            TycoonGameManager.instance.currentStage != GameStage.FillCup)
            return;

        // 가속도계 및 자이로스코프 값을 얻기
        //Vector3 acceleration = Input.acceleration;
        Vector3 rotationRate = Input.gyro.rotationRate;
        float yaw = rotationRate.z;

        Debug.Log("yaw : " + yaw);
        if (TycoonGameManager.instance.curliquidType == LiquidType.Milk)
        {
            MilkOn();
            
        }
        else
        {
            WaterOn();
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
