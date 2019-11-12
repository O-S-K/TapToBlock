using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyBlock : MonoBehaviour
{
    [SerializeField] ParticleSystem m_ParticleBlock;

    void OnMouseDown()
    {
        if (GameManager.instance.isWin || GameManager.instance.isLose) { }
        else
        {
            Instantiate(m_ParticleBlock, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Block dơi ra khỏi màn hình thì Destroy
        if (collision.gameObject.CompareTag("Dead"))
        {
            Destroy(gameObject);
        }
    }
}
