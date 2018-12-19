using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGameTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        if (player)
        {
            player.WinGame();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
