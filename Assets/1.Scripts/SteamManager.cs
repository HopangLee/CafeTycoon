using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SteamManager : MonoBehaviour
{
    [SerializeField] private Image button;
    [SerializeField] private Sprite buttonStartImg;
    [SerializeField] private Sprite buttonStopImg;
    [SerializeField] private TMP_Text tempText;
    [SerializeField] private TMP_Text buttonText;
    private bool isActive = false;
    private float temperature;
    Coroutine temperatureUp;

    public void onClickButton()
    {
        if (!isActive)
        {
            isActive = true;
            buttonText.text = "STOP";
            button.sprite = buttonStopImg;
            temperatureUp =  StartCoroutine(TemperatureUp());
        }
        else
        {
            isActive = false;
            buttonText.text = "START";
            button.sprite = buttonStartImg;
            TycoonGameManager.instance.MoveNextStage();
        }
    }

    IEnumerator TemperatureUp()
    {
        temperature = 0;
        while (isActive)
        {
            yield return new WaitForSeconds(0.1f);
            temperature++;
            tempText.text = temperature + "";
        }
        TycoonGameManager.instance.curCup.setTemp(temperature);
    }
}
