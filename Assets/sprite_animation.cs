using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite_animation : MonoBehaviour
{
    // i dont understand sprite animation :(

    public float walkSpeed = 1;
    private bool _isGrounded = false;
    const int IDLE = 0;
    const int WALK = 1;
    const int JUMP = 2;
    const int LAND = 3;
    const int GRAPPLE = 4;

    Animator animator;

    bool _isWalk = false;

    string currDir = "right";
    int _currAniState = IDLE;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            changeState(GRAPPLE);
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

            case GRAPPLE:
                animator.SetInteger("state", GRAPPLE);
                break;
        }

        _currAniState = state;
    }
}
