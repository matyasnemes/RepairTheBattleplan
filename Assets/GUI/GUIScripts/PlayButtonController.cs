using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayButtonController : MonoBehaviour,
                                    IPointerClickHandler
{
    public GameObject menuBar;
    public GameObject teamBar;
    public GameObject actionBar;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameplayController.setGameState(GameplayController.GameState.PLAY_STATE);

        // Random.seed = ???


        // Generate map

        // Switch GUI items
        menuBar.SetActive(false);
        teamBar.SetActive(true);
        actionBar.SetActive(true);
    }
}
