using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // If level IndexStage = 50 else run new Index is indexStageComplete
    public static bool isCompleteLevel = false;
    public static int indexStageComplete = 1;

    /// <summary>
    /// CountLose >= 2 Show Button Skip Level
    /// </summary>
    public static int countDieShowSKip = 0;

    [HideInInspector] public Stage stage;
    [HideInInspector] public int indexStage = 1;

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

        if (countDieShowSKip >= 2)
        {
            //SoundManager.instance.audioSound.PlayOneShot(SoundManager.instance.buttonSoundSkip);
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

    void LoadStagePrefab(int index)
    {
        stage = Instantiate(Resources.Load<Stage>("Level/Stage " + index.ToString()) as Stage);
    }

    public IEnumerator WaitNextStage()
    {
        //SoundManager.instance.audioSound.PlayOneShot(SoundManager.instance.winGameSound);
        countDieShowSKip = 0;

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

        FbaLogEvent.LogGameEvent("win_game", indexStage.ToString());

        PlayerPrefs.Save();
        yield return new WaitForSeconds(1);
        Manager.Add(WINGAMEController.WINGAME_SCENE_NAME);
    }

    public IEnumerator GameOver()
    {
        //SoundManager.instance.audioSound.PlayOneShot(SoundManager.instance.gameOverSound);
        isLose = true;
        m_PanelButton.SetActive(false);
        yield return new WaitForSeconds(1);
        Manager.Add(GAMEOVERController.GAMEOVER_SCENE_NAME);
    }
}
