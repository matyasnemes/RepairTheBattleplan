using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayButtonController : MonoBehaviour,
                                    IPointerClickHandler
{
    public GameObject menuGUI;
    public GameObject gameplayGUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Random.seed = ???

        // Generate map

        // Switch GUI items
        menuGUI.SetActive(false);
        gameplayGUI.SetActive(true);
    }
}
