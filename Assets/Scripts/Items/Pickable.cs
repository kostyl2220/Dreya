using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public abstract class Pickable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    protected abstract void AddItemToPlayer(GameObject player);

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag != "Player")
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Pick item");
            AddItemToPlayer(collision.gameObject);

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
