using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerZone : MonoBehaviour {
    [SerializeField] private List<GameObject> m_objectsToExpect;

    private List<GameObject> m_objectsInside;
    // Use this for initialization
    void Start () {
        m_objectsInside = new List<GameObject>(m_objectsToExpect.Count);
    }

    public bool AllInside()
    {
        return m_objectsInside.Count == m_objectsToExpect.Count;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (m_objectsToExpect.Contains(collider.gameObject) && !m_objectsInside.Contains(collider.gameObject))
        {
            m_objectsInside.Add(collider.gameObject);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (m_objectsToExpect.Contains(collider.gameObject) && m_objectsInside.Contains(collider.gameObject))
        {
            m_objectsInside.Remove(collider.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
