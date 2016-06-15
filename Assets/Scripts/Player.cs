using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;

    public float maxJumpVelocity;
    public float minJumpVelocity;

    public float moveSpeed = 6;
    public bool facingRight = true;
    public int jumpHeavyAttackPath;

    [HideInInspector]
    public Vector2 velocity;
    [HideInInspector]
    public float gravity = -50f;

    private Vector2 input = Vector2.zero;
    private Controller2D controller;
    private Animator animator;

    private float velocityXSmoothing;
    private bool slowdownTimeActive;
    private float slowdownTime;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {
        //this.StopHorizontalMovement();
        this.ApplyGravity();
        controller.Move(this.velocity * Time.deltaTime, input);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        this.UpdateAnimator();
    }

    public void Jump(InputActions inputAction, bool groundOnly = true)
    {
        if ((inputAction == InputActions.PRESSED || inputAction == InputActions.HOLD) && ((!groundOnly) || (groundOnly && controller.collisions.below)))
        {
            velocity.y = maxJumpVelocity;
        }
        if ((inputAction == InputActions.RELEASED || inputAction == InputActions.NONE) && velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    private void UpdateAnimator()
    {
        this.animator.SetBool("Grounded", this.controller.collisions.below);
        this.animator.SetFloat("VerticalSpeed", this.velocity.y);
        if (this.velocity.y <= -1 * maxJumpVelocity)
        {
            this.animator.SetBool("FallHardFlag", true);
        }
        animator.SetInteger("JumpHeavyAtackPath", this.jumpHeavyAttackPath);
    }

    private void ApplyGravity()
    {
        this.velocity.y += gravity * Time.deltaTime;
    }

}
