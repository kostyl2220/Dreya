using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class SimpleBrain : MonoBehaviour {

    [System.Serializable]
    public struct MinMaxRange
    {
        [SerializeField]
        public float min;
        [SerializeField]
        public float max;

        public MinMaxRange(float m, float M)
        {
            min = m;
            max = M;
        }

        public float GetInRange()
        {
            return Random.Range(min, max);
        }
    }

    [SerializeField] public GameObject m_player;
    [SerializeField] private float m_range;
    [SerializeField] private float m_lookAngle;
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

    public void SetState(SimpleBrainState state)
    {
        m_currentState = state;
        m_currentState.Setup();
    }

    public bool SeePlayer()
    {
        if (Vector3.Distance(m_player.transform.position, transform.position) > m_range)
        {
            return false;
        }

        if (Vector3.Angle(m_player.transform.position - transform.position, transform.forward) > m_lookAngle)
        {
            return false;
        }

        //TODO check on dirrect looking
        return true;
    }
}
