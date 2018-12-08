using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour {

    [SerializeField] private LightManager m_lightManager;
    [SerializeField] private GameObject m_playerLightCenter;
    [SerializeField] private GameObject m_playerLookCenter;
    [SerializeField] private float m_lightUpdateTime = 0.2f;

    private PlayerSkills m_playerSkills;
    private float m_currentLightIntensity;
    private float m_lastUpdateTime;
	// Use this for initialization
	void Start ()
    {
		if (!m_lightManager)
        {
            m_lightManager = GetComponent<LightManager>();
        }
        if (!m_playerLightCenter)
        {
            m_playerLightCenter = gameObject;
        }

        m_lastUpdateTime = -1.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Time.time > m_lastUpdateTime)
        {
            m_lastUpdateTime = Time.time + m_lightUpdateTime;
            m_currentLightIntensity = m_lightManager.CalculateLightIntensity(m_playerLightCenter);

            if (!m_playerSkills)
            {
                return;
            }

            Skill flashHider = m_playerSkills.GetSkill(Skill.SkillType.LightHider);
            if (flashHider)
            {
                m_currentLightIntensity *= flashHider.GetEffect();
            }

            //Debug.Log("Current light intensity: " + m_currentLightIntensity);
        }
	}

    public Vector3 GetPlayerLookCenter()
    {
        return m_playerLookCenter ? m_playerLookCenter.transform.position : Vector3.zero;
    }

    public float GetCurrentIntensity()
    {
        return m_currentLightIntensity;
    }
}
