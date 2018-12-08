using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSwitcher : Switcher
{
    [SerializeField] private GameObject m_objectToEnable;

    private new void Start()
    {
        if (!m_objectToEnable)
        {
            m_objectToEnable = gameObject;
        }
        base.Start();
    }

    protected override void Switched()
    {
        m_objectToEnable.SetActive(m_switchValue);
    }
}
