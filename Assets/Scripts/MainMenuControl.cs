using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControl : MonoBehaviour {

    [SerializeField] private GameObject start;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject stats;
    [SerializeField] private GameObject exit;
    [SerializeField] private EventSystem eventSys;
    [SerializeField] private GameObject selectedObject;

    [SerializeField] private List<GameObject> controlScreen;
    [SerializeField] private List<GameObject> controlTexts;
    private bool showingControls = false;

    [SerializeField] private List<GameObject> statScreen;
    [SerializeField] private List<GameObject> statTexts;
    private bool showingStats = false;

    private float direction = 0f;

    // Use this for initialization
    void Start () {
        eventSys.SetSelectedGameObject(start);
    }
	
	// Update is called once per frame
	void Update () {

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
            else if (showingStats)
            {
                foreach (GameObject c in statScreen)
                {
                    c.GetComponent<Image>().enabled = false;
                }
                foreach (GameObject c in statTexts)
                {
                    c.GetComponent<Text>().enabled = false;
                }
                showingStats = false;
            }
            else if (selected == start)
            {
                GameObject.Find("StatManager").GetComponent<StatSaver>().Load();
                SceneManager.LoadScene("CardSelection");
            }
            else if (selected == exit)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
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
            else if (selected == stats)
            {
                StatSaver stats = GameObject.Find("StatManager").GetComponent<StatSaver>();
                stats.Load();
                GameObject.Find("Pelit").GetComponent<Text>().text = stats.statData.gamesPlayed.ToString();
                GameObject.Find("P1W").GetComponent<Text>().text = stats.statData.p1Wins.ToString();
                GameObject.Find("P2W").GetComponent<Text>().text = stats.statData.p2Wins.ToString();

                foreach (GameObject c in statScreen)
                {
                    c.GetComponent<Image>().enabled = true;
                }
                foreach (GameObject c in statTexts)
                {
                    c.GetComponent<Text>().enabled = true;
                }
                showingStats = true;
            }
        }

        /*
        if (direction == -1 && !showingControls && mId > 0)
        {
            mId--;
            eventSys.SetSelectedGameObject(menu[mId]);
        }
        else if (direction == 1 && !showingControls && mId < menu.Length-1)
        {
            mId++;
            eventSys.SetSelectedGameObject(menu[mId]);
        } */
    }
}
