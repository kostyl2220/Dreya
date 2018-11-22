using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : WanderState {

    [SerializeField] private SimpleBrain.MinMaxRange m_waitTime = new SimpleBrain.MinMaxRange(1.0f, 2.2f);

    public override string GetStateName()
    {
        return "IdleState";
    }
 

    protected override bool InnerUpdateState()
    {
        return Time.time < m_endActionTime ? false : SetNewState(m_decisionState);
    }

    public override void Setup()
    {
        m_endActionTime = Time.time + m_waitTime.GetInRange();
    }
}
