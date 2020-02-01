using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollidable : MonoBehaviour {

    Collider m_ObjectCollider;

    // Use this for initialization
    void Start () {
        m_ObjectCollider = GetComponent<Collider>();
        m_ObjectCollider.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
