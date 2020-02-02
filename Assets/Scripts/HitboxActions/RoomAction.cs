using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAction : MonoBehaviour
{
    public Node node;
    BoxCollider2D m_collider;
    public int fighterCount;

    // Use this for initialization
    void Start () {
        m_collider = GetComponent<BoxCollider2D>();
        m_collider.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        var fighter = other.GetComponent<Fighter>();
        if (fighter!=null)
        {
            node.fighterCount++;
            if (node.enemyCount == 0 && node.fighterCount == 5)
            {
                fighter.currentNode = node;
                fighter.currentNode.visited = true;
                fighter.goOut();
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var fighter = other.GetComponent<Fighter>();
        node.fighterCount--;
        if (fighter != null && node.fighterCount==0)
        {
            fighter.currentNode = null;
        }

    }
}
