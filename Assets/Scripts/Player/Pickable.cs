using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class Pickable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag != "Player")
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player)
            {
                Debug.Log("Pick item");
                player.PickUp(this);
            }

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
