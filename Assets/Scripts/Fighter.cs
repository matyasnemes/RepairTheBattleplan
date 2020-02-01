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
    BoxCollider2D m_collider;

    private Vector2? target;

    // Use this for initialization
    void Start()
    {
        m_collider = GetComponent<BoxCollider2D>();
        forward = new Vector3(1, 0, 0);
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {

        float step = speed * Time.deltaTime;

        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)target, step);
            if (Vector2.Distance((Vector2)target, transform.position) < 0.1)
                target = null;
        }

        else
        {
            transform.position += forward * step;
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
            //folyoson vagyunk, vissza kell vergodni a szobaig
        }
        else
        {
            //szobaban vagyubnk uj utkereses
            target = currentNode.chooseDoor(this, true);
        }
    }

    public void turn()
    {
        var direction = Random.Range(0, 1);

        if (direction == 0) direction = -1;
        else direction = 1;

        var castDirection = Quaternion.Euler(0, 0, 90 * direction) * forward;

        RaycastHit2D[] results = new RaycastHit2D[4];
        int numberOfCollisions = m_collider.Raycast(castDirection, results, 0.5f);
        if (numberOfCollisions == 0)
        {
            forward = castDirection;
        }
        else
        {
            castDirection *= -1;
            numberOfCollisions = m_collider.Raycast(castDirection, results, 0.5f);
            if (numberOfCollisions == 0)
            {
                forward = castDirection;
            }
        }
    }
}
