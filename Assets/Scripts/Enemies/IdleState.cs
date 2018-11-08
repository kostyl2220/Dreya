using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : SimpleBrainState {

    [SerializeField] private SimpleBrain.MinMaxRange m_waitTime;
    private DecisionState m_decisionState;

    public override bool UpdateState()
    {
        if (Time.time < m_parent.m_endActionTime)
        {
            return false;
        }
        SetNewState(m_decisionState);
        return true;
    }

    protected override void Finalized()
    {
        m_decisionState = GetComponent<DecisionState>();
    }

    protected override void Setup()
    {
        m_parent.m_endActionTime = Time.time + m_waitTime.GetInRange();
    }
}
