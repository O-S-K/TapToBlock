using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTeleport : MonoBehaviour
{
    [SerializeField] Transform m_TargetEnd;
    [SerializeField] Transform m_TargetStart;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Start"))
        {
            transform.position = m_TargetEnd.transform.position;
            StartCoroutine(waitTimeChageTagEnd(.5F));
        }
        if (collision.gameObject.CompareTag("End"))
        {
            transform.position = m_TargetStart.transform.position;
            StartCoroutine(waitTimeChageTagStart(.5F));
        }
    }

    IEnumerator waitTimeChageTagStart(float t)
    {
        m_TargetStart.gameObject.tag = "Untagged";
        yield return new WaitForSeconds(t);
        m_TargetStart.gameObject.tag = "Start";
    }

    IEnumerator waitTimeChageTagEnd(float t)
    {
        m_TargetEnd.gameObject.tag = "Untagged";
        yield return new WaitForSeconds(t);
        m_TargetEnd.gameObject.tag = "End";
    }

}
