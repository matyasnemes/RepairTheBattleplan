using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float speed;
    public Vector3 forward;
    public List<Node> route = new List<Node>();
    public Node currentNode;
    public Node targetNode;

    private Vector2? target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float step = speed * Time.deltaTime;

        var position = transform.position;
        if (target != null)
        {
            position = Vector2.MoveTowards(position, (Vector2)target, step);
            if (Vector2.Distance((Vector2)target, position) < 0.0001)
                target = null;
        }

        if (target == null)
        {
            position += forward;
        }

    }

    public void goOut()
    {
        if (route.Any())
        {
            target = currentNode.findDoor(route[0]);
            route.RemoveAt(0);
        }
        else
        {
            target = currentNode.chooseDoor(this, false);
        }
    }

    public void doorClosed()
    {
        if (currentNode == null)
        {

        }
        else
        {
            target = currentNode.chooseDoor(this,true);
        }
    }
}
