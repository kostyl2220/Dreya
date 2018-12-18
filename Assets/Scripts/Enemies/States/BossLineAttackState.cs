using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLineAttackState : FollowState {

    [SerializeField] private BossFearLine m_line;
    [SerializeField] private float m_lineSpeed = 2.0f;
    [SerializeField] private SimpleBrainState m_nextState;
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_bossAttackTrigger = "BossAttack1";
    [SerializeField] private SimpleBrain.MinMaxRange m_countOfLines = new SimpleBrain.MinMaxRange(2, 3);

    private int m_leftLineCount;

    public override string GetStateName()
    {
        return "BossLineAttackState";
    }

    public override void Setup()
    {
        m_leftLineCount = m_countOfLines.GetInRangeInt();
        if (m_animator)
        {
            m_animator.SetTrigger(m_bossAttackTrigger);
        }
        m_parent.AgentSetUpdatePosition(false);
    }

    public void OnAnimEnd()
    {
        BossFearLine line = Instantiate(m_line);
        line.transform.position = transform.position;
        line.transform.rotation = transform.rotation;
        line.SetSpeed(m_lineSpeed);
        m_parent.AgentSetUpdatePosition(true);

        --m_leftLineCount;

        if (m_leftLineCount != 0)
        {
            m_animator.SetTrigger(m_bossAttackTrigger);
        }
    }

    public override bool UpdateState()
    {
        ShouldUpdatePath();      
        return (m_leftLineCount == 0 ? SetNewState(m_nextState) : false);
    }

    protected override void Finalized()
    {
      
    }
}
