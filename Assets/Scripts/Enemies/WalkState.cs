using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : SimpleBrainState {
    static float EPSILON = 0.000001f;

    [SerializeField] private SimpleBrain.MinMaxRange m_walkRadius;
    [SerializeField] private SimpleBrain.MinMaxRange m_waitTime;

    private DecisionState m_decisionState;
    private bool m_reachedPos;
    protected override void Finalized()
    {
        m_decisionState = GetComponent<DecisionState>();
    }

    protected override void Setup()
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

    public override bool UpdateState()
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

        SetNewState(m_decisionState);
        return true;
    }
}
