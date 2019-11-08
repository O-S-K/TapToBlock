using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class PopupComingSoonController : Controller
{
    public const string POPUPCOMINGSOON_SCENE_NAME = "PopupComingSoon";

    public override string SceneName()
    {
        return POPUPCOMINGSOON_SCENE_NAME;
    }

    public void RatingGame()
    {
        NativeReviewRequest.RequestReview(Const.APP_URL);
    }

    public void ReplayLevel()
    {
        GameManager.isCompleteLevel = true;
        GameManager.indexStageComplete = 1;
        Manager.Load(GAMEController.GAME_SCENE_NAME);
    }
}