using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour {
    [SerializeField] protected bool m_switchValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool SwitchOn()
    {
        if (m_switchValue)
        {
            return false;
        }
        Debug.Log("SwitchOn");

        m_switchValue = true;

        return true;
    }

    public bool SwitchOff()
    {
        if (!m_switchValue)
        {
            return false;
        }
        Debug.Log("SwitchOff");

        m_switchValue = false;

        return true;
    }
}
