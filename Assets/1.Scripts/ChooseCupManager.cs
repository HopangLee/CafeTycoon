using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCupManager : MonoBehaviour
{
    Camera cam;
    [Header("Cup prefabs")]
    [SerializeField] private Cup hotCup;
    [SerializeField] private Cup iceCup;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (TycoonGameManager.instance.currentGameState != GameState.Playing ||
            TycoonGameManager.instance.currentStage != GameStage.ChooseCup)
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
                    if (hit.collider.tag == "Hot Cup")
                    {
                        Debug.Log("choose hot cup");
                        TycoonGameManager.instance.curCup = hotCup;
                        TycoonGameManager.instance.MoveNextStage();
                       
                    }
                    else if (hit.collider.tag == "Ice Cup")
                    {
                        Debug.Log("choose ice cup");
                        TycoonGameManager.instance.curCup = iceCup;
                        TycoonGameManager.instance.MoveNextStage();
                    }
                    return;
                }
            }
        }
    }
}
