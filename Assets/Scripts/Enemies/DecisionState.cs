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

    public override bool UpdateState()
    {
       SetNewState(GetNextRandomAIState());
        return true;
    }

    protected override void Setup()
    {

    }

    protected override void Finalized()
    {
        NormalizeChances();
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
