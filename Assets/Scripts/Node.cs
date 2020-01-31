using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public bool visited = false;
    private Dictionary<Node, Vector2> neighbours = new Dictionary<Node, Vector2>();

    public Vector2 findDoor()
    {
        foreach (var n in neighbours)
        {
            if (!n.Key.visited)
                return n.Value;
        }


    }
}
