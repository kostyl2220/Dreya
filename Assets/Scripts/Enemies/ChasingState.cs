using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : FollowState
{

    [SerializeField] private float m_endToPlayerDistance;

    private DecisionState m_searchingState;
    private AttackState m_attackState;

    public override bool UpdateState()
    {
        ShouldUpdatePath();

        float distanceToPlayer = Vector3.Distance(m_parent.m_agent.destination, transform.position);

        if (distanceToPlayer > m_endToPlayerDistance)
        {
            return false;
        }

        if (m_parent.SeePlayer())
        {
            return SetNewState(m_attackState);
        }

        if (distanceToPlayer > EPSILON)
        {
            return false;
        }

        return SetNewState(m_searchingState);
    }

    protected override void Finalized()
    {
        m_searchingState = GetComponent<DecisionState>();
        m_attackState = GetComponent<AttackState>();
    }
}
