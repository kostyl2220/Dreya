using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour {

    [SerializeField] private LightManager m_lightManager;
    [SerializeField] private float m_lightUpdateTime = 0.2f;

    private float m_currentLightIntensity;
    private float m_lastUpdateTime;
	// Use this for initialization
	void Start ()
    {
		if (!m_lightManager)
        {
            m_lightManager = GetComponent<LightManager>();
        }
        m_lastUpdateTime = -1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Time.time > m_lastUpdateTime)
        {
            m_lastUpdateTime = Time.time + m_lightUpdateTime;
            m_currentLightIntensity = m_lightManager.CalculateLightIntensity(gameObject);
            Debug.Log("Current light intensity: " + m_currentLightIntensity);
        }
	}

    public float GetCurrentIntensity()
    {
        return m_currentLightIntensity;
    }
}
