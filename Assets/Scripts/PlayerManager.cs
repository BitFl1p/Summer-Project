using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(instance);
    }
    public PlayerMovement player;
    public Camera cam;
    public CinemachineFreeLook fCam;
    public GroundCheck groundCheck;
    public Animator anim;
}
