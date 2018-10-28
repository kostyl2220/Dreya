using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Interactable
{

    [SerializeField] private float m_decreaseFearValue;

	// Use this for initialization
	void Start () {
        m_pickable = true;
	}

    protected override void InteractWithPlayer(GameObject player)
    {
        var fearable = player.GetComponent<Fearable>();
        if (fearable)
        {
            fearable.ChangeFear(-m_decreaseFearValue);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
