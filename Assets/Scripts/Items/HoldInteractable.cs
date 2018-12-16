using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class HoldInteractable : ItemOutline
{

    [SerializeField] private float m_processTime = 2.0f;
    private ProgressCircle m_playerProgressCircle;

    protected abstract void InteractWithPlayer(GameObject player);

    private void ProcessInteraction(GameObject obj)
    {
        InteractWithPlayer(obj);

        if (ShouldDestroyAfterInteraction())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag != GameDefs.PLAYER_TAG)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            m_playerProgressCircle = collision.gameObject.GetComponent<ProgressCircle>();

            if (m_playerProgressCircle)
            {
                m_playerProgressCircle.SetProgress(m_processTime, () => { ProcessInteraction(collision.gameObject); });
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            if (m_playerProgressCircle)
            {
                m_playerProgressCircle.ResetProgress();
            }
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
