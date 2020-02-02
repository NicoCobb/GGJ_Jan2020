using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite_animation : MonoBehaviour
{
    // i dont understand sprite animation :(
    const int IDLE = 0;
    const int WALK = 1;
    const int JUMP = 2;
    const int LAND = 3;
    const int GRAPPLE = 4;

    Animator animator;

    int _currAniState = IDLE;

    Player p;
    Controller2D c;

    // Start is called before the first frame update
    void Start()
    {
        p = GetComponentInParent<Player>();
        c = GetComponentInParent<Controller2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            changeState(GRAPPLE);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            changeState(JUMP);
        }
        else if (p.directionalInput.x != 0)
        {
            changeState(WALK);
        }

        else if (c.collisions.below == true)
        {
            changeState(IDLE);
        }
        
        else if (Input.GetMouseButtonUp(0))
        {
            changeState(IDLE);
        }
        
        
        
        
        
    }
    void changeState(int state)
    {
        if (_currAniState == state) return;

        switch (state)
        {
            case IDLE:
                animator.SetInteger("state", IDLE);
                break;
            case (JUMP):
                animator.SetInteger("state", JUMP);
                break;
            case WALK:
                animator.SetInteger("state", WALK);
                break;
            case GRAPPLE:
                animator.SetInteger("state", GRAPPLE);
                break;
        }

        _currAniState = state;
    }
}
