using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    Animator anim;
    Rigidbody rb;
    PlayerInput input;
    Vector2 moveInput = Vector2.zero;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); 
        
    }
    private void FixedUpdate()
    {
        float yVel = rb.velocity.y;
        rb.velocity += Camera.main.transform.forward * moveInput.y * speed + Camera.main.transform.right * moveInput.x * speed;
        transform.LookAt(transform.position + rb.velocity);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        rb.velocity = new Vector3(rb.velocity.x, yVel, rb.velocity.z);
        anim.SetFloat("WalkSpeed", rb.velocity.magnitude);
        
    }
}
