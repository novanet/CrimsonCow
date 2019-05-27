using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTextColorChangeScript : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<Image>().color = new Color(255, 255, 255, 255);

    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<Image>().color = new Color(0, 140, 255, 255);

    }
}
