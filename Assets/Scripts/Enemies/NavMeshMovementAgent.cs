﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementAgent : MovementAgent {
    [SerializeField] private NavMeshAgent m_agent;

    public override float GetAgentLookAngleDiff()
    {
        return Vector3.Angle(transform.forward, (m_agent.steeringTarget - transform.position));
    }

    public override Vector3 GetDestination()
    {
        return m_agent.destination;
    }

    public override float GetRemainingDistance()
    {
        return m_agent.remainingDistance;
    }

    public override void ResetPath()
    {
        m_agent.ResetPath();
    }

    public override void SetDestination(Vector3 destination)
    {
        m_agent.destination = destination;
    }

    public override void SetSpeed(float speed)
    {
        m_agent.speed = speed;
    }

    public override void SetUpdatePosition(bool update)
    {
        m_agent.updatePosition = update;
    }

    public override void SetUpdateRotation(bool update)
    {
        m_agent.updateRotation = update;
    }

    // Use this for initialization
    void Start () {
		if (!m_agent)
        {
            m_agent = GetComponent<NavMeshAgent>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
