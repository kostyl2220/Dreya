using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Dress
{
    [SerializeField] private float m_cooldown = 1.0f;


    private float m_lastHit;
    private bool m_isInHit;
    private SimpleCharacterControl m_scc;

	// Use this for initialization
	void Start () {
        m_lastHit = -m_cooldown;
        m_scc = gameObject.GetComponent<SimpleCharacterControl>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_isInHit)
        {
            return;
        }


    }

    void TryHit()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            return;
        }

        if (Time.time < m_lastHit + m_cooldown)
        {
            return;
        }

        m_lastHit = Time.time;
        m_isInHit = true;
        //hit
    }

	// Update is called once per frame
	void Update () {
        TryHit();
    }
}
