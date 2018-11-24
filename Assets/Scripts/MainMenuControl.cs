using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

    [SerializeField] private GameObject start;
    [SerializeField] private GameObject controls;
    [SerializeField] private EventSystem eventSys;

    private float direction = 0f;

    // Use this for initialization
    void Start () {
        eventSys.SetSelectedGameObject(start);
        
    }
	
	// Update is called once per frame
	void Update () {
        direction = Input.GetAxis("Vertical-1");

        if (Input.GetButtonDown("SelectCard-1"))
        {
            GameObject selected = eventSys.currentSelectedGameObject;

            if (selected == start) SceneManager.LoadScene("CardSelection");
            else if (selected == controls)
            {

            }
        }

        if (direction < 0) eventSys.SetSelectedGameObject(start);
        else if (direction > 0) eventSys.SetSelectedGameObject(controls);
    }
}
