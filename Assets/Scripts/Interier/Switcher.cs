using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switcher : MonoBehaviour {
    [SerializeField] protected bool m_switchValue;

    protected abstract void Switched();

    void Start()
    {
        Switched();
    }

    public void Switch()
    {
        m_switchValue = !m_switchValue;
        Switched();
    }

    public bool SwitchOn()
    {
        Debug.Log("SwitchOn");
        return ForceSwitch(true);
    }

    public bool ForceSwitch(bool value)
    {
        if (m_switchValue == value)
        {
            return false;
        }      
        m_switchValue = value;
        Switched();
        return true;
    }

    public bool SwitchOff()
    {
        Debug.Log("SwitchOff");
        return ForceSwitch(false);
    }
}
