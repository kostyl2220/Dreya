using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSendPreparationState : SimpleBrainState
{
    [SerializeField] private GameObject m_fearDust;
    [SerializeField] private float m_waitTime = 1.0f;

    private SimpleBrainState m_senderState;
    private float m_setNextStateTime;

    public override string GetStateName()
    {
        return "FearSendPreparationState";
    }

    public void ActivateSmoke(bool activate)
    {
        m_fearDust.SetActive(activate);
    }

    public override void Setup()
    {
        ActivateSmoke(true);
        m_setNextStateTime = Time.time + m_waitTime;
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
