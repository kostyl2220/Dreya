using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Pickable
{

    [SerializeField] private float m_decreaseFearValue;

    protected override bool PickedByPlayer(GameObject player)
    {
        var fearable = player.GetComponent<Fearable>();
        if (fearable)
        {
            fearable.ChangeFear(-m_decreaseFearValue);
        }

        return true;
    }
}
