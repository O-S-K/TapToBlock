using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public static Ball  instance;
    public Rigidbody2D  m_Rigidbody;
    [SerializeField] CircleCollider2D   circleCollider2D;
    [SerializeField] Animator           animatorPlayer;
    [SerializeField] GameObject         m_SpashShit;
    [SerializeField] ParticleSystem     m_ParticleDead;
    [SerializeField] Splatter           m_Splatter;

    GameObject m_Target;

    int countIsGround = 0;
    float timer = 1f;
    bool isGround = false;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        m_Target = GameObject.FindGameObjectWithTag("Target");
    }

    void Update()
    {
        if (isGround)
        {
            timer -= 0.1f;
        }

        CheckFallDownPlayer();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            countIsGround += 1;
            CheckFallDownPlayer();

            if (countIsGround == 2)
            {
                Splatter splatterObj = (Splatter)Instantiate(m_Splatter, transform.position, Quaternion.identity);
                splatterObj.transform.parent = collision.gameObject.transform;
                countIsGround = 1;
            }
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            circleCollider2D.isTrigger = true;
            transform.DOMove(m_Target.transform.position, .5F).OnComplete(() =>
            {
                m_Rigidbody.velocity = Vector2.zero;
                m_Rigidbody.gravityScale = 0;
            });

            StartCoroutine(GameManager.instance.WaitNextStage());
        }

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Dead"))
        {
            GameManager.countDieShowSkip += 1;

            animatorPlayer.CrossFadeInFixedTime("Player_Dead", 0.1f);
            circleCollider2D.isTrigger = true;
            Instantiate(m_ParticleDead, transform.position, Quaternion.identity);
            StartCoroutine(GameManager.instance.GameOver());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            isGround = true;
        }
    }

    void CheckFallDownPlayer()
    {
        if (timer <= 0 && countIsGround == 2)
        {
            Instantiate(m_SpashShit, transform.position, Quaternion.identity);
            timer = 1f;
            isGround = false;
        }
    }

}
