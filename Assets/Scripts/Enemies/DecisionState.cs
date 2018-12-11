using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionState : SimpleBrainState {

    [SerializeField] private DecisionStateNode m_calmNode;
    [SerializeField] private DecisionStateNode m_searchNode;
    [SerializeField] private float m_searchTime = 5.0f;

    private SimpleBrainState m_returnState;

    private float m_lastUpdateTime;
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

    public override void Setup()
    {

    }

    protected override void Finalized()
    {
        if (!m_returnState)
        {
            m_returnState = GetComponent<ReturnState>();
        }
    }
}
