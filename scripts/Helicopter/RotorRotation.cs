using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorRotation : MonoBehaviour
{
    public enum Axis
    {
        x,
        y,
        z
    }


    public bool m_inverseRotation = false;
    public Axis m_rotateAxis;


    private Vector3 m_rotation;
    private float m_rotateDegree;


    private float m_rotorSpeed;
    public float rotorSpeed
    {
        get { return m_rotorSpeed; }
        set { m_rotorSpeed = Mathf.Clamp(value, 0, 3000f); }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inverseRotation)
        {
            m_rotateDegree -= m_rotorSpeed * Time.deltaTime;
        }
        else
        {
            m_rotateDegree += m_rotorSpeed * Time.deltaTime;
        }
        m_rotateDegree = m_rotateDegree % 360;

        switch (m_rotateAxis)
        {
            case Axis.y:
                transform.localRotation = Quaternion.Euler(m_rotation.x, m_rotateDegree, m_rotation.z);
                break;
            case Axis.z:
                transform.localRotation = Quaternion.Euler(m_rotation.x, m_rotation.y, m_rotateDegree);
                break;
            default:
                transform.localRotation = Quaternion.Euler(m_rotateDegree, m_rotation.x, m_rotation.z);
                break;
        }
    }
}
