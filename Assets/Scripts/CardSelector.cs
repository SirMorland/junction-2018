using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelector : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    [SerializeField] private GameObject selectionMarker;

    public void OnSelect(BaseEventData eventData)
    {
        Image img = selectionMarker.GetComponent("Image") as Image;
        img.enabled = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Image img = selectionMarker.GetComponent("Image") as Image;
        img.enabled = false;
    }
}
