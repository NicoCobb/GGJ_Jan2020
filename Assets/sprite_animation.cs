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
        /*
        if(Input.GetKey("space") && _isGrounded)
        {
            changeState(JUMP);
            _isGrounded = false;
        }
        else if (!_isGrounded && )
        {

        }
        */
    }
}
