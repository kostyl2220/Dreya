using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingState : SimpleBrainState
{
    //defines length on which AI walks definitely after losing player
    [SerializeField] private float m_searchStep = 1.0f;

    private DecisionState m_decisionState;
    private WalkState m_walkState;

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
        m_walkState.SetForceWalkPos(m_parent.transform.position + m_parent.m_lastPlayerMoveDirection * m_searchStep);
        return SetNewState(m_walkState);
    }

    protected override void Finalized()
    {
        m_decisionState = GetComponent<DecisionState>();
        m_walkState = GetComponent<WalkState>();
    }
}
