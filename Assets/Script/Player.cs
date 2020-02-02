using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

    public float grappleShotSpeed;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	[HideInInspector]
	public float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	[HideInInspector]
	public Vector3 velocity;
	float velocityXSmoothing;
	Controller2D controller;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;

    //TODO: grapply object
    public GameObject GrapplePrefab;

	GameObject grappleInstance;

	[HideInInspector]
	public bool isSwinging = false;
	bool initSwing = true;

	Vector2 currentGrappleDir;

	SpriteRenderer sp;
	//public Sprite idle;
	//public Sprite grapple;

	void Start() {
		controller = GetComponent<Controller2D> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

		sp = GetComponentInChildren<SpriteRenderer>();
		//idle = Art.Load("idle1", typeof(Sprite)) as Sprite;
		// i still dont understand sprite animation or how to change sprites :( 
	}

	void Update() {
		if(isSwinging) {
			HandleSwing();
			controller.Move (velocity * Time.deltaTime);
		} else {
			CalculateVelocity();
			controller.Move (velocity * Time.deltaTime, directionalInput);
		}
		// CalculateVelocity();
        HandleWallClip();
		// HandleSwing();


		if (controller.collisions.above || controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
			} else {
				velocity.y = 0;
			}
		}
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (wallSliding) {
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = maxJumpVelocity;
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

    void HandleWallClip()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;

        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below)
        {
            velocity.y = 0;
        }
    }


    void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

    void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}

	public void setSwing(Vector2 grappleDir) {
		isSwinging = true;
		initSwing = true;
		currentGrappleDir = grappleDir;
	}
	void HandleSwing() {
		Vector2 playerDir;
		playerDir.x = currentGrappleDir.y;
		playerDir.y = currentGrappleDir.x;
		playerDir.Normalize();
		playerDir.x = playerDir.x * Mathf.Sign(velocity.x);
		playerDir.y = playerDir.y * Mathf.Sign(velocity.y);

		//the initial swing velocity takes no inputs and just resets the velocity direction
		if(initSwing) {
			//We want the initial swing velocity to be the same as current velocity, how do?
			velocity = new Vector2(playerDir.x * Mathf.Abs(velocity.x), playerDir.y * Mathf.Abs(velocity.y));
			initSwing = false;
			return;
		}
		float grappleScalar = 2.0f;

		//find the direction we should be moving

		//allow direction to influence trajectory
		if(Mathf.Sign(directionalInput.x) == Mathf.Sign(playerDir.x)) {
			grappleScalar*=1.5f;
		} else {
			grappleScalar*=0.5f;
		}

		//use dotProd to see how much gravity should influence swing
		float grappleDotProd = Vector2.Dot(velocity, currentGrappleDir);
		grappleScalar*= grappleDotProd*gravity;
	}

    public void ShootGrapple(Vector2 mousePos) {
        grappleInstance = (GameObject) Instantiate(GrapplePrefab, transform.position, Quaternion.identity);
        grappleInstance.GetComponent<GrapplingHook>().AimTongue(mousePos, this, grappleShotSpeed);
		//this.GetComponent<SpriteRenderer>().sprite = grapple;
    }
	
	public void EndGrapple() {
		Destroy(grappleInstance);
		isSwinging = false;
		initSwing = true;
		//this.GetComponent<SpriteRenderer>().sprite = idle;
	}

	public void SetVelocity(Vector2 vec) {
		velocity.x = vec.x;
		velocity.y = vec.y;
	}
}