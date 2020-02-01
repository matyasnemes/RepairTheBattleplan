using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool visited = false;
    public Dictionary<Node, Vector2> neighbours = new Dictionary<Node, Vector2>();

    public Vector2 chooseDoor(Fighter fighter, bool routeFailed)
    {
        foreach (var n in neighbours)
        {
            if (!n.Key.visited)
            {
                if (routeFailed && n.Key == fighter.targetNode)
                    continue;
                fighter.targetNode = n.Key;
                return n.Value;
            }
        }

        var route = Graph.findPathToNew(this, fighter, routeFailed);
        var nextNode = route[0];
        fighter.targetNode = nextNode;
        route.RemoveAt(0);
        fighter.route = route;
        return neighbours[nextNode];
    }

    public Vector2 findDoor(Node toRoom)
    {
        return neighbours[toRoom];
    }
}