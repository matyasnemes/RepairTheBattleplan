using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ActionButtonController :   MonoBehaviour, 
                                        IPointerEnterHandler,
                                        IPointerExitHandler,
                                        IPointerClickHandler
{
    public static int currentAction;
    public Image tooltipImage;

    public void Start()
    {
        tooltipImage.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipImage.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipImage.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Fuckyou more!");
    }

}
