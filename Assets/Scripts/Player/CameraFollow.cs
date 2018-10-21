using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private GameObject m_objectToFollow;
    [SerializeField] private float m_lookDistance;
    [SerializeField] private float m_normalAngle;
    [SerializeField] private Vector3 m_lookDirection;
    [SerializeField] private Vector3 m_lookOffset;

    private Vector3 m_fixedLookDirection;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        //TODO_Start remove it to Start method after tuning
        Vector3 rightVector = Vector3.Cross(m_lookDirection, Vector3.up);
        m_fixedLookDirection = Quaternion.AngleAxis(-m_normalAngle, rightVector) * m_lookDirection;
        m_fixedLookDirection.Normalize();
        transform.rotation = Quaternion.LookRotation(m_fixedLookDirection);
        //TODO_End
        if (m_objectToFollow)
        {
            transform.position = m_objectToFollow.transform.position + m_lookOffset - m_fixedLookDirection * m_lookDistance;
        }
	}
}
