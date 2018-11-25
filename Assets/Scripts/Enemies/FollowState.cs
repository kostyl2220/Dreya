using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FollowState : SimpleBrainState
{
    [SerializeField] private float m_pathUpdateTime = 0.2f;

    protected float m_lastUpdateTime;

    public override void Setup()
    {
        m_lastUpdateTime = Time.time + m_pathUpdateTime;

        if (m_parent.SeePlayer())
        {
            m_parent.SetNewMovePosition(m_parent.m_player.transform.position);
        }
    }

    protected void ShouldUpdatePath()
    {
        if (Time.time < m_lastUpdateTime)
        {
            return;
        }

        m_lastUpdateTime = Time.time + m_pathUpdateTime;
        m_parent.SetNewMovePosition(m_parent.m_player.transform.position);
        m_parent.m_lastPlayerMoveDirection = m_parent.transform.forward;
    }
}
