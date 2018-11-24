using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour {

    [SerializeField] private GameObject leftCard;
    [SerializeField] private GameObject rightCard;
    [SerializeField] private EventSystem eventSys;
    private float direction = 0f;

    public int player;

    // Use this for initialization
    void Start () {
        eventSys.SetSelectedGameObject(leftCard);
    }
	
	// Update is called once per frame
	void Update () {
        direction = Input.GetAxis("Horizontal-" + player);

        if (Input.GetButtonDown("SelectCard-" + player))
        {
            eventSys.currentSelectedGameObject.GetComponentInChildren<Text>().text = "Selected";
        }

        if (direction < 0) eventSys.SetSelectedGameObject(leftCard);
        else if (direction > 0) eventSys.SetSelectedGameObject(rightCard);

        
    }
}
