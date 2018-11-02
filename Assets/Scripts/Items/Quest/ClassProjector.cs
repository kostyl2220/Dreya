using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassProjector : Switcher {
    [SerializeField] private Light m_light;

    protected override void Switched()
    {
        // Enable projector;
        if (m_light)
        {
            m_light.gameObject.SetActive(m_switchValue);
        }
    }
}
