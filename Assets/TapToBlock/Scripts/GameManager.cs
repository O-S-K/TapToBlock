using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int indexStage = 1;
    

    [HideInInspector] public Stage stage;
    [SerializeField] GameObject m_BtnReset;
    [SerializeField] GameObject m_BtnSound;
    [SerializeField] GameObject m_ParticleFinish;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadStagePrefab(indexStage);
    }

    void LoadStagePrefab(int index)
    {
        stage = Instantiate(Resources.Load<Stage>("Level/Stage " + index.ToString()) as Stage);
    }

    public IEnumerator WaitNextStage()
    {
        //run animation win
        SetBallWhenDead();
        m_BtnSound.SetActive(false);
        m_ParticleFinish.SetActive(true);

        if (indexStage < 50)
        {
            indexStage += 1;
        }
        else
        {
            Manager.Load(PopupComingSoonController.POPUPCOMINGSOON_SCENE_NAME);
        }

        m_BtnReset.SetActive(false);
        yield return new WaitForSeconds(1);
        Manager.Add(WINGAMEController.WINGAME_SCENE_NAME);
    }

    public IEnumerator GameOver()
    {
        //run animation dead

        SetBallWhenDead();
        m_BtnSound.SetActive(false);
        m_BtnReset.SetActive(false);
        yield return new WaitForSeconds(1);
        Manager.Add(GAMEOVERController.GAMEOVER_SCENE_NAME);
    }

    void SetBallWhenDead()
    {
        Ball.instance.m_Rigidbody.isKinematic = true;
        Ball.instance.m_Rigidbody.velocity = Vector3.zero;
        Ball.instance.m_Rigidbody.angularVelocity = 0;
    }
}
