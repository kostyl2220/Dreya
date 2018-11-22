using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingState : SimpleBrainState
{
    private DecisionState m_decisionState;

    public override string GetStateName()
    {
        return "SearchingState";
    }

    public override void Setup()
    {
        
    }

    public override bool UpdateState()
    {
        m_decisionState.SetSearch();
        return SetNewState(m_decisionState);
    }

    protected override void Finalized()
    {
        m_decisionState = GetComponent<DecisionState>();
    }
}
