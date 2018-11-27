using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiveState : SimpleBrainState {

    private DamageReceiveComponent m_drc;
    private ChasingState m_chasingState;

    public override string GetStateName()
    {
        return "DamageReceiveState";
    }

    public override void Setup()
    {

    }

    public override bool UpdateState()
    {
        return SetNewState(m_chasingState);
    }

    protected override void Finalized()
    {
        m_chasingState = gameObject.GetComponent<ChasingState>();
        m_drc = gameObject.GetComponent<DamageReceiveComponent>();
        if (m_drc)
        {
            m_drc.OnHit += ((float damage) => { SetNewState(this); });
        }
    }
}
