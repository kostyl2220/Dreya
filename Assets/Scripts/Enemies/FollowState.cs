using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FollowState : SimpleBrainState {
    static protected float EPSILON = 0.000001f;

    [SerializeField] private float m_pathUpdateTime = 0.2f;

    protected float m_lastUpdateTime;

    public override void Setup()
    {
        m_lastUpdateTime = Time.time;
        m_parent.m_agent.destination = m_parent.m_player.transform.position;
    }

    protected void ShouldUpdatePath()
    {
        if (Time.time < m_lastUpdateTime + m_pathUpdateTime)
        {
            return;
        }

        if (!m_parent.SeePlayer())
        {
            return;
        }

        m_lastUpdateTime = Time.time;
        m_parent.m_agent.destination = m_parent.m_player.transform.position;
    }
}
