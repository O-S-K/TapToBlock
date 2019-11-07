using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public Stage stage;
    public static int indexStage = 25;
    [Space]
    [Header("Particle Effect")]
    [SerializeField] ParticleSystem m_DeadBall;

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

        yield return new WaitForSeconds(1.5f);
        indexStage += 1;
        Manager.Load(GAMEController.GAME_SCENE_NAME);
    }

    public IEnumerator GameOver()
    {
        //run animation dead
        SetBallWhenDead();
        //Instantiate(m_DeadBall, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Manager.Add(GAMEOVERController.GAMEOVER_SCENE_NAME);
    }

    void SetBallWhenDead()
    {
        Ball.instance.m_Rigidbody.isKinematic = true;
        Ball.instance.m_Rigidbody.velocity = Vector3.zero;
        Ball.instance.m_Rigidbody.angularVelocity = 0;
    }
}
