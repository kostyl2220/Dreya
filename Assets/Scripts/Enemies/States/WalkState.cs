using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : WanderState
{

    [SerializeField] private SimpleBrain.MinMaxRange m_walkRadius = new SimpleBrain.MinMaxRange(2.0f, 5.0f);

    private new static float EPSILON = 0.1f;
    private bool m_forceWalk;

    public void SetForceWalkPos(Vector3 newPos)
    {
        m_forceWalk = true;
        m_parent.SetNewMovePosition(newPos);
    }

    public override string GetStateName()
    {
        return "WalkState";
    }

    public override void Setup()
    {
        if (!m_forceWalk)
        {
            m_parent.SetNewMovePosition(RandomNavmeshLocation(m_walkRadius.GetInRange()));
        }
        m_forceWalk = false;
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += m_parent.gameObject.transform.position;
        return randomDirection;
    }

    protected override void Finalized()
    {
        if (!m_chasingState)
        {
            m_chasingState = GetComponent<ReturnState>();
        }
        if (!m_decisionState)
        {
            m_decisionState = GetComponent<IdleState>();
        }
    }

    protected override bool InnerUpdateState()
    {
        return m_parent.GetAgentRemainingDistance() > EPSILON ? false : SetNewState(m_decisionState);
    }
}
