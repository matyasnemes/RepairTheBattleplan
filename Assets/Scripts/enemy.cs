using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed;
    Fighter fToFollow=null;
    bool inFight = false;
    private float damageTakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        speed = GameplayController.getGameplayOptions().goblinSpeed;
    }

    // Update is called once per frame
    void Update() 
    {
        GameActor self = GetComponent<GameActor>();
        if (self.getHealth() < 0.1f) return;

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

        damageTakeTimer -= Time.deltaTime;
        if(damageTakeTimer <= 0.0f && inFight)
        {
            // Damage self
            self.setHealth(self.getHealth() - 20);

            // Reset timer
            damageTakeTimer = 1.0f;
        }
    }

    public void fight(Fighter f)
    {
        fToFollow = f;
        
        //ANIM HIVAS IDE
    }

}
