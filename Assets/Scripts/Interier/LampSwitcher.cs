using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSwitcher : Interactable {

    [SerializeField] private float m_maxIntensity;
    [SerializeField] private Light m_lightSource;
    [SerializeField] private bool m_enabled;

    protected override void InteractWithPlayer(GameObject player)
    {
        m_enabled = !m_enabled;
        if (m_lightSource)
        {
            m_lightSource.intensity = (m_enabled ? m_maxIntensity : 0.0f);
        }
    }
}
