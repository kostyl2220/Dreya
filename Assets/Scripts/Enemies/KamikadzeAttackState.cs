using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikadzeAttackState : FollowState
{
    [SerializeField] private float m_passiveFearDamage = 3.0f;
    [SerializeField] private float m_contactDamage = 8.0f;
    [SerializeField] private float m_contactAttackDistance = 0.3f;
    [SerializeField] private SimpleBrain.MinMaxRange m_AttackDistance = new SimpleBrain.MinMaxRange(0.2f, 1.0f);
    //[SerializeField] private float 

    private SimpleBrainState m_chasingState;

    public override string GetStateName()
    {
        return "KamikadzeAttackState";
    }

    public override void Setup()
    {

    }

    public override bool UpdateState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, m_parent.m_player.transform.position);

        if (!m_parent.SeePlayer(false) || distanceToPlayer > m_AttackDistance.max)
        {
            return SetNewState(m_chasingState);
        }

        //not on max close distance
        if (distanceToPlayer > m_AttackDistance.min)
        {
            ShouldUpdatePath();
        }

        Fearable fearable = m_parent.m_player.GetComponent<Fearable>();
        if (fearable)
        {
            // perform passive damage
            float distancePersentage = 1.0f - distanceToPlayer / m_AttackDistance.max;
            float fear = distancePersentage * Time.deltaTime * Mathf.Pow(m_passiveFearDamage, 2);
            fearable.ChangeFear(fear);

            // perform active damage
            if (distanceToPlayer < m_contactAttackDistance)
            {
                fearable.ChangeFear(m_contactDamage);
                Destroy(gameObject);
            }
        }

        return false;
    }

    protected override void Finalized()
    {
        m_chasingState = GetComponent<DyingState>();
    }
}
