using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollower : MonoBehaviour {

    [SerializeField] private Transform m_target;

    public void SetTarget(Transform target)
    {
        m_target = target;
    }

	// Use this for initialization
	void Start () {
		
	}
 
    // Update is called once per frame
    void Update () {
        Vector3 wantedPos = Camera.main.WorldToScreenPoint(m_target.position);
        transform.position = wantedPos;
    }
}
