using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAction : BaseCollidable
{
    public Node node; 

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        var fighter = other.GetComponent(typeof(Fighter));
        if (fighter!=null)
        {
            ((Fighter)fighter).currentNode = node;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var fighter = other.GetComponent(typeof(Fighter));
        if (fighter != null)
        {
            ((Fighter)fighter).currentNode = null;
        }
    }
}
