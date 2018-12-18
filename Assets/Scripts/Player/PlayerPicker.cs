using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicker : MonoBehaviour {

    [SerializeField] private float m_maxPickAngle = 30.0f;
  
    private List<Interactable> m_currentInteractions;
    private Interactable m_lastClosest;

	// Use this for initialization
	void Start () {
        m_currentInteractions = new List<Interactable>();
        m_lastClosest = null;
    }

    public void AddInteractable(Interactable interactable)
    {
        m_currentInteractions.Add(interactable);
    }
	
    public void DeleteInteractable(Interactable interactable)
    {
        m_currentInteractions.Remove(interactable);
    }

    private float GetInteractionCost(Interactable current)
    {
        if (current.IsPicked())
        {
            return 0.0f;
        }

        Vector3 interactorDirection = current.gameObject.transform.position - transform.position;
        interactorDirection[1] = 0.0f;
        float angle = Vector3.Angle(interactorDirection, transform.forward);

        if (angle > m_maxPickAngle)
        {
            return 0.0f;
        }

        return 1 / (interactorDirection.magnitude * angle);
    }

    private Interactable FindClosestInteractable()
    {
        DeleteAllEmpty();
        if (m_currentInteractions.Count == 0)
        {
            return null;
        }

        Interactable bestVariant = m_currentInteractions[0];
        float variantCost = GetInteractionCost(m_currentInteractions[0]);
        for (int i = 1; i < m_currentInteractions.Count; ++i)
        {
            float currentCost = GetInteractionCost(m_currentInteractions[i]);
            if (currentCost > variantCost)
            {
                variantCost = currentCost;
                bestVariant = m_currentInteractions[i];
            }
        }

        return variantCost > 0.0f ? bestVariant : null;
    }

    private void DeleteAllEmpty()
    {
        m_currentInteractions.RemoveAll((Interactable i) => { return i == null || i.gameObject == null; });
    }

    private void HighLightInteractable()
    {
        Interactable inter = FindClosestInteractable();

        if (inter == m_lastClosest)
        {
            return;
        }

        if (m_lastClosest != null && m_lastClosest.gameObject != null)
        {
            m_lastClosest.SetOutlineEnabled(false);
        }
     
        m_lastClosest = inter;

        if (inter != null)
        {
            m_lastClosest.SetOutlineEnabled(true);
        }
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F))
        {
            Interactable interactable = FindClosestInteractable();

            if (interactable)
            {
                interactable.StartInteractionWithPlayer(gameObject);
            }
        }

        HighLightInteractable();
	}
}
