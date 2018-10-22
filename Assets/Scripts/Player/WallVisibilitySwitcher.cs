using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SwitchDirection
{
    SwitchDirection_UP,
    SwitchDirection_RIGHT
}


[RequireComponent(typeof(BoxCollider))]
public class WallVisibilitySwitcher : MonoBehaviour {

    [SerializeField] private List<Switcher> m_startSwitchers;
    [SerializeField] private List<Switcher> m_endSwitchers;
    [SerializeField] private SwitchDirection m_direction;
    [SerializeField] private Vector3 m_upDirection;

    private Vector3 m_rightDirection;

	// Use this for initialization
	void Start ()
    {
        m_rightDirection = Vector3.Cross(m_upDirection, Vector3.up);
	}

    void OnTriggerExit(Collider other)
    {
        Debug.Log("ExitCollision");
        if (other.transform.tag != "Player")
        {
            return;
        }

        Vector3 walkDirection = other.transform.position - transform.position;

        bool straightDirection = ((m_direction == SwitchDirection.SwitchDirection_UP
            && Vector3.Angle(walkDirection, m_upDirection) < 90.0f)
            || (m_direction == SwitchDirection.SwitchDirection_RIGHT
            && Vector3.Angle(walkDirection, m_rightDirection) < 90.0f));

        List<Switcher> toTurnOn = straightDirection ? m_endSwitchers : m_startSwitchers;
        List<Switcher> toTurnOf = straightDirection ? m_startSwitchers : m_endSwitchers;

        foreach (var sw in toTurnOn)
        {
            sw.SwitchOn();
        }
        foreach (var sw in toTurnOf)
        {
            sw.SwitchOff();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
