using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeShotManager : MonoBehaviour
{

    private void Update()
    {
        if (TycoonGameManager.instance.currentGameState != GameState.Playing ||
            TycoonGameManager.instance.currentStage != GameStage.OutShot)
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
                    if (hit.collider.tag == "Coffee Tap Button")
                    {
                        CoffeeTapButton coffeeTap = hit.collider.GetComponent<CoffeeTapButton>();
                        Debug.Log(coffeeTap.number + " button choose");

                        if (!coffeeTap.isActive)
                        {
                            coffeeTap.setActive(true);
                            //TycoonGameManager.instance.curCup.shotNum++;
                        }
                    }
                }
            }
        }
    }
}
