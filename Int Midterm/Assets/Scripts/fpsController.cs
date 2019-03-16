﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsController : MonoBehaviour
{
    public Rigidbody myRb;
    public Camera myCamera;

    public float pitch; //mouse movement up/down
    public float yaw; //mouse movement left/right

    public float fpForwardBackward; //Input float from w and s keys, as well as on the controller
    public float fpStrafe; //You know. 

    public Vector3 inputVelocity; // The cumulative velocity to move the character
    
    public float velocityModifier; //Velocity of the controller mulitplied by this number
    float verticalLook = 0f; 
    
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

 
    void Update()
    {
        yaw = Input.GetAxis("Mouse X");
        transform.Rotate(0,yaw,0);

        pitch = Input.GetAxis("Mouse Y");
        myCamera.transform.Rotate(-pitch, 0, 0);

        fpForwardBackward = Input.GetAxis("Vertical");
        fpStrafe = Input.GetAxis("Horizontal");
        
        inputVelocity = transform.forward * fpForwardBackward;
        inputVelocity += transform.right * fpStrafe;
        
        // BETTER MOUSE LOOK:
        // add mouse input to verticalLook, then clamp verticalLook
        verticalLook += -pitch;
        verticalLook = Mathf.Clamp(verticalLook, -80f, 80f);
		
        // actually apply verticalLook to camera's rotation
        myCamera.transform.localEulerAngles = new Vector3(verticalLook,0f,0f);        

    }

    private void FixedUpdate()
    {
        myRb.velocity = inputVelocity * velocityModifier + (Physics.gravity * .69f);
    }
}
