using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : FollowState
{
    [SerializeField] private float m_endToPlayerDistance = 1.0f;

    private SimpleBrainState m_searchingState;
    [SerializeField] private SimpleBrainState m_attackState;

    public override string GetStateName()
    {
        return "ChasingState";
    }

    public override bool UpdateState()
    {
        float distanceToTarget = m_parent.GetAgentRemainingDistance();

        if (m_parent.SeePlayer())
        {
            ShouldUpdatePath();
            return distanceToTarget > m_endToPlayerDistance ? false : SetNewState(m_attackState);
        }

        return distanceToTarget > EPSILON ? false : SetNewState(m_searchingState);
    }

    protected override void Finalized()
    {
        m_searchingState = GetComponent<SearchingState>();
        if (!m_attackState)
        {
            m_attackState = GetComponent<AttackState>();
        }
    }
}
