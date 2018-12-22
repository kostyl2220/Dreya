using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : FollowState
{
    [SerializeField] private float m_rotationSpeed = 120.0f;
    [SerializeField] private float m_passiveFearDamage = 3.0f;
    [SerializeField] private float m_contactDamage = 8.0f;
    [SerializeField] private float m_cooldown = 1.0f;
    [SerializeField] private float m_contactAttackDistance = 0.3f;
    [SerializeField] private string m_attackStateName = "Attack";
    [SerializeField] private Animator m_anim;
    [SerializeField] private DamageDetector m_detector;
    [SerializeField] private SimpleBrain.MinMaxRange m_AttackDistance = new SimpleBrain.MinMaxRange( 0.2f, 1.0f );
    //[SerializeField] private float 

    private bool m_isAttacking;
    private float m_attackCooldownTime;
    private SimpleBrainState m_chasingState;

    public override string GetStateName()
    {
        return "AttackState";
    }

    public void OnAttackEnded()
    {
        m_isAttacking = false;
    }

    public override void Setup()
    {
        if (m_detector)
        {
            m_detector.OnDetect += OnHit;
        }
    }

    private void OnHit(GameObject gameObject)
    {
        if (!m_isAttacking)
        {
            return;
        }

        Fearable fearable = gameObject.GetComponent<Fearable>();
        if (fearable)
        {
            fearable.ChangeFear(m_contactDamage);
        }
        m_isAttacking = false;
    }

    public override bool UpdateState()
    {
        {
            Quaternion endAngle = Quaternion.LookRotation(m_parent.GetAgentDestination() - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, endAngle, m_rotationSpeed * Time.deltaTime);
        }

        if (m_isAttacking)
        {
            return false;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, m_parent.m_player.transform.position);

        if (!m_parent.SeePlayer(false) || distanceToPlayer > m_AttackDistance.max)
        {
            m_parent.AgentSetUpdatePosition(true);
            return SetNewState(m_chasingState);
        }
      
        //not on max close distance
        if (distanceToPlayer > m_AttackDistance.min)
        {
            m_parent.AgentSetUpdatePosition(true);
            ShouldUpdatePath();
        }
        else
        {
            m_parent.AgentSetUpdatePosition(false);
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
                    if (m_detector)
                    {
                        m_isAttacking = true;
                        m_detector.SetPerformingAttack(true);
                    }
                }

                m_parent.AgentSetUpdatePosition(false);
                m_attackCooldownTime = Time.time + m_cooldown;               
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
