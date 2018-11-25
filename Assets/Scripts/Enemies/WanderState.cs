using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WanderState : SimpleBrainState {

    protected SimpleBrainState m_decisionState;
    protected SimpleBrainState m_chasingState;

    protected float m_endActionTime;

    public void SetNextDecisionState(SimpleBrainState nextState)
    {
        m_decisionState = nextState;
    }

    protected abstract bool InnerUpdateState();

    public override bool UpdateState()
    {
        if (m_parent.SeePlayer())
        {
            m_parent.SetAgressive(true);
            return SetNewState(m_chasingState);
        }

        return InnerUpdateState();
    }

    protected override void Finalized()
    {
        m_decisionState = GetComponent<DecisionState>();
        m_chasingState = GetComponent<ReturnState>();
    }
}
