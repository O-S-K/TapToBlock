using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool isCompleteLevel = false;
    public static int indexStageComplete = 1;

    [HideInInspector] public int indexStage = 1;
    [HideInInspector] public Stage stage;

    [SerializeField] GameObject m_PanelButton;
    [SerializeField] GameObject m_ParticleFinish;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        indexStage = PlayerPrefs.GetInt("SaveIdStage", 1);

        if (isCompleteLevel)
        {
            indexStage = indexStageComplete;
        }

        if (indexStage > Const.NUM_TOTAL_STAGE)
        {
            Manager.Load(PopupComingSoonController.POPUPCOMINGSOON_SCENE_NAME);
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
        SetComponetBall();
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

        FbaLogEvent.LogGameEvent("win_game", indexStage);

        PlayerPrefs.Save();
        yield return new WaitForSeconds(1);
        Manager.Add(WINGAMEController.WINGAME_SCENE_NAME);
    }

    public IEnumerator GameOver()
    {
        m_PanelButton.SetActive(false);

        yield return new WaitForSeconds(1);
        Manager.Add(GAMEOVERController.GAMEOVER_SCENE_NAME);
    }

    void SetComponetBall()
    {
        Ball.instance.m_Rigidbody.isKinematic = true;
        Ball.instance.m_Rigidbody.velocity = Vector3.zero;
        Ball.instance.m_Rigidbody.angularVelocity = 0;
    }
}
