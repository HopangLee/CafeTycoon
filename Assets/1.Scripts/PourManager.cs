using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourManager : MonoBehaviour
{
    [SerializeField] GameObject milk;
    [SerializeField] GameObject water;
    private float threshold = -0.15f;

    [SerializeField] private Transform liquidOutPos;
    private float min_pourAmount = 0;
    private float max_pourAmount = 1.2f;

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
                float amount = Mathf.Lerp(min_pourAmount, max_pourAmount, Mathf.Min((threshold - rot), 1));
                liquidOutPos.localScale = new Vector3(amount, 1, 1);
                if(!liquidOutPos.gameObject.activeSelf) liquidOutPos.gameObject.SetActive(true);

                TycoonGameManager.instance.curCup.PourLiquid(amount * Time.deltaTime);
                Debug.Log("우유 부음 value : " + (int)(rot * 100));

            }
            else
            {
                if (liquidOutPos.gameObject.activeSelf) liquidOutPos.gameObject.SetActive(false);
                Debug.Log("우유 멈춤 value : " + (int)(rot * 100));
            }
        }
        else
        {
            WaterOn();
            if (rot < threshold)
            {
                float amount = Mathf.Lerp(min_pourAmount, max_pourAmount, Mathf.Min((threshold - rot), 1));
                liquidOutPos.localScale = new Vector3(amount, 1, 1);
                if (!liquidOutPos.gameObject.activeSelf) liquidOutPos.gameObject.SetActive(true);
                Debug.Log(" 부음 value : " + (int)(rot * 100));
            }
            else
            {
                if (liquidOutPos.gameObject.activeSelf) liquidOutPos.gameObject.SetActive(false);
                Debug.Log("물 멈춤 value : " + (int)(rot * 100));
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
