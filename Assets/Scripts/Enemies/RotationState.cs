using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationState : WanderState {

    static float EPSILON = 0.0001f;

    [SerializeField] private SimpleBrain.MinMaxRange m_countOfRotations = new SimpleBrain.MinMaxRange(2.0f, 6.0f);
    [SerializeField] private SimpleBrain.MinMaxRange m_angle = new SimpleBrain.MinMaxRange(15.0f, 78.0f);
    [SerializeField] private float m_rotationSpeed = 40.0f;

    private float m_fullRotationTime;
    private float m_rotationTime;
    private bool m_clockwise;
    private int m_currentCountOfRotations;
    private Quaternion m_startAngle;
    private Quaternion m_endAngle;

    protected override bool InnerUpdateState()
    {
        if (Quaternion.Angle(m_endAngle, transform.rotation) < EPSILON)
        {
            --m_currentCountOfRotations;
            if (m_currentCountOfRotations == 0)
            {
                return SetNewState(m_decisionState);             
            }        
        }
        else
        {
            m_rotationTime += Time.deltaTime;
            float stage = m_rotationTime / m_fullRotationTime;
            transform.rotation = Quaternion.Lerp(m_startAngle, m_endAngle, stage);
        }

        return false;
    }

    void SetupNewRotation()
    {
        float rotationAngle = m_angle.GetInRange();
        m_endAngle = GetNewRotation(rotationAngle);
        m_fullRotationTime = rotationAngle / m_rotationSpeed;
        m_rotationTime = 0;
        m_clockwise = !m_clockwise;
    }

    Quaternion GetNewRotation(float angleToRotate)
    {
        float rotationSide = m_clockwise ? 1 : -1;
        return m_startAngle * Quaternion.Euler(0.0f, rotationSide * m_angle.GetInRange(), 0.0f);
    }

    public override void Setup()
    {
        m_currentCountOfRotations = (int)m_countOfRotations.GetInRange();
        m_startAngle = transform.rotation;
        m_clockwise = Random.Range(0, 2) == 0;
        SetupNewRotation();
    }
}
