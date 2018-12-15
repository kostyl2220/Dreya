using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossScene : MonoBehaviour {

    [SerializeField] private List<Switcher> m_swichers;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            return;
        }

        m_swichers.ForEach((s) => s.Switch());

        Collider coll = GetComponent<Collider>();
        if (coll)
        {
            coll.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
