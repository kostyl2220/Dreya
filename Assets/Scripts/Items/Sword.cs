using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Dress
{
    [SerializeField] private float m_cooldown = 1.0f;
    [SerializeField] private float m_damage = 20.0f;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Animation m_animation;

    [SerializeField] private string m_simpleAttack = "SwordAttack";

    private float m_lastHitCooldown;
    private bool m_isInHit;
    private SimpleCharacterControl m_scc;

	// Use this for initialization
	void Start () {
        m_lastHitCooldown = 0;
        m_scc = gameObject.GetComponent<SimpleCharacterControl>();

        if (!m_animator)
        {
            m_animator = gameObject.GetComponent<Animator>();
        }
        if (!m_animation)
        {
            m_animation = GetComponent<Animation>();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_isInHit)
        {
            return;
        }

        DamageReceiveComponent drc = collision.gameObject.GetComponent<DamageReceiveComponent>();
        if (drc)
        {
            drc.GetDamage(m_damage);
        }

    }

    void TryHit()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            return;
        }

        if (Time.time < m_lastHitCooldown)
        {
            return;
        }

        m_lastHitCooldown = Time.time + m_cooldown;
        m_isInHit = true;

        //play hit anim
        m_animator.Play(m_simpleAttack);
        m_animation.Play(m_simpleAttack);
    }

	// Update is called once per frame
	void Update () {
        TryHit();
    }
}
