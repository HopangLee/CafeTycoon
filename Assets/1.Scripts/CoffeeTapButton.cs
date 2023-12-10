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

    [HideInInspector]
    public bool isFilled = false;

    private float shotStartY = -0.38f;
    private float shotPauseY = -0.78f;
    private float shotEndY = -1.21f;

    private float dropTime = 0.2f;
    private float pauseTime = 1f;

    Coroutine coffeeShot;

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
        isFilled = true;
    }

    public void setReset()
    {
        buttonMesh.material = yellow;
        isFilled = false;
    }
}
