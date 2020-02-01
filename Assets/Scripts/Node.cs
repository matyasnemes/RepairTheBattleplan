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

        var route = findPathToNew(this, fighter, routeFailed);
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

    public static List<Node> findPathToNew(Node from, Fighter fighter, bool routeFailed)
    {
        List<Node> traverseOrder = new List<Node>();
        Queue<Node> toBeProcessed = new Queue<Node>();
        HashSet<Node> processed = new HashSet<Node>();
        toBeProcessed.Enqueue(from);
        processed.Add(from);

        while (toBeProcessed.Count > 0)
        {
            Node n = toBeProcessed.Dequeue();
            traverseOrder.Add(n);

            if (!n.visited)
            {
                break;
            }

            foreach (Node node in n.neighbours.Keys)
            {
                if (!processed.Contains(node))
                {
                    toBeProcessed.Enqueue(node);
                    processed.Add(node);
                }
            }
        }
        var path = new List<Node>();
        Node currentNode = traverseOrder[traverseOrder.Count - 1];
        path.Add(currentNode);

        while (currentNode != from)
        {
            findBaseNode(currentNode, traverseOrder, path);
            currentNode = path[path.Count - 1];
        }

        return path;
    }

    private static void findBaseNode(Node from, List<Node> traverseOrder, List<Node> path)
    {
        foreach (Node node in traverseOrder)
        {
            foreach (Node neighbour in from.neighbours.Keys)
            {
                if (node == neighbour)
                {
                    path.Add(node);
                }
            }
        }
    }
}