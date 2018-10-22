using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : Switcher {

    [SerializeField] private float m_transparency = 0.2f;

	// Use this for initialization
	void Start () {
		if (!m_switchValue)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, m_transparency);
        }
    }

    new void SwitchOn() 
    {
        if (!base.SwitchOn())
        {
            return;
        }
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    new void SwitchOff()
    {
        if (!base.SwitchOff())
        {
            return;
        }
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, m_transparency);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
