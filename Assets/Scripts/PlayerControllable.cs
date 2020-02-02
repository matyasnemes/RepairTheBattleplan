using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllable : MonoBehaviour
{
    private GameActor actor;

    void Start()
    {
        // Getting actor component
        actor = GetComponent<GameActor>();
    }

    void Update()
    {
        // Checking death condition
        if (actor.getHealth() == 0) return;

        // Reading player movement speed
        float speed = GameplayController.getGameplayOptions().playerSpeed;

        // Movement indicator
        bool moving = false;

        // Getting current position
        Vector3 newPosition = transform.position;

        // Performing movement
        if(Input.GetKey("w"))
        {
            newPosition.y += speed * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey("a"))
        {
            newPosition.x -= speed * Time.deltaTime;
            moving = true;

            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.flipX = true;
        }
        if (Input.GetKey("s"))
        {
            newPosition.y -= speed * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey("d"))
        {
            newPosition.x += speed * Time.deltaTime;
            moving = true;

            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.flipX = false;
        }

        // Updating animation controller
        if (moving) actor.doMoveAnimation();

        // Updating position
        transform.position = newPosition;
    }
}
