using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using TMPro;

public class GAMEOVERController : Controller
{
    public const string GAMEOVER_SCENE_NAME = "GAMEOVER";

    public override string SceneName()
    {
        return GAMEOVER_SCENE_NAME;
    }

    public void ReplayGame()
    {
        Manager.Load(GAMEController.GAME_SCENE_NAME);
    }
}