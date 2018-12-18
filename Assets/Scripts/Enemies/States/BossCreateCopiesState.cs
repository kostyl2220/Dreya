using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreateCopiesState : SimpleBrainState {

    [SerializeField] private SimpleBrainState m_nextState;
    [SerializeField] private ParentDamageReceive m_copy;
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_bossAttackTrigger = "BossAttack3";
    [SerializeField] private SimpleBrain.MinMaxRange m_countOfCopies = new SimpleBrain.MinMaxRange(2, 3);

    private bool m_animPlayed;

    public override string GetStateName()
    {
        return "BossCreateCopiesState";
    }

    public void OnAnimEnded()
    {
        //Create copies
        DamageReceiveComponent drc = GetComponent<DamageReceiveComponent>();

        int countOfCopies = (int)m_countOfCopies.GetInRange();
        for (int i = 0; i < countOfCopies; ++i)
        {
            ParentDamageReceive enemy = Instantiate(m_copy);
            enemy.SetParent(drc);
            m_copy.transform.position = transform.position;
            m_copy.transform.rotation = transform.rotation;
        }

        m_animPlayed = true;
    }

    public override void Setup()
    {
        m_animPlayed = false;
        if (m_animator)
        {
            m_animator.SetTrigger(m_bossAttackTrigger);
        }
    }

    public override bool UpdateState()
    {
        return (m_animPlayed ? SetNewState(m_nextState) : false);
    }

    protected override void Finalized()
    {
        
    }
}
