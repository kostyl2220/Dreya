using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionState : SimpleBrainState {

    [SerializeField] protected DecisionStateNode m_calmNode;
    [SerializeField] protected DecisionStateNode m_searchNode;
    [SerializeField] protected float m_searchTime = 5.0f;

    private SimpleBrainState m_returnState;

    protected float m_lastUpdateTime;
    private bool m_searching;

    public override string GetStateName()
    {
        return "DecisionState";
    }

    public override bool UpdateState()
    {
        if (m_searching)
        {
            if (Time.time > m_lastUpdateTime)
            {
                m_searching = false;
                return SetNewState(m_returnState);
            }
            if (m_searchNode)
            {
                return SetNewState(m_searchNode.GetNextRandomAIState());
            }
        }
        if (m_calmNode)
        {
            return SetNewState(m_calmNode.GetNextRandomAIState());
        }
        return SetNewState(m_returnState);
    }

    public void SetSearch()
    {
        m_searching = true;
        m_lastUpdateTime = Time.time + m_searchTime;
    }

    protected override void Finalized()
    {
        if (!m_returnState)
        {
            m_returnState = GetComponent<ReturnState>();
        }
    }

    public override void Setup()
    {
        
    }
}
