using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSenderState : SimpleBrainState {

    [SerializeField] private SimpleBrainState m_nextState;
    [SerializeField] private GameObject m_fear;
    [SerializeField] private float m_sendingTimeForOne = 1.2f;
    [SerializeField] private SimpleBrain.MinMaxRange m_fearCount = new SimpleBrain.MinMaxRange(3, 4);
    [SerializeField] private Animator m_animator;
    [SerializeField] private string m_bossAttackTrigger = "FearCastEnd";

    private int m_countLeft;
    private float m_nextSendTime;
    private FearSendPreparationState m_senderPrepareState;

    public override string GetStateName()
    {
        return "FearSenderState";
    }

    public override void Setup()
    {
        m_nextSendTime = Time.time + m_sendingTimeForOne;
        m_countLeft = Random.Range((int)m_fearCount.min, (int)m_fearCount.max + 1);
    }

    public override bool UpdateState()
    {
        if (Time.time < m_nextSendTime)
        {
            return false;
        }

        if (m_countLeft == 0)
        {
            m_senderPrepareState.ActivateSmoke(false);
            if (m_animator)
            {
                m_animator.SetTrigger(m_bossAttackTrigger);
            }
            return SetNewState(m_nextState);
        }

        SendFear();
        --m_countLeft;
        m_nextSendTime = Time.time + m_sendingTimeForOne;
        return false;
    }

    protected override void Finalized()
    {
        if (!m_nextState)
        {
            m_nextState = GetComponent<DecisionState>();
        }
        m_senderPrepareState = GetComponent<FearSendPreparationState>();
    }

    private void SendFear()
    {
        GameObject fear = Instantiate(m_fear, transform.position, transform.rotation);
        fear.transform.Rotate(Vector3.up, Random.Range(0.0f, 360.0f));
    }
}
