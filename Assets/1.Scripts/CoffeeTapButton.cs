using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeTapButton : MonoBehaviour
{
    public enum ButtonNumber { TAP1, TAP2, TAP3, TAP4}

    public ButtonNumber number;
    [HideInInspector] public bool isActive = false;
    //[SerializeField] Transform coffeeOutPos;
    [SerializeField] MeshRenderer buttonMesh;

    [SerializeField] Transform shotTr;
    [SerializeField] Material yellow, green, gray;
    [SerializeField] SphereCollider shotCup;

    [HideInInspector]
    public bool isFilled = false;

    private float shotStartY = -0.38f;
    private float shotPauseY = -0.78f;
    private float shotEndY = -1.21f;

    private float dropTime = 0.2f;
    private float pauseTime = 1f;

    private bool dragging = false;
    private bool isAnimating = false;

    Vector3 diff;
    Vector3 originPos;

    Coroutine coffeeShot;
    Coroutine coffeeDrop;

    private void Start()
    {
        originPos = shotCup.transform.position;
    }

    private void Update()
    {
        if (TycoonGameManager.instance.currentGameState != GameState.Playing ||
            TycoonGameManager.instance.currentStage != GameStage.OutShot)
            return;

        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 pos = touch.position;

            if (!dragging && touch.phase == TouchPhase.Began)
            {

                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (isFilled && !isAnimating && hit.collider == shotCup)
                    {
                        dragging = true;
                        diff = shotCup.transform.position - ray.origin;
                        Debug.Log("shot click");
                    }
                }
            }
            else if (dragging && touch.phase == TouchPhase.Moved)
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);


                shotCup.transform.position = ray.GetPoint(diff.magnitude);
            }
            else if (dragging && touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.tag == "Coffee Cup")
                    {
                        TycoonGameManager.instance.curCup.AddShot();
                        Debug.Log("shot drop");
                        coffeeDrop = StartCoroutine(CoffeeDropAnimation());
                    }
                    else shotCup.transform.position = originPos;

                    dragging = false;
                }
            }

        }
        else
        {
            // 드래그 중간에 다른 Stage로 넘어가는 경우, 오류 방지
            dragging = false;

            if(!isAnimating)
                shotCup.transform.position = originPos;
        }
    }

    public void setActive(bool isActive)
    {
        this.isActive = isActive;

        if (isActive)
        {
            buttonMesh.material = green;
            coffeeShot = StartCoroutine(CoffeeShot());
        }
        
    }

    IEnumerator CoffeeShot()
    {

        float curTime = 0;
        shotTr.gameObject.SetActive(true);
        // drop
        while (curTime < dropTime)
        {
            yield return null;
            curTime += Time.deltaTime;
            shotTr.localPosition = new Vector3(shotTr.localPosition.x, Mathf.Lerp(shotStartY, shotPauseY, curTime/dropTime), shotTr.localPosition.z);
        }
     
        curTime = 0;
        // pause
        while (curTime < pauseTime)
        {
            yield return null;
            curTime += Time.deltaTime;
        }
    
        curTime = 0;
        // drop
        while (curTime < dropTime)
        {
            yield return null;
            curTime += Time.deltaTime;
            shotTr.localPosition = new Vector3(shotTr.localPosition.x, Mathf.Lerp(shotPauseY, shotEndY, curTime / dropTime), shotTr.localPosition.z);
        }

        buttonMesh.material = gray;
        shotTr.gameObject.SetActive(false);
        isFilled = true;
    }

    IEnumerator CoffeeDropAnimation()
    {
        isAnimating = true;
        Vector3 posOffset = new Vector3(0, (TycoonGameManager.instance.curCup.cupType == CupType.Hot) ? 0.26f : 0.51f, 0.14f); 
        Vector3 rotOffset = new Vector3(-60f, 0f, 0f);
        Vector3 originRot = shotCup.transform.rotation.eulerAngles;

        float aniTime = 0.5f;
        float curTime = 0;
        shotCup.transform.position = TycoonGameManager.instance.curCup.transform.position + posOffset;
        while (curTime < aniTime)
        {
            shotCup.transform.rotation = Quaternion.Euler(originRot + (rotOffset - originRot) * Mathf.Pow((curTime/aniTime), 2));
            yield return null;
            curTime += Time.deltaTime;
        }

        shotCup.transform.position = originPos;
        shotCup.transform.rotation = Quaternion.Euler(originRot);
        isAnimating = false;
    }

    public void setReset()
    {
        buttonMesh.material = yellow;
        isFilled = false;
    }
}
