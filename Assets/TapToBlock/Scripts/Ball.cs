using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;

public class Ball : MonoBehaviour
{
    public static Ball instance;
    public Rigidbody2D m_Rigidbody;

    [SerializeField] GameObject m_SpashShit;
    [SerializeField] Splatter m_Splatter;

    // Check lần thứ 2 chạm Block thi mới spawn splash
    int countIsGround = 0;
    // check thời gian dơi của Shit khi ra ngoài mặt đất
    float timer = 1f;
    bool isGround = false;

    void Awake()
    {
        instance = this;
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
            StartCoroutine(GameManager.instance.WaitNextStage());
        }   

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Dead"))
        {
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
