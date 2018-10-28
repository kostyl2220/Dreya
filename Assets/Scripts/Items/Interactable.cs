using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour {

    [SerializeField] protected bool m_pickable = false;

    protected abstract void InteractWithPlayer(GameObject player);

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag != "Player")
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Pick item");
            InteractWithPlayer(collision.gameObject);

            if (ShouldDestroy())
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual bool ShouldDestroy()
    {
        return m_pickable;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
