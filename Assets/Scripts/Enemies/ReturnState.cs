using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : SimpleBrainState
{
    private SimpleBrainState m_decisionState;
    private SimpleBrainState m_chasingState;

    private Vector3 m_startPos;
    private bool m_startChasing;

    public override string GetStateName()
    {
        return "ReturnState";
    }

    public override void Setup()
    {
        m_startChasing = !m_startChasing;
        if (!m_startChasing)
        {
            m_parent.m_agent.destination = m_startPos;
        }
    }

    public override bool UpdateState()
    {
        if (m_startChasing)
        {
            m_startPos = transform.position;
            return SetNewState(m_chasingState);
        }

        if (m_parent.SeePlayer())
        {
            return SetNewState(m_chasingState);
        }

        if (m_parent.m_agent.remainingDistance > EPSILON)
        {
            return false;
        }

        return SetNewState(m_decisionState);
    }

    protected override void Finalized()
    {
        m_decisionState = GetComponent<DecisionState>();
        m_chasingState = GetComponent<ChasingState>();
        m_startChasing = false;
    }
}
