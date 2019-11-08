using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int indexStage = 1;
    
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
        m_PanelButton.SetActive(false);
        m_ParticleFinish.SetActive(true);

        if (indexStage < 50)
        {
            indexStage += 1;
            PlayerPrefs.SetInt("SaveIdStage", indexStage);
        }
        else
        {
            Manager.Load(PopupComingSoonController.POPUPCOMINGSOON_SCENE_NAME);
        }

      
        PlayerPrefs.Save();
        yield return new WaitForSeconds(1);
        Manager.Add(WINGAMEController.WINGAME_SCENE_NAME);
    }

    public IEnumerator GameOver()
    {
        //run animation dead

        m_PanelButton.SetActive(false);
        
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
