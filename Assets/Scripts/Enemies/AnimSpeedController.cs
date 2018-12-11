using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimSpeedController : MonoBehaviour {

    private static float EPSILON = 0.0001f;

    [SerializeField] private Animator m_animator;
    [SerializeField] private float m_calmMaxSpeed = 1.2f;
    [SerializeField] private float m_agressiveMaxSpeed = 2.0f;
    [SerializeField] private SimpleBrain m_brain;
    [SerializeField] private string m_speedName = "MoveSpeed";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_brain)
        {
            return;
        }

        if (!m_animator)
        {
            return;
        }

        bool stayOnPlace = m_brain.GetAgentRemainingDistance() < EPSILON;
        bool isAgressive = m_brain.IsAgressive();
        m_brain.SetAgentSpeed(isAgressive ? m_agressiveMaxSpeed : m_calmMaxSpeed);

        if (stayOnPlace)
        {
            m_animator.SetFloat(m_speedName, 0.0f);
        }
        else
        {
            m_animator.SetFloat(m_speedName, isAgressive ? 1.0f : 0.5f);
        }
        
    }
}
