using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Pickable {

    [SerializeField] private float m_decreaseFearValue;

	// Use this for initialization
	void Start () {
		
	}

    protected override void AddItemToPlayer(GameObject player)
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
