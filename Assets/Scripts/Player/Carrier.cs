using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour {
    private static string CARRY_TAG = "Carriable";
    private static float EPSILON = 0.1f;

    [SerializeField] private Transform m_point1;
    [SerializeField] private Transform m_point2;
    [SerializeField] private float m_castRadius = 0.2f;
    [SerializeField] private float m_maxCarryDistance = 0.5f;

    private float m_distance;
    private GameObject m_carriableItem;

    private SimpleCharacterControl m_scc;
	// Use this for initialization
	void Start () {
        m_carriableItem = null;
        m_scc = gameObject.GetComponent<SimpleCharacterControl>();
    }

    public bool IsCarrying()
    {
        return m_carriableItem != null;
    }

    void CheckDistance()
    {
        if (!m_carriableItem)
        {
            return;
        }

        if (GetDistanceToCarriable() > m_distance + EPSILON)
        {
            ResetCariable();
            return;
        }

        RaycastHit hit;
        if (Physics.Linecast(transform.position, m_carriableItem.transform.position, out hit, GameDefs.PLAYER_LAYER))
        {
            ResetCariable();
            return; 
        }

        if (!(m_scc.IsGrounded() && m_scc.IsWalking()))
        {
            ResetCariable();
            return;
        }
    }

    private void ResetCariable()
    {
        m_carriableItem.transform.SetParent(null);
        m_carriableItem = null;
    }

	// Update is called once per frame
	void Update () {
        CheckDistance();
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (m_carriableItem)
            {
                ResetCariable();
            }
            else
            {
                m_carriableItem = CanCarrySomething();
                if (m_carriableItem)
                {
                    m_carriableItem.transform.SetParent(transform);
                    m_distance = GetDistanceToCarriable();
                }
            }
        }
	}

    private float GetDistanceToCarriable()
    {
        return (m_carriableItem.transform.position - transform.position).magnitude;
    }

    public GameObject CanCarrySomething()
    {
        RaycastHit hit;
        if (Physics.CapsuleCast(m_point1.position, m_point2.position, m_castRadius, transform.forward, out hit, m_maxCarryDistance))
        {
            if (hit.transform.tag == CARRY_TAG)
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }
}
