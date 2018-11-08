using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class SimpleBrain : MonoBehaviour {

    [System.Serializable]
    public struct MinMaxRange
    {
        [SerializeField]
        float min;
        [SerializeField]
        float max;

        public float GetInRange()
        {
            return Random.Range(min, max);
        }
    }

    public void SetState(SimpleBrainState state)
    {
        m_currentState = state;
    }

    [SerializeField] private SimpleBrainState m_startupState;

    private SimpleBrainState m_currentState;
    public float m_endActionTime { get; set; }
    public NavMeshAgent m_agent { get; private set; }

	void Start () {
        m_endActionTime = -1;
        m_agent = GetComponent<NavMeshAgent>();
        if (m_agent)
        {
            m_agent.destination = transform.position;
        }
        SetState(m_startupState);
    }

    // Update is called once per frame
    void Update () {
        m_currentState.UpdateState();
	}
}
