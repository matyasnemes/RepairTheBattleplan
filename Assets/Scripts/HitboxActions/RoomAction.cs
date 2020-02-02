using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAction : MonoBehaviour
{
    public Node node;
    BoxCollider2D m_collider;
    private bool wentOut = false;
    private bool fightersHere = false;

    // Use this for initialization
    void Start () {
        m_collider = GetComponent<BoxCollider2D>();
        m_collider.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (fightersHere && node.wasEnemy && node.enemyCount==0 && !wentOut)
        {
            FighterController.goOut();
            wentOut = true;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        var fighter = other.GetComponent<Fighter>();
        if (fighter!=null)
        {
            wentOut = false;
            fightersHere = true;
            node.fighterCount++;
            fighter.currentNode = node;
            if (node.enemyCount == 0 && node.fighterCount == FighterController.fighters.Count)
            {
                fighter.currentNode.visited = true;
                FighterController.goOut();
                wentOut = true;
            }
            else if(node.enemyCount!=0)
            {
                node.fight(fighter);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var fighter = other.GetComponent<Fighter>();
        node.fighterCount--;
        if (fighter != null && node.fighterCount==0)
        {
            foreach(var f in FighterController.fighters)
                f.currentNode = null;
            fightersHere = false;
        }

    }
}
