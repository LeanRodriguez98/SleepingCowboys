﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player playerInstacne;
    private Rigidbody rb;
    private bool movement = false;

    public Camera cam;
    public float shootTime;
    public float movementSpeed;

    private void Awake()
    {
        playerInstacne = this;

    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (movement)
        {
            rb.position += (new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * Time.deltaTime * movementSpeed);
        }
    }

    public void EnableMovement()
    {
        movement = true;
    }

    public void DisableMovement()
    {
        movement = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

   
}
