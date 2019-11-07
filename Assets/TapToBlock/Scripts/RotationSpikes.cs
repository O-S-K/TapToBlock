using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSpikes : MonoBehaviour
{
    [SerializeField] float m_SpeedRotate = 90;
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, m_SpeedRotate * Time.deltaTime));
    }
}
