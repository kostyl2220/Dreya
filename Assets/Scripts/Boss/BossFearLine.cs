using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFearLine : MonoBehaviour {

    [SerializeField] private float m_damage = 20.0f;
    [SerializeField] private float m_lifetime = 5.0f;

    private float m_currentDamage;
    private float m_timeLeft;
    private float m_speed;

    public void SetSpeed(float speed)
    {
        m_speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != GameDefs.PLAYER_LAYER)
        {
            return;
        }

        Fearable fearable = other.gameObject.GetComponent<Fearable>();
        if (fearable)
        {
            fearable.ChangeFear(m_damage);
        }
    }

    // Use this for initialization
    void Start () {
        m_timeLeft = m_lifetime;
    }
	
	// Update is called once per frame
	void Update () {
        float alpha = Mathf.Min(Time.deltaTime / m_timeLeft, 1.0f);
        m_currentDamage = Mathf.Lerp(m_currentDamage, 0.0f, alpha);
        m_timeLeft -= Time.deltaTime;

        transform.position += transform.forward * m_speed * Time.deltaTime;

        if (m_timeLeft < 0.0f)
        {
            Destroy(gameObject);
        }
	}
}
