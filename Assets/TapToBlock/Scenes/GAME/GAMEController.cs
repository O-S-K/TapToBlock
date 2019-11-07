using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using SS.View;

public class GAMEController : Controller
{
    public const string GAME_SCENE_NAME = "GAME";

    public static GAMEController instance;

    public override string SceneName()
    {
        return GAME_SCENE_NAME;
    }

    private void Awake()
    {
        instance = this;
    }

    public void ReplayGame()
    {
        Manager.Load(GAME_SCENE_NAME);
    }
}