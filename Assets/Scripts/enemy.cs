using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    Fighter fToFollow=null;
    bool inFight = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = GameplayController.getGameplayOptions().goblinSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (fToFollow != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, fToFollow.transform.position - new Vector3(0.32f, 0.0f), step);
            GetComponent<GameActor>().doMoveAnimation();
            inFight = true;
        }
        if (inFight)
        {
            GetComponent<GameActor>().doHitAnimation();

        }
    }

    public void fight(Fighter f)
    {
        fToFollow = f;
        
        //ANIM HIVAS IDE
    }

}
