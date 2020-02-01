using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMover : MonoBehaviour {

    List<Node> nodes = new List<Node>();

	// Use this for initialization
	void Start () {
        Node Node1 = new Node();
        Node Node2 = new Node();
        Node Node3 = new Node();
        Vector2 Door1 = new Vector2(0.513f, 0.016f);
        Vector2 Door2 = new Vector2(2.996f, -0.552f);
        Vector2 Door3 = new Vector2(4.189f, -0.552f);
        Vector2 Door4 = new Vector2(3.529f, -1.353f);

        Node1.neighbours.Add(Node2, Door1);
        Node2.neighbours.Add(Node1, Door2);
        Node2.neighbours.Add(Node3, Door4);
        Node3.neighbours.Add(Node2, Door4);

        nodes.Add(Node1);
        nodes.Add(Node2);
        nodes.Add(Node3);

        var room1 = GameObject.Find("Room1");
        var act = room1.GetComponent(typeof(RoomAction)) as RoomAction;
        act.node = Node1;

        var room2 = GameObject.Find("Room2");
        act = room2.GetComponent(typeof(RoomAction)) as RoomAction;
        act.node = Node2;

        var room3 = GameObject.Find("Room3");
        act = room3.GetComponent(typeof(RoomAction)) as RoomAction;
        act.node = Node3;

        var fighter = GameObject.Find("Knight");
        var f = fighter.GetComponent(typeof(Fighter)) as Fighter;
        f.currentNode = Node1;

        f.goOut();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
