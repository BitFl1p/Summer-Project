using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public float jumpHeight;
    public float maxJumpCharge;
    float jumpCharge;
    public float turnSpeed;

    bool sprinting;
    public bool isGrounded;
    public bool isWallRunning;
    public bool canMove;

    Animator anim;
    Rigidbody rb;

    #region Don't worry about it
    float turnSmoothVelocity;
    
    Vector2 moveInput = Vector2.zero;
    Vector3 moveDir = Vector3.zero;
    #endregion
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    void OnJump()
    {
        if((isGrounded || isWallRunning) && canMove)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            isGrounded = isWallRunning = false;
        }
    }
    
    void FixedUpdate()
    {
        if (canMove) if(sprinting) FigureOutMovement(speed, maxSpeed*6);
        else FigureOutMovement(speed, maxSpeed);
        anim.SetBool("Grounded", isGrounded);
        rb.velocity = new Vector3(rb.velocity.x - rb.velocity.x / speed, rb.velocity.y, rb.velocity.z - rb.velocity.z / speed);

    }
    #region Don't worry about it
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnSprint(InputValue value)
    {
        sprinting = value.isPressed;
    }
    void FigureOutMovement(float speed, float maxSpeed)
    {
        float yVel = rb.velocity.y;
        if (moveInput.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSpeed);
            transform.eulerAngles = new Vector3(0, angle, 0);
            moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            // I'm not even gonna try with this one. figure out angles for character to move using weird maths I found on brackeys
        }
        else
        {
            moveDir = Vector3.zero;
        }
        
        Vector3 movement = new Vector3(moveDir.x * speed, 0, moveDir.z * speed);
        rb.velocity += movement;
        Vector2 bruh = Vector2.ClampMagnitude(new Vector2(rb.velocity.x, rb.velocity.z), maxSpeed);
        rb.velocity = new Vector3(bruh.x, rb.velocity.y, bruh.y);
        anim.SetFloat("WalkSpeed", Mathf.Lerp(anim.GetFloat("WalkSpeed"), new Vector2(rb.velocity.x, rb.velocity.z).magnitude, turnSpeed));
    }
    float Drag(float val, float drag)
    {
        if (val >= 0) val -= drag * .8f;
        else val += drag * .8f;
        if (val > -drag && val < drag) val = 0;
        return val;
    }
    #endregion
}
