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
    [SerializeField] private float m_rangeKoef = 0.4f;
    [SerializeField] private float m_lookAngle;
    [SerializeField] private SimpleBrainState m_startupState;

    [SerializeField] public bool m_enableDebug;

    private SimpleBrainState m_currentState;
    public NavMeshAgent m_agent { get; private set; }

	void Start ()
    {
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
        if (m_enableDebug)
        {
            Debug.Log(m_currentState.GetStateName());
        }
    }

    public bool SeePlayer()
    {
        PlayerLightController plc = m_player.GetComponent<PlayerLightController>();
        if (Vector3.Distance(m_player.transform.position, transform.position) > m_rangeKoef * plc.GetCurrentIntensity())
        {
            return false;
        }

        if (Vector3.Angle(m_player.transform.position - transform.position, transform.forward) > m_lookAngle)
        {
            return false;
        }

        //TODO check on dirrect looking
        //Debug.Log("I see you!");

        return true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        PlayerLightController plc = m_player.GetComponent<PlayerLightController>();
        Gizmos.DrawWireSphere(transform.position, m_rangeKoef * plc.GetCurrentIntensity());
    }
}
