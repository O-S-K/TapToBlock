using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] float m_MoveX;
    [SerializeField] float m_MoveY;
    [SerializeField] float m_Speed;

    Vector3 initPosition;

    void Start()
    {
        initPosition = transform.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, initPosition + new Vector3(m_MoveX, m_MoveY, 0), m_Speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, initPosition + new Vector3(m_MoveX, m_MoveY, 0)) < 0.001f)
        {
            m_MoveX *= -1;
            m_MoveY *= -1;
            initPosition = transform.position;
        }
    }
}
