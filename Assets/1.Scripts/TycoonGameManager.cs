using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public enum GameStage
{
    LevelSelect,
    Order,
    ChooseCup,
    OutShot,
    ChooseLiquid,
    SteamMilk,
    FillCup,
    LatteArt,
    Topping,
    Evalution,
}

public enum CupType
{
    Hot,
    Ice,
}

public enum LiquidType
{
    Milk,
    Water,
}

public enum Topping
{
    Cocoa,
    Caramel,
    JavaChip,
    Chocolate // 나중에 추가할 거 있으면 추가하는 방안으E
}

public enum LatteArt
{
    Angel,
    Flower,
    Heart,

}

[System.Serializable]
public struct CameraPos
{
    public GameStage gameStage;
    public Transform tr;
}

[System.Serializable]
public struct CupPos
{
    public GameStage gameStage;
    public Transform tr;
}

[System.Serializable]
public struct UIObjects
{
    public GameStage gameStage;
    public GameObject[] objects;
}

public class TycoonGameManager : MonoBehaviour
{
    public static TycoonGameManager instance;

    // 현재 게임 상태
    public GameState currentGameState = GameState.Playing;

    public GameStage currentStage = GameStage.LevelSelect;


    [Header("current Cup")]
    public Cup curCup;

    [SerializeField]
    public LiquidType curliquidType;

    [Header("camera and cup moving position")]
    [SerializeField] private CameraPos[] cameraPos;
    [SerializeField] private CupPos[] cupPos;

    [Header("UI")]
    [SerializeField] private UIObjects[] uiObjects;
    [SerializeField] private Slider timerSlider;
    [SerializeField] private GameObject TopUI;
    [SerializeField] private TMP_Text customerName;
    [SerializeField] private TMP_Text levelText;

    [Header("Managers")]
    [SerializeField] private OrderManager orderManager;

    private int level = -1;
    private Coroutine timerCoroutine;

    [HideInInspector] public Order curOrder;

    

    private void Start()
    {
        // GameManager의 인스턴스를 설정
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("GameManager 인스턴스가 이미 존재합니다.");
        }

        // DEBUG
        StartGame(1);

    }

    private void Update()
    {
        if (currentGameState != GameState.Playing) return;

        //DEBUG
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveNextStage();
        }

        
    }

    public void StartGame(int level)
    {
        this.level = level;
        MoveStage(GameStage.Order);
        timerCoroutine = StartCoroutine(decreaseTimer(60)); // test 60초

        orderManager.chooseOrder(level - 1);
        levelText.text = level + "일차";
        customerName.text = curOrder.name;
    }
    

    public void MoveStage(GameStage stage)
    {
        switch (currentStage)
        {
            case GameStage.FillCup:
                curCup.SetLine(false);
                curCup.SetTransparent(false);
                break;


        }

        currentStage = stage;
        Camera cam = Camera.main;


        // 스테이지 별로 카메라, 컵 위치 이동
        Transform _t;

        if ((_t = GetCameraPos(stage)) != null) {
            cam.transform.parent = _t;
            ResetTransform(cam.transform);
        }

        if ((_t = GetCupPos(stage)) != null && curCup != null)
        {
            curCup.gameObject.SetActive(true);
            curCup.transform.parent = _t;
            ResetTransform(curCup.transform);
        }

        switch (currentStage)
        {
            case GameStage.FillCup:
                curCup.SetLine(true);
                curCup.SetTransparent(true);
                break;
            
            
        }

        SetActiveUI();
    }

    private void ResetTransform(Transform tr)
    {
        if (tr == null) return;
        tr.localPosition = Vector3.zero;
        tr.localRotation = Quaternion.Euler(Vector3.zero);
        tr.localScale = Vector3.one;
    }

    public void MoveNextStage()
    {
        if (currentStage == GameStage.Evalution) MoveStage(GameStage.Order);
        else MoveStage((currentStage + 1));
    }

    private void SetActiveUI()
    {
        List<GameObject> uiList_active = new List<GameObject>();

        foreach(var ui in uiObjects)
        {
            if(ui.gameStage == currentStage)
            {
                //Debug.Log("SETACTIVE TRUE " + currentStage + ", ui: " + ui.gameStage);
                foreach (var _u in ui.objects)
                {
                    _u.SetActive(true);
                    uiList_active.Add(_u);
                }
            }
            else
            {
                //Debug.Log("SETACTIVE FALSE " + currentStage + ", ui: " + ui.gameStage);
                foreach (var _u in ui.objects)
                {
                    if (!uiList_active.Contains(_u))
                        _u.SetActive(false);
                }
            }
        }
    }

    private Transform GetCameraPos(GameStage g)
    {
        foreach(var c in cameraPos)
        {
            if(c.gameStage == g)
            {
                return c.tr;
            }
        }

        return null;
    }

    private Transform GetCupPos(GameStage g)
    {
        foreach (var c in cupPos)
        {
            if (c.gameStage == g)
            {
                return c.tr;
            }
        }

        return null;
    }

    // 다른 클래스에서 호출할 수 있는 예시 함수
    public void PauseGame()
    {
        if (currentGameState == GameState.Playing)
        {
            Time.timeScale = 0f; // 게임 일시 정지
            currentGameState = GameState.Paused;
            Debug.Log("Game Paused");
        }
    }

    public void ResumeGame()
    {
        if (currentGameState == GameState.Paused)
        {
            Time.timeScale = 1f; // 게임 재개
            currentGameState = GameState.Playing;
            Debug.Log("Game Resumed");
        }
    }

    public void EndGame()
    {
        if (currentGameState != GameState.GameOver)
        {
            Time.timeScale = 0f; // 게임 일시 정지
            currentGameState = GameState.GameOver;
            Debug.Log("Game Over");
        }
    }

    IEnumerator decreaseTimer(int limitTime)
    {
        float curTime = 0;
        while (curTime < limitTime) {
            if(currentGameState == GameState.Playing) {
                yield return new WaitForSeconds(0.2f);
                curTime += 0.2f;
                timerSlider.value = Mathf.Max(0, 1 - curTime / limitTime);
            }
        }

        EndGame();
    }
}

