using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

    private Node currentNode;
    public float speed;

    private Vector2 target;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    public void setNode(Node node)
    {
        currentNode = node;
    }

    public void goOut()
    {
        target = currentNode.findDoor();
    }
}
