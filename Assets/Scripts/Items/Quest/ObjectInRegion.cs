using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInRegion : Interactable {

    [SerializeField] private List<Switcher> m_switchers;
    [SerializeField] private TriggerZone m_acceptableZone;

    protected override void InteractWithPlayer(GameObject player)
    {
        if (m_acceptableZone.AllInside())
        {
            m_switchers.ForEach((Switcher s) => { s.Switch(); } );
        }
    }
}
