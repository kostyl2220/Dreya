using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    [SerializeField] private float m_time;

    private float m_destroyTime;

	// Use this for initialization
	void Start () {
        m_destroyTime = Time.time + m_time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > m_destroyTime)
        {
            Destroy(gameObject);
        }
	}
}
