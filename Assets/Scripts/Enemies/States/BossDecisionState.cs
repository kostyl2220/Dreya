using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDecisionState : DecisionState {

    [SerializeField] private SimpleBrain.MinMaxRange m_betweenSpecialAttackState = new SimpleBrain.MinMaxRange(6.0f, 10.0f);

    private bool m_specialAttack;
    private bool m_updateEnabled = false;

    public override string GetStateName()
    {
        return "BossDecisionState";
    }

    protected override void Finalized()
    {
        base.Finalized();
        m_specialAttack = false;
    }

    public override bool UpdateState()
    {
        m_specialAttack = !m_specialAttack;
        m_lastUpdateTime = Time.time + m_betweenSpecialAttackState.GetInRange();
        SimpleBrainState nextState = (m_specialAttack ? m_calmNode.GetNextRandomAIState() : m_searchNode.GetNextRandomAIState());
        return SetNewState(nextState);
    }

    public override void Setup()
    {
        m_updateEnabled = true;
        m_lastUpdateTime = Time.time + m_betweenSpecialAttackState.GetInRange();
    }
    
    void Update () {
        if (!m_updateEnabled)
        {
            return;
        }

        if (m_specialAttack)
        {
            return;
        }

        if (m_lastUpdateTime > Time.time)
        {
            return;
        }

        m_parent.AgentSetUpdatePosition(true);

        // switch to special attack state
        SetNewState(this);
    }
}
