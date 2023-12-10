using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLiquidManager : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (TycoonGameManager.instance.currentGameState != GameState.Playing ||
            TycoonGameManager.instance.currentStage != GameStage.ChooseLiquid)
            return;

        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Water")
                    {
                        Debug.Log("choose water");
                        TycoonGameManager.instance.curliquidType = LiquidType.Water;
                        TycoonGameManager.instance.curCup.setLiquid(LiquidType.Water);
                        TycoonGameManager.instance.MoveStage(GameStage.FillCup);

                    }
                    else if (hit.collider.tag == "Milk")
                    {
                        Debug.Log("choose milk");
                        TycoonGameManager.instance.curliquidType = LiquidType.Milk;
                        TycoonGameManager.instance.curCup.setLiquid(LiquidType.Milk);
                        TycoonGameManager.instance.MoveNextStage();
                    }
                    return;
                }
            }
        }
    }
}
