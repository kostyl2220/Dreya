using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingState : SimpleBrainState {
    private DamageReceiveComponent m_drc;

    public override string GetStateName()
    {
        return "DyingState";
    }

    public override void Setup()
    {
        
    }

    public override bool UpdateState()
    {
        // later play dying anim??
        Debug.Log("Die...");
        Destroy(gameObject);
        return true;
    }

    protected override void Finalized()
    {
        m_drc = gameObject.GetComponent<DamageReceiveComponent>();
        if (m_drc)
        {
            m_drc.OnDeath += ((float damage) => { SetNewState(this); });
        }
    }
}
