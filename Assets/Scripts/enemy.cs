using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed=1;
    public Vector2? target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)target + new Vector2(0.0f, 0.16f), step);
            if (Vector2.Distance((Vector2)target + new Vector2(0.0f, 0.16f), transform.position) < 0.1)
            {
                target = null;
            }
        }
    }

    public void fight(Vector2 where)
    {
        target = where;
        //ANIM HIVAS IDE
    }

}
