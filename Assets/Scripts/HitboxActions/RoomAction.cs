using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAction : MonoBehaviour
{
    public Node node;
    BoxCollider2D m_collider;

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
            fighter.currentNode = node;
            fighter.currentNode.visited = true;
            //tesztre, kesobb ne a szoba dobja ki a fightereket
            fighter.goOut();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var fighter = other.GetComponent<Fighter>();
        if (fighter != null)
        {
            fighter.currentNode = null;
        }
    }
}
