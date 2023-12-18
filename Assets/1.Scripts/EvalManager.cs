using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalManager : MonoBehaviour
{
    public void CheckList()
    {

    }

    public void RightButton()
    {
        bool isClear = true;

        if (isClear) ClearAndNextStage();
        else RetryStage();
    }

    public void ClearAndNextStage()
    {
        TycoonGameManager.instance.ClearAndNextStage();
    }

    public void RetryStage()
    {
        TycoonGameManager.instance.MoveNextStage();
    }
}
