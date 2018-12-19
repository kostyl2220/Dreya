using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAttackAdapter : MonoBehaviour {

    [SerializeField] private AttackState m_attack;

    public void OnAttackEnded()
    {
        m_attack.OnAttackEnded();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
