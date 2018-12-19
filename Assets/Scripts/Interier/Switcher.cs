using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switcher : MonoBehaviour {
    [SerializeField] protected bool m_switchValue;
    [SerializeField] protected bool m_oneTimeUse = false;

    protected bool m_switched = false;
    protected abstract void Switched();

    private void InnerShitched()
    {
        if (m_oneTimeUse && m_switched)
        {
            return;
        }

        Switched();
        m_switched = true;
    }

    protected void Start()
    {
        Switched();
    }

    public void Switch()
    {
        m_switchValue = !m_switchValue;
        InnerShitched();
    }

    public void SwitchOn()
    {
        ForceSwitch(true);
    }

    public bool ForceSwitch(bool value)
    {
        if (m_switchValue == value)
        {
            return false;
        }      
        m_switchValue = value;
        InnerShitched();
        return true;
    }

    public void SwitchOff()
    {
        ForceSwitch(false);
    }
}
