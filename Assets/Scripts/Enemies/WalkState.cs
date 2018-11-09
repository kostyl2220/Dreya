using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : WanderState {
    static float EPSILON = 0.000001f;

    [SerializeField] private SimpleBrain.MinMaxRange m_walkRadius = new SimpleBrain.MinMaxRange(2.0f, 5.0f);
    [SerializeField] private SimpleBrain.MinMaxRange m_waitTime = new SimpleBrain.MinMaxRange(0.5f, 2.0f);

    private bool m_reachedPos;

    public override void Setup()
    {
        m_reachedPos = false;
        m_parent.m_agent.destination = RandomNavmeshLocation(m_walkRadius.GetInRange());
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += m_parent.gameObject.transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private bool ReachedPosition()
    {
        return m_parent.m_agent.remainingDistance < EPSILON;
    }

    protected override bool InnerUpdateState()
    {
        if (!m_reachedPos)
        {
            if (!ReachedPosition())
            {
                return false;
            }
            m_reachedPos = true;
            m_parent.m_endActionTime = Time.time + m_waitTime.GetInRange();
        }
        if (Time.time < m_parent.m_endActionTime)
        {
            return false;
        }

        return SetNewState(m_decisionState);
    }
}
