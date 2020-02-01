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
    public Image tooltipImage;
    public GameplayController.PlayerAction playerAction;

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
        GameplayController.setCurrentAction(playerAction);
    }

}
