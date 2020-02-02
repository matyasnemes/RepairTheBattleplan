using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float speed;

    public Node currentNode;
    public Vector2? target;
    public Vector3 forward;
    enemy eToFollow = null;

    public Vector2 doorDirection;
    BoxCollider2D m_collider;

    private bool inFight = false;


    System.Random rnd;

    // Use this for initialization
    void Start()
    {
        m_collider = GetComponent<BoxCollider2D>();
        forward = new Vector3(1, 0, 0);
        speed = GameplayController.getGameplayOptions().knightSpeed;
        rnd = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {

        float step = speed * Time.deltaTime;

        if (forward.x == 1 && GetComponent<SpriteRenderer>().flipX == false)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (forward.x == -1 && GetComponent<SpriteRenderer>().flipX == true)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)target + new Vector2(0.0f, 0.16f), step);

            GetComponent<GameActor>().doMoveAnimation();
            if (Vector2.Distance((Vector2)target + new Vector2(0.0f, 0.16f), transform.position) < 0.1)
            {
                target = null;
                forward = doorDirection;
            }
        }
        else if (eToFollow != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, eToFollow.transform.position + new Vector3(0.32f, 0.0f), step);
            GetComponent<GameActor>().doMoveAnimation();
            if ((Vector2.Distance(eToFollow.transform.position + new Vector3(0.32f, 0.0f), transform.position) < 0.1))
            {
                eToFollow = null;
            }
        }
        else if (eToFollow == null && inFight)
        {
            GetComponent<GameActor>().doHitAnimation();
        }
        else if (!inFight)
        {
            GetComponent<GameActor>().doMoveAnimation();
            transform.position += forward * step;
        }

    }

    public void fight(enemy e)
    {
        eToFollow = e;
        inFight = true;
        GetComponent<GameActor>().doHitAnimation();
        //currentNode.killed(e);
    }
    public void turn()
    {
        if (target != null)
            return;

        var direction = rnd.Next(0, 2);

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
