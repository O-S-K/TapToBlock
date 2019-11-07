using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SS.View;

public class GAMEController : Controller
{
    public const string GAME_SCENE_NAME = "GAME";
    public static GAMEController instance;

    [SerializeField] TextMeshProUGUI levetext;

    public override string SceneName()
    {
        return GAME_SCENE_NAME;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levetext.text = "LEVEL: " + GameManager.indexStage.ToString();
    }

    public void ReplayGame()
    {
        Manager.Load(GAME_SCENE_NAME);
    }
}