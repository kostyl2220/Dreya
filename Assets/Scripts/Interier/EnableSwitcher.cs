using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSwitcher : Switcher
{
    protected override void Switched()
    {
        gameObject.SetActive(m_switchValue);
    }
}
