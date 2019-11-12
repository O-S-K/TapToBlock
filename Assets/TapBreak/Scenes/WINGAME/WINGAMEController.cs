using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class WINGAMEController : Controller
{
    public const string WINGAME_SCENE_NAME = "WINGAME";

    public override string SceneName()
    {
        return WINGAME_SCENE_NAME;
    }

    public void NextStage()
    {
        Manager.Load(GAMEController.GAME_SCENE_NAME);
    }

    public void ResetLevel()
    {
        if (!GameManager.isCompleteLevel)
        {
            GameManager.instance.indexStage -= 1;
            PlayerPrefs.SetInt("SaveIdStage", GameManager.instance.indexStage);
            Manager.Load(GAMEController.GAME_SCENE_NAME);
        }
        else
        {
            GameManager.indexStageComplete -= 1;
            Manager.Load(GAMEController.GAME_SCENE_NAME);
        }
    }
}