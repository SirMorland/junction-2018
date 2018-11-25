using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour {

    [SerializeField] private GameObject start;
    [SerializeField] private GameObject controls;
    [SerializeField] private EventSystem eventSys;

    [SerializeField] private List<GameObject> controlScreen;
    [SerializeField] private List<GameObject> controlTexts;
    private bool showingControls = false;

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

            if (showingControls)
            {
                foreach (GameObject c in controlScreen)
                {
                    c.GetComponent<Image>().enabled = false;
                }
                foreach (GameObject c in controlTexts)
                {
                    c.GetComponent<Text>().enabled = false;
                }
                showingControls = false;
            }
            else if (selected == start)
            {
                SceneManager.LoadScene("CardSelection");
            }
            else if (selected == controls)
            {
                foreach (GameObject c in controlScreen)
                {
                    c.GetComponent<Image>().enabled = true;
                }
                foreach (GameObject c in controlTexts)
                {
                    c.GetComponent<Text>().enabled = true;
                }
                showingControls = true;
            }
        }

        if (direction < 0 && !showingControls) eventSys.SetSelectedGameObject(start);
        else if (direction > 0 && !showingControls) eventSys.SetSelectedGameObject(controls);
    }
}
