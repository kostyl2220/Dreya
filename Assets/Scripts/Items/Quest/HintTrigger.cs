using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Collider))]
public class HintTrigger : MonoBehaviour {

    [SerializeField] private DialogElement m_dialog;

    private DialogManager m_manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != GameDefs.PLAYER_TAG)
        {
            return;
        }

        Player player = other.gameObject.GetComponent<Player>();
        if (!player)
        {
            return;
        }

        if (!player.IsInvoking() && m_manager && m_dialog)
        {
            m_manager.SetDialog(m_dialog);
        }
    }

    // Use this for initialization
    void Start () {
        m_manager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
