using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour {
    [SerializeField] private GameObject m_HUD;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0.0f;
	}

    public void ResetFocus()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        if (m_HUD)
        {
            m_HUD.SetActive(true);
        }

        Time.timeScale = 1.0f;
    }

    public void ReloadGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
