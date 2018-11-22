using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionState : SimpleBrainState {

    [System.Serializable]
    public class StateChance
    {
        [SerializeField]
        public SimpleBrainState state;
        [SerializeField]
        public float chance;

        public void SetNewChance(float newChance)
        {
            this.chance = newChance;
        }
    }

    [SerializeField] private List<StateChance> m_chances;
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
        if (m_searching && Time.deltaTime > m_lastUpdateTime)
        {
            m_searching = false;
            return SetNewState(m_returnState);
        }
        return SetNewState(GetNextRandomAIState());
    }

    public void SetSearch()
    {
        m_searching = true;
    }

    public override void Setup()
    {
        if (m_searching)
        {
            m_lastUpdateTime = Time.time + m_searchTime;
        }
    }

    protected override void Finalized()
    {
        NormalizeChances();
        if (!m_returnState)
        {
            m_returnState = GetComponent<ReturnState>();
        }
    }

    SimpleBrainState GetNextRandomAIState()
    {
        float random = Random.Range(0.0f, 1.0f);
        float curVal = 0;
        for (int i = 0; i < m_chances.Count; ++i)
        {
            curVal += m_chances[i].chance;
            if (random <= curVal)
            {
                return m_chances[i].state;
            }
        }
        return null;
    }

    void NormalizeChances()
    {
        float sum = 0;
        foreach (var stateChance in m_chances)
        {
            sum += stateChance.chance;
        }
        for (int i = 0; i < m_chances.Count; ++i)
        {
            m_chances[i].SetNewChance(m_chances[i].chance / sum);
        }
    }
}
