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

    public override bool SwitchOn() 
    {
        if (!base.SwitchOn())
        {
            return false;
        }
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        return true;
    }

    public override bool SwitchOff()
    {
        if (!base.SwitchOff())
        {
            return false;
        }
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, m_transparency);
        return true;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
