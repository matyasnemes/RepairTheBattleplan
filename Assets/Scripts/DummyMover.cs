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
        Node Node4 = new Node();
        Node Node5 = new Node();

        doorData Door1 = new doorData(new Vector2(0.45f, 0.183f), new Vector3(1,0,0));
        doorData Door2 = new doorData(new Vector2(1.227f, -0.796f), new Vector3(0,1,0));
        doorData Door3 = new doorData(new Vector2(1.802f, -1.39f), new Vector3(1,0,0));
        doorData Door4 = new doorData(new Vector2(3.245f, -0.126f), new Vector3(-1,0,0));
        doorData Door5 = new doorData(new Vector2(4.14f, -0.126f), new Vector3(1,0,0));
        doorData Door6 = new doorData(new Vector2(3.77f, -0.58f), new Vector3(0,-1,0));
        doorData Door7 = new doorData(new Vector2(3.77f, -1.243f), new Vector3(0,1,0));
        doorData Door8 = new doorData(new Vector2(4.875f, -0.135f), new Vector3(-1,0,0));

        Node1.neighbours.Add(Node2, Door1);

        Node2.neighbours.Add(Node1, Door2);
        Node2.neighbours.Add(Node3, Door3);

        Node3.neighbours.Add(Node2, Door4);
        Node3.neighbours.Add(Node4, Door6);
        Node3.neighbours.Add(Node5, Door5);

        Node4.neighbours.Add(Node3, Door7);

        Node5.neighbours.Add(Node3, Door8);

        nodes.Add(Node1);
        nodes.Add(Node2);
        nodes.Add(Node3);
        nodes.Add(Node4);
        nodes.Add(Node5);

       // Node1.fighterCount = 0;

        var room1 = GameObject.Find("Room1");
        var act = room1.GetComponent<RoomAction>();
        act.node = Node1;

        var room2 = GameObject.Find("Room2");
        act = room2.GetComponent<RoomAction>();
        act.node = Node2;

        var room3 = GameObject.Find("Room3");
        act = room3.GetComponent<RoomAction>();
        act.node = Node3;

        var room4 = GameObject.Find("Room4");
        act = room4.GetComponent<RoomAction>();
        act.node = Node4;

        var room5 = GameObject.Find("Room5");
        act = room5.GetComponent<RoomAction>();
        act.node = Node5;

        var f = (GameObject.Find("K")).GetComponent<Fighter>();
        var f1 = (GameObject.Find("K1")).GetComponent<Fighter>();
        f.currentNode = Node1;
        f.currentNode = Node1;
        FighterController.fighters.Add(f);
        FighterController.fighters.Add(f1);
        FighterController.goOut();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
