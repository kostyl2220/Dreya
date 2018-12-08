using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLight : Skill {

    [SerializeField] private float m_cooldown = 10.0f;
    [SerializeField] private Light m_light;
    [SerializeField] private float m_maxIntensity = 5.0f;
    [SerializeField] private float m_timeToLive = 3.0f;

    private float m_currentIntensity;
    private float m_leftTime;
    private float m_nextUpdate;
    // Use this for initialization
    new void Start () {
        base.Start();
	}

    public virtual void Use()
    {
        if (m_leftTime > 0)
        {
            return;
        }

        if (Time.time < m_nextUpdate)
        {
            return;
        }

        m_nextUpdate = Time.time + m_cooldown;
        m_leftTime = m_timeToLive;
        m_currentIntensity = m_maxIntensity;
        m_light.gameObject.SetActive(true);
        m_light.intensity = m_maxIntensity;
    }

    // Update is called once per frame
    void Update () {
        if (!m_light)
        {
            return;
        }

	    if (m_leftTime < 0.0f)
        {
            return;
        }

        float alpha = Mathf.Min(Time.deltaTime / m_leftTime, 1.0f);
        m_currentIntensity = Mathf.Lerp(m_currentIntensity, 0.0f, alpha);
        m_light.intensity = m_currentIntensity;
        m_leftTime -= Time.deltaTime;

        if (m_leftTime < 0.0f)
        {
            m_light.gameObject.SetActive(false);
        }
	}
}
