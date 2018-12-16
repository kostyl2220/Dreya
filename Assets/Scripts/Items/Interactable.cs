using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : ItemOutline {

    protected abstract void InteractWithPlayer(GameObject player);
    protected bool m_isPicked;

    protected void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag != GameDefs.PLAYER_TAG)
        {
            return;
        }

        PlayerPicker pp = collision.GetComponent<PlayerPicker>();
        if (pp)
        {
            pp.AddInteractable(this);
        }
    }

    public bool IsPicked()
    {
        return m_isPicked;
    }

    protected void OnTriggerExit(Collider collision)
    {
        if (collision.transform.tag != GameDefs.PLAYER_TAG)
        {
            return;
        }

        PlayerPicker pp = collision.GetComponent<PlayerPicker>();
        if (pp)
        {
            pp.DeleteInteractable(this);
        }
    }

    protected void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag != GameDefs.PLAYER_TAG)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartInteractionWithPlayer(collision.gameObject);
        }
    }

    public void StartInteractionWithPlayer(GameObject player)
    {
        InteractWithPlayer(player);

        if (ShouldDestroyAfterInteraction())
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        SetOutlineEnabled(false);
    }

    protected virtual bool ShouldDestroyAfterInteraction()
    {
        return false;
    }
}
