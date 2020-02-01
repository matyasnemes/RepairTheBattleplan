using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableAction : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        var fighter = other.gameObject.GetComponent(typeof(Fighter));
        if (fighter != null)
        {
            ((Fighter)fighter).turn();
        }
    }
}
