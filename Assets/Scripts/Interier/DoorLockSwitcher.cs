using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockSwitcher : Switcher {

    [SerializeField] private DoorOpener m_door;

    protected override void Switched()
    {
        m_door.SetDoorLocked(m_switchValue);
    }

    // Use this for initialization
    protected new void Start () {
		if (!m_door)
        {
            m_door = GetComponent<DoorOpener>();
        }
        base.Start();
	}

}
