using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiveState : SimpleBrainState {

    //weird variable name, forget about it
    private static float MAX_SPEED_FOR_MOVING = 0.1f;

    [SerializeField] private float m_afterDamageWalkDistance = 1.0f;

    private DamageReceiveComponent m_drc;
    private WalkState m_walkState;

    private Vector3 m_pushForce;

    public override string GetStateName()
    {
        return "DamageReceiveState";
    }

    public override void Setup()
    {

    }

    public override bool UpdateState()
    {
        m_walkState.SetForceWalkPos(m_parent.transform.position - m_pushForce * m_afterDamageWalkDistance);
        return SetNewState(m_walkState);
    }

    protected override void Finalized()
    {
        m_walkState = gameObject.GetComponent<WalkState>();
        m_drc = gameObject.GetComponent<DamageReceiveComponent>();
        if (m_drc)
        {
            m_drc.OnHit += ((float damage, Vector3 pushForce) => { m_pushForce = pushForce; SetNewState(this); });
        }
    }
}
