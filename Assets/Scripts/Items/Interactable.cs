using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : ItemOutline {

    [SerializeField] private int m_taskId = -1;
    private TaskManager m_taskManager;

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

    protected virtual bool CanInteractWithPlayer()
    {
        if (m_taskId == -1)
        {
            return true;
        }

        if (!m_taskManager)
        {
            return false;
        }

        return m_taskManager.IsTaskActive(m_taskId);
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
        if (!CanInteractWithPlayer())
        {
            return;
        }

        InteractWithPlayer(player);

        if (ShouldDestroyAfterInteraction())
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        m_taskManager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        SetOutlineEnabled(false);
    }

    protected virtual bool ShouldDestroyAfterInteraction()
    {
        return false;
    }
}
