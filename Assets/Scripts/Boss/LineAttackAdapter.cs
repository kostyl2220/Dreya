using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAttackAdapter : MonoBehaviour {

    [SerializeField] private BossLineAttackState m_lineAttackState;
    [SerializeField] private BossCreateCopiesState m_createCopies;

    //Fast hack

    public void OnLineAttackAnimEnded()
    {
        m_lineAttackState.OnAnimEnd();
    }

    public void OnCreateCopiesAnimEnded()
    {
        m_createCopies.OnAnimEnded();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
