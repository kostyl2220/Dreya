using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WanderState : SimpleBrainState {

    protected DecisionState m_decisionState;
    protected ChasingState m_chasingState;

    protected abstract bool InnerUpdateState();

    public override bool UpdateState()
    {
        if (m_parent.SeePlayer())
        {
            return SetNewState(m_chasingState);
        }

        return InnerUpdateState();
    }

    protected override void Finalized()
    {
        m_decisionState = GetComponent<DecisionState>();
        m_chasingState = GetComponent<ChasingState>();
    }
}
