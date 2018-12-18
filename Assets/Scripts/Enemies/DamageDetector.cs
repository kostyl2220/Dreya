using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class DamageDetector : MonoBehaviour {

    public delegate void OnDetectHit(GameObject gameObject);
    public event OnDetectHit OnDetect;

    private bool m_performingAttack;

    public void SetPerformingAttack(bool pa)
    {
        m_performingAttack = pa;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != GameDefs.PLAYER_TAG)
        {
            return;
        }

        if (!m_performingAttack)
        {
            return;
        }

        m_performingAttack = false;
        if (OnDetect != null)
        {
            OnDetect.Invoke(other.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        m_performingAttack = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
