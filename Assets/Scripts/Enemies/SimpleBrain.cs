using Assets.Scripts;
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
    [SerializeField] public bool m_enableDebug;
    public Vector3 m_lastPlayerMoveDirection { get; set; }
    [SerializeField] private MovementAgent m_agent;

    [SerializeField] private float m_rangeKoef = 0.4f;
    [SerializeField] private float m_agroRangeKoef = 1.8f;
    [SerializeField] private float m_lookAngle = 60.0f;
    [SerializeField] private float m_agroLookAngle = 120.0f;
    [SerializeField] private SimpleBrainState m_startupState;
    [SerializeField] private Transform m_lookCenter;

    private bool m_isAgressive;
    private SimpleBrainState m_currentState;

    public void SetAgressive(bool agresive)
    {
        if (m_isAgressive == agresive)
        {
            return;
        }

        m_isAgressive = agresive;
        Player player = m_player.GetComponent<Player>();
        if (player)
        {
            player.ChangeReveal(agresive);
        }
    }

    public void SetAgentSpeed(float speed)
    {
        m_agent.SetSpeed(speed);
    }

    public bool IsAgressive()
    {
        return m_isAgressive;
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

    public float GetAgentLookAngleDiff()
    {
        return m_agent.GetAgentLookAngleDiff();
    }

    public float GetAgentRemainingDistance()
    {
        return m_agent.GetRemainingDistance();
    }

    public void AgentForceStop()
    { 
        m_agent.ResetPath();
    }

    public void SetNewMovePosition(Vector3 newPos)
    {
        NavMeshHit hit;
        Vector3 finalPosition = transform.position;
        float radius = Vector3.Distance(transform.position, newPos);
        if (NavMesh.SamplePosition(newPos, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        m_agent.SetDestination(finalPosition);
    }

    public bool SeePlayer(bool checkRotation = true)
    {
        PlayerLightController plc = m_player.GetComponent<PlayerLightController>();
        float actualLookRange = (m_isAgressive ? m_agroRangeKoef : m_rangeKoef) * plc.GetCurrentIntensity();
        if (Vector3.Distance(m_player.transform.position, transform.position) > actualLookRange)
        {
            return false;
        }

        float actualLookRangeAngle = (m_isAgressive ? m_agroLookAngle : m_lookAngle);
        if (checkRotation && Vector3.Angle(m_player.transform.position - transform.position, transform.forward) > actualLookRangeAngle)
        {
            return false;
        }

        //check on dirrect looking
        RaycastHit hit;
        if (Physics.Linecast(m_lookCenter.position, plc.GetPlayerLookCenter(), out hit, GameDefs.PLAYER_LAYER | GameDefs.ENEMY_LAYER))
        {
            return false;
        }

        return true;
    }

    void Start ()
    {
        if (!m_agent)
        {
            m_agent = GetComponent<NavMeshMovementAgent>();
        }
        if (!m_player)
        {
            m_player = GameObject.Find("Player");
        }

        if (m_startupState)
        {
            SetState(m_startupState);
        }
    }

    // Update is called once per frame
    void Update () {
        if (m_currentState)
        {
            m_currentState.UpdateState();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        PlayerLightController plc = m_player.GetComponent<PlayerLightController>();
        Gizmos.DrawWireSphere(transform.position, m_rangeKoef * plc.GetCurrentIntensity());
    }
}
