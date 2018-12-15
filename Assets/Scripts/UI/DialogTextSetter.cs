using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogTextSetter : MonoBehaviour {

    [SerializeField] private Text m_text; 
    
    public void SetText(string text)
    {
        if (m_text)
        {
            m_text.text = text;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
