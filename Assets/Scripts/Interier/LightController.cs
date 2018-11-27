using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    [SerializeField] private float m_minRange = 4.0f;
    [SerializeField] private float m_maxRange = 8.0f;
    [SerializeField] private float m_minIntensity = 1.0f;
    [SerializeField] private float m_maxIntensity = 3.0f;
    [SerializeField] private float m_scroolSensivity = 0.05f;
    [SerializeField] private Light m_lighter;

    private float m_currentValue;

	void Start ()
    {
        m_currentValue = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            float currentChange = Input.GetAxis("Mouse ScrollWheel") * m_scroolSensivity;
            m_currentValue = Mathf.Clamp(m_currentValue + currentChange, 0, 1);
        }

        m_lighter.range = Mathf.Lerp(m_minRange, m_maxRange, m_currentValue);
        m_lighter.intensity = Mathf.Lerp(m_minIntensity, m_maxIntensity, m_currentValue);
	}
}
