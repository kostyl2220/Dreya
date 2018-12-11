using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FollowState
{
    [SerializeField] private float m_passiveFearDamage = 3.0f;
    [SerializeField] private float m_contactDamage = 8.0f;
    [SerializeField] private float m_cooldown = 1.0f;
    [SerializeField] private float m_contactAttackDistance = 0.3f;
    [SerializeField] private string m_attackStateName = "Attack";
    [SerializeField] private Animator m_anim;
    [SerializeField] private SimpleBrain.MinMaxRange m_AttackDistance = new SimpleBrain.MinMaxRange( 0.2f, 1.0f );
    //[SerializeField] private float 

    private float m_attackCooldownTime;
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
        else
        {
            m_parent.AgentForceStop();
        }

        
        Fearable fearable = m_parent.m_player.GetComponent<Fearable>();
        if (fearable)
        {
            // perform passive damage
            float distancePersentage = 1.0f - distanceToPlayer / m_AttackDistance.max;
            float fear = distancePersentage * Time.deltaTime * Mathf.Pow(m_passiveFearDamage, 2);
            fearable.ChangeFear(fear);

            // perform active damage
            if (distanceToPlayer < m_contactAttackDistance && m_attackCooldownTime < Time.time)
            {
                if (m_anim)
                {
                    m_anim.SetTrigger(m_attackStateName);
                }
                m_attackCooldownTime = Time.time + m_cooldown;
                fearable.ChangeFear(m_contactDamage);
            }
        }

        return false;      
    }

    protected override void Finalized()
    {
        m_chasingState = GetComponent<ChasingState>();
        m_attackCooldownTime = 0.0f;
    }
}
