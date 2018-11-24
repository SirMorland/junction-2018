using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuItem : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private string defaultText;

    public void OnSelect(BaseEventData eventData)
    {
        Text text = gameObject.GetComponent<Text>();
        text.text = "> " + defaultText;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Text text = gameObject.GetComponent<Text>();
        text.text = "  " + defaultText;
    }
}
