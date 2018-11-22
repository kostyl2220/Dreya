using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : WanderState {

    [SerializeField] private SimpleBrain.MinMaxRange m_walkRadius = new SimpleBrain.MinMaxRange(2.0f, 5.0f);

    private bool m_reachedPos;

    public override string GetStateName()
    {
        return "WalkState";
    }

    protected override void Finalized()
    {
        m_chasingState = GetComponent<ReturnState>();
        m_decisionState = GetComponent<IdleState>();
    }

    public override void Setup()
    {
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

    protected override bool InnerUpdateState()
    {
        return m_parent.m_agent.remainingDistance > EPSILON ? false : SetNewState(m_decisionState);
    }
}
