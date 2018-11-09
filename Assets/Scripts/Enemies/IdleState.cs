using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : WanderState {

    [SerializeField] private SimpleBrain.MinMaxRange m_waitTime = new SimpleBrain.MinMaxRange(1.0f, 2.2f);

    protected override bool InnerUpdateState()
    {
        if (Time.time < m_parent.m_endActionTime)
        {
            return false;
        }
        return SetNewState(m_decisionState);
    }

    public override void Setup()
    {
        m_parent.m_endActionTime = Time.time + m_waitTime.GetInRange();
    }
}
