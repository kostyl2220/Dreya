using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeEndOfGameSwitcher : EnableSwitcher {

	// Use this for initialization
	protected override void Switched()
    {
        Time.timeScale = m_switchValue ? 0.0f : 1.0f;
        base.Switched();
    }

    private void Start()
    {
        
    }
}
