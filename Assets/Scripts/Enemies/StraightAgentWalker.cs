﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StraightAgentWalker : MovementAgent {

    static float EPSILON = 0.0001f;

    [SerializeField] private float m_speed = 2.0f;
    [SerializeField] private float m_rotationSpeed = 120.0f;

    private Vector3 m_destination;

    public override float GetRemainingDistance()
    {
        return Vector3.Distance(m_destination, transform.position);
    }

    public override void ResetPath()
    {
        m_destination = transform.position;
    }

    public override void SetDestination(Vector3 destination)
    {
        m_destination = destination;

      
    }

    public override void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        Quaternion endAngle = Quaternion.LookRotation(m_destination - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, endAngle, m_rotationSpeed * Time.deltaTime);

        if (GetRemainingDistance() < EPSILON)
        {
            return;
        }

        gameObject.transform.position += gameObject.transform.forward * m_speed * Time.deltaTime;
	}
}