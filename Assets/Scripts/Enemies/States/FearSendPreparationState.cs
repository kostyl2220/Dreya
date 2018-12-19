using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSendPreparationState : SimpleBrainState
{
    [SerializeField] private GameObject m_fearDust;
    [SerializeField] private float m_waitTime = 1.0f;
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_bossAttackTrigger = "BossAttack2";

    private GameObject m_fearDustInstance;
    private SimpleBrainState m_senderState;
    private float m_setNextStateTime;

    public override string GetStateName()
    {
        return "FearSendPreparationState";
    }

    public void ActivateSmoke(bool activate)
    {
        if (activate)
        {
            if (m_fearDustInstance)
            {
                Destroy(m_fearDustInstance);
            }
            m_fearDustInstance = Instantiate(m_fearDust, transform.position, transform.rotation, transform);
        }
        else if (m_fearDustInstance)
        {
            m_fearDustInstance.SetActive(activate);
        }
    }

    public override void Setup()
    {
        ActivateSmoke(true);
        m_setNextStateTime = Time.time + m_waitTime;
        if (m_animator)
        {
            m_animator.SetTrigger(m_bossAttackTrigger);
        }
    }

    public override bool UpdateState()
    {
        return (Time.time < m_setNextStateTime) ? false : SetNewState(m_senderState);
    }

    protected override void Finalized()
    {
        m_senderState = GetComponent<FearSenderState>();
    }
}
