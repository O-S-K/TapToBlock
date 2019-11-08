using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SS.View;

public class LoadingController : Controller
{
    public const string LOADING_SCENE_NAME = "Loading";
    [SerializeField] Slider slider;
    float i = 0;

    public override string SceneName()
    {
        return LOADING_SCENE_NAME;
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
    }
    void Update()
    {
        if (i <= 1)
        {
            i += .75f * Time.deltaTime;
            slider.value = i;
        }
        else
        {
            FirebaseManager.CheckGooglePlayService();
            Manager.Load(GAMEController.GAME_SCENE_NAME);
            this.enabled = false;
        }
    }
}