using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipable : MonoBehaviour
{
    private Vector3 oldPosition;
    public Vector3 referenceAxis;
    void Start()
    {
        referenceAxis = new Vector3(1, 0, 0);
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var direction = Vector3.Dot(transform.position - oldPosition, referenceAxis);
        oldPosition = transform.position;
        if (direction >= 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
