using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSwitch : Switcher {
    [SerializeField] private float m_transparency = 0.2f;

    protected override void Switched() 
    {
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f,  m_switchValue ? 1.0f : m_transparency);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
