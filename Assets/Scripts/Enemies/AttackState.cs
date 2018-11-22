using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FollowState
{
    [SerializeField] private float m_passiveFearDamage = 3.0f;
    [SerializeField] private float m_contactDamage = 20.0f;
    [SerializeField] private SimpleBrain.MinMaxRange m_AttackDistance = new SimpleBrain.MinMaxRange( 0.2f, 1.0f );
    //[SerializeField] private float 

    private SimpleBrainState m_chasingState;

    public override string GetStateName()
    {
        return "AttackState";
    }

    public override void Setup()
    {

    }

    public override bool UpdateState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, m_parent.m_player.transform.position);

        if (!m_parent.SeePlayer() || distanceToPlayer > m_AttackDistance.max)
        {
            return SetNewState(m_chasingState);
        }
      
        //not on max close distance
        if (distanceToPlayer > m_AttackDistance.min)
        {
            ShouldUpdatePath();
        }

        // perform passive damage
        Fearable fearable = m_parent.m_player.GetComponent<Fearable>();
        if (fearable)
        {
            float distancePersentage = 1.0f - distanceToPlayer / m_AttackDistance.max;
            float fear = distancePersentage * Time.deltaTime * Mathf.Pow(m_passiveFearDamage, 2);
            fearable.ChangeFear(fear);
        }

        return false;      
    }

    protected override void Finalized()
    {
        m_chasingState = GetComponent<ChasingState>();
    }
}
