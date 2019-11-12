using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // If level IndexStage = 50 else run new Index is indexStageComplete
    public static bool  isCompleteLevel = false;
    public static int   indexStageComplete = 1;

    /// <summary>
    /// CountLose >= 2 Show Button Skip Level
    /// </summary>
    public static int countDieShowSkip = 0;

    [HideInInspector] public Stage  stage;
    [HideInInspector] public int    indexStage = 1;

    /// <summary>
    /// If Win Else Can't Touch Destroy Block
    /// </summary>
    [HideInInspector] public bool isWin = false;
    [HideInInspector] public bool isLose = false;

    public GameObject m_SkipLevel;

    /// <summary>
    /// When Win Or Lose else Hide buttons in Scene
    /// </summary>
    [SerializeField] GameObject m_PanelButton;
    [SerializeField] GameObject m_ParticleFinish;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        indexStage = PlayerPrefs.GetInt("SaveIdStage", 1);

        if (countDieShowSkip >= 2)
        {
            m_SkipLevel.SetActive(true);
        }

        if (isCompleteLevel)
        {
            indexStage = indexStageComplete;
        }

        if (indexStage > Const.NUM_TOTAL_STAGE)
        {
            Manager.Add(PopupComingSoonController.POPUPCOMINGSOON_SCENE_NAME);
        }
        else
        {
            LoadStagePrefab(indexStage);
        }
    }

    public IEnumerator WaitNextStage()
    {
        countDieShowSkip = 0;

        isWin = true;
        m_PanelButton.SetActive(false);
        m_ParticleFinish.SetActive(true);

        if (!isCompleteLevel)
        {
            indexStage += 1;
            PlayerPrefs.SetInt("SaveIdStage", indexStage);
        }
        else
        {
            indexStageComplete += 1;
        }

        PlayerPrefs.Save();
        FbaLogEvent.LogGameEvent("win_game", indexStage.ToString());

        yield return new WaitForSeconds(1);
        Manager.Add(WINGAMEController.WINGAME_SCENE_NAME);

        //Add Ads when IndexStage level 5
        if (indexStage >= Const.START_ADS && AdsWrapper.instance != null)
        {
            if (Time.realtimeSinceStartup - AdsWrapper.instance.interstitialTime >= Const.TIME_BETWEEN_ADS)
            {
                AdsWrapper.instance.ShowInterstitial();
            }
        }
    }

    void LoadStagePrefab(int index)
    {
        stage = Instantiate(Resources.Load<Stage>("Level/Stage " + index.ToString()) as Stage);
    }

    public void OnButtonSkipLevelTap()
    {
        Manager.Add(PopupShowAdsController.POPUPSHOWADS_SCENE_NAME, new PopupData("Do you want to watch video to go to the next level?", PopupType.YES_NO, ShowRewardVideo));
    }

    void ShowRewardVideo()
    {
#if UNITY_EDITOR
        NextStage();
#else
        if (AdsWrapper.instance != null)
        {
            AdsWrapper.instance.ShowRewardBasedVideo((rewarded) =>
            {
                if (rewarded)
                {
                    NextStage();
                }
            });
        }
#endif
    }

    void NextStage()
    {
        m_SkipLevel.SetActive(false);
        countDieShowSkip = 0;

        if (!isCompleteLevel)
        {
            indexStage += 1;
            PlayerPrefs.SetInt("SaveIdStage", indexStage);
        }
        else
        {
            indexStageComplete += 1;
        }
        
        Manager.Load(GAMEController.GAME_SCENE_NAME);
    }

    public IEnumerator GameOver()
    {
        isLose = true;
        m_PanelButton.SetActive(false);
        yield return new WaitForSeconds(.5F);
        Manager.Add(GAMEOVERController.GAMEOVER_SCENE_NAME);
    }
}
