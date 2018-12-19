using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StraightAgentWalker : MovementAgent {

    static float EPSILON = 0.0001f;

    [SerializeField] private float m_speed = 2.0f;
    [SerializeField] private float m_rotationSpeed = 120.0f;
    [SerializeField] private float m_startSlowDown = 0.2f;

    private bool m_updateRotation;
    private bool m_updatePosition;
    private Vector3 m_destination;

    public override float GetAgentLookAngleDiff()
    {
        return Vector3.Angle(transform.forward, (m_destination - transform.position));
    }

    public override float GetRemainingDistance()
    {
        return Vector3.Distance(m_destination, transform.position);
    }

    public override void ResetPath()
    {
        m_destination = transform.position;
    }

    public override void SetDestination(Vector3 destination)
    {
        m_destination = destination;   
    }

    public override void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    // Use this for initialization
    void Start ()
    {
        m_updateRotation = true;
        m_updatePosition = true;
    }

    private float GetActualSpeed()
    {
        float distanceToTarget = Vector3.Distance(transform.position, m_destination);
        if (distanceToTarget > m_startSlowDown)
        {
            return m_speed;
        }
        return m_speed * distanceToTarget / m_startSlowDown;      
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_updateRotation)
        {
            Quaternion endAngle = Quaternion.LookRotation(m_destination - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, endAngle, m_rotationSpeed * Time.deltaTime);
        }

        if (GetRemainingDistance() < EPSILON)
        {
            return;
        }

        if (m_updatePosition)
        {
            gameObject.transform.position += gameObject.transform.forward * GetActualSpeed() * Time.deltaTime;
        }
	}

    public override void SetUpdateRotation(bool update)
    {
        m_updateRotation = update;
    }

    public override void SetUpdatePosition(bool update)
    {
        m_updatePosition = update;
    }
}
