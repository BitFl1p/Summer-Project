using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerMovement player;
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground") player.isGrounded = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground") player.isGrounded = false;
    }
}
