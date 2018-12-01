using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour {

    protected abstract void InteractWithPlayer(GameObject player);

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag != GameDefs.PLAYER_TAG)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Pick item");
            InteractWithPlayer(collision.gameObject);

            if (ShouldDestroyAfterInteraction())
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual bool ShouldDestroyAfterInteraction()
    {
        return false;
    }
}
