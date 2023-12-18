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
    //[SerializeField] private TMP_Text tempText;
    [SerializeField] private Transform needle;
    [SerializeField] private Slider temperature_slider;
    [SerializeField] private TMP_Text buttonText;
    private bool isActive = false;
    private float temperature;
    Coroutine temperatureUp;

    private void Awake()
    {
        button.GetComponent<PointerDownUpHandler>().setSteamManager(this);
    }

    public void onPressedButton()
    {
        if (!isActive)
        {
            isActive = true;
            buttonText.text = "작동중";
            button.sprite = buttonStopImg;
            temperatureUp =  StartCoroutine(TemperatureUp());
        }
    }

    public void onReleasedButton()
    {
        if (isActive)
        {
            isActive = false;
            buttonText.text = "시 작";
            button.sprite = buttonStartImg;
            //StopCoroutine(temperatureUp);
            TycoonGameManager.instance.MoveNextStage();
        }
    }

    IEnumerator TemperatureUp()
    {
        temperature = 0;
        temperature_slider.value = 0;
        while (isActive)
        {
            yield return new WaitForSeconds(0.05f);
            temperature+=0.5f;
            //tempText.text = temperature + "";
            //needle.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Max(-temperature * 10, -264)));
            temperature_slider.value += 0.025f;
        }
        TycoonGameManager.instance.curCup.setTemp(temperature);
        //needle.rotation = Quaternion.Euler(Vector3.zero);
        temperature_slider.value = 0;
    }
}
