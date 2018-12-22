using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//should realize only
// - GetStateName
// - InnerUpdateState
// - Setup

public abstract class WanderState : SimpleBrainState {

    [SerializeField] protected SimpleBrainState m_decisionState;
    [SerializeField] protected SimpleBrainState m_chasingState;

    protected float m_endActionTime;

    protected abstract bool InnerUpdateState();

    public override bool UpdateState()
    {
        if (m_parent.SeePlayer())
        {
            m_parent.SetAgressive(true);

            if (!m_chasingState)
            {
                m_chasingState = GetComponent<ReturnState>();
            }

            return SetNewState(m_chasingState);
        }

        return InnerUpdateState();
    }

    protected override void Finalized()
    {
        if (!m_decisionState)
        {
            m_decisionState = GetComponent<DecisionState>();
        }
        if (!m_chasingState)
        {
            m_chasingState = GetComponent<ReturnState>();
        }
    }
}
