using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolingState : WanderState {


    [SerializeField] private List<Transform> m_positions;
    [SerializeField] private SimpleBrainState m_idleState;
    [SerializeField] private bool m_cycled = false;

    private int m_nextPositionIndex;
    private bool m_directWalkSide;

    public override string GetStateName()
    {
        return "PatrolingState";
    }

    public override void Setup()
    {
        SetNextPos();
        m_parent.SetNewMovePosition(m_positions[m_nextPositionIndex].position);
    }

    private void SetNextPos()
    {
        if (m_cycled)
        {
            ++m_nextPositionIndex;
            if (m_nextPositionIndex >= m_positions.Count)
            {
                m_nextPositionIndex = 0;
            }
        }
        else
        {
            m_nextPositionIndex += (m_directWalkSide ? 1 : -1);
            if (m_nextPositionIndex < 0 || m_nextPositionIndex >= m_positions.Count)
            {
                m_directWalkSide = !m_directWalkSide;
                m_nextPositionIndex = (m_directWalkSide) ? 1 : (m_positions.Count - 2);
            }
        }
    }

    protected override bool InnerUpdateState()
    {
        return m_parent.GetAgentRemainingDistance() > EPSILON ? false : SetNewState(m_idleState);
    }

    protected override void Finalized()
    {
        m_nextPositionIndex = -1;
        m_directWalkSide = true;
    }
}
