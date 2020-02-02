using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfter : MonoBehaviour
{

    private float time;

    public float timeToLive = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > time + timeToLive)
        {
            Destroy(this.gameObject);
        }   
    }
}
