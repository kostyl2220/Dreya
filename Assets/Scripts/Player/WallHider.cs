using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHider : MonoBehaviour {

    [SerializeField] private float m_distance = 2.0f;
    [SerializeField] private Vector3 m_offset = new Vector3(0.0f, 0.5f, 0.0f);

    private WallSwitch m_currentWallSwitch;
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Ray ray = new Ray(gameObject.transform.position + m_offset, -Vector3.forward);

        if (Physics.Raycast(ray, out hit, m_distance, LayerMask.NameToLayer("Wall")))
        {
            WallSwitch sw = hit.collider.gameObject.GetComponent<WallSwitch>();
            if (sw)
            {
                if (m_currentWallSwitch == sw)
                {
                    return;
                }

                m_currentWallSwitch.SwitchOn();
                m_currentWallSwitch = sw;
                sw.SwitchOff();
            }
        }
    }
}
