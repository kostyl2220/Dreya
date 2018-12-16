using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikadzeAttackState : FollowState
{
    [SerializeField] private float m_kamikadzeSpeed = 3.0f;
    [SerializeField] private float m_contactDamage = 8.0f;
    [SerializeField] private float m_kamikadzeDistance = 1.5f;
    //[SerializeField] private float 

    private SimpleBrainState m_chasingState;
    private bool m_kamikadze;

    public override string GetStateName()
    {
        return "KamikadzeAttackState";
    }

    public override void Setup()
    {
        m_kamikadze = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != GameDefs.PLAYER_LAYER)
        {
            return;
        }

        Fearable fearable = other.gameObject.GetComponent<Fearable>();
        if (fearable)
        {
            fearable.ChangeFear(m_contactDamage);
            Destroy(gameObject);
        }
    }

    public override bool UpdateState()
    {
        if (!m_kamikadze)
        {
            return NormalUpdate();
        }
        return KamikadzeUpdate();
    }

    private bool KamikadzeUpdate()
    {
        float distanceToTarget = m_parent.GetAgentRemainingDistance();

        if (distanceToTarget < EPSILON)
        {
            return SetNewState(m_chasingState);
        }

        return false;
    }

    private bool NormalUpdate()
    {
        float distanceToTarget = m_parent.GetAgentRemainingDistance();

        if (!m_parent.SeePlayer(false))
        {
            return SetNewState(m_chasingState);
        }

        if (distanceToTarget < EPSILON)
        {
            return SetNewState(m_chasingState);
        }

        //not on max close distance
        if (distanceToTarget > m_kamikadzeDistance)
        {
            ShouldUpdatePath();
        }
        else if (m_parent.GetAgentLookAngleDiff() < EPSILON)
        {
            m_kamikadze = true;
            m_parent.SetAgentSpeed(m_kamikadzeSpeed);
        }

        return false;
    }

    protected override void Finalized()
    {
        m_chasingState = GetComponent<DyingState>();
    }
}
