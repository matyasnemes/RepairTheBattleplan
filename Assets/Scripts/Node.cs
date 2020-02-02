using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct doorData
{
    public Vector2 position;
    public Vector2 direction;

    public doorData(Vector2 pos, Vector2 dir)
    {
        position = pos;
        direction = dir;
    }
}

public class Node
{
    private System.Random rnd = new System.Random();
    public bool visited = false;
    public Dictionary<Node, doorData> neighbours = new Dictionary<Node, doorData>();
    public int fighterCount = 0;

    public bool wasEnemy = false;

    public int enemyCount = 0;
    private List<enemy> enemies;

    public void setEnemies(List<enemy> enem)
    {
        if (enem.Count == 0)
            return;
        enemies = enem;
        wasEnemy = true;
        enemyCount = enemies.Count;
    }

    public void killed(enemy e)
    {
        if (enemies.Contains(e))
        {
            enemies.Remove(e);
            enemyCount--;
        }
    }

    public void fight(Fighter f)
    {
        int i = rnd.Next(0, enemies.Count);
        var e = enemies[i];
        //idleEnemies.RemoveAt(i);
        f.fight(e);
        e.fight(f);
    }

    private Node randomChoose(List<Node> keyList)
    {
        if (keyList.Count == 0)
            return null;

        int r = rnd.Next(0, keyList.Count);
        Node key = keyList[r];
        if (!key.visited)
        {
            return key;
        }

        keyList.RemoveAt(r);

        randomChoose(keyList);
        return null;
    }

    public doorData chooseDoor()
    {
        List<Node> keyList = new List<Node>(neighbours.Keys);

        Node key = randomChoose(keyList);
        if (key != null)
            return neighbours[key];

        var route = findPathToNew(this);
        var nextNode = route[0];
        route.RemoveAt(0);
        FighterController.route = route;
        return neighbours[nextNode];
    }

    public doorData findDoor(Node toRoom)
    {
        return neighbours[toRoom];
    }

    public static List<Node> findPathToNew(Node from)
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

        while (!currentNode.Equals(from))
        {
            findBaseNode(currentNode, traverseOrder, path);
            currentNode = path[path.Count - 1];
        }
        path.Reverse();
        //ahol vagyunk az nem kell az útvonal tervezéshez
        path.RemoveAt(0);
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
                    return;
                }
            }
        }
    }
}