
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VR : Player
{
    [Space(10)]
    [Header("Player VR' reticle pointer settings")]
    public Camera cam;
    public GvrReticlePointer reticlePointer;
    [Space(10)]
    [Header("Player VR' real space canvas")]
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;

    private bool movement = false;


    public void ShootBullet()
    {
        gun.SpawnBullet();
    }

    // Use this for initialization
    void Start()
    {
        speed = movementSpeed;


        rb = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();
        pointerRay = reticlePointer.GetRayForDistance(reticlePointer.ReticleDistanceInMeters).ray;
        hitPosition = reticlePointer.GetPointAlongPointer(reticlePointer.ReticleDistanceInMeters);
        pauseCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    public override Ray GetPointerRay()
    {
        pointerRay = reticlePointer.GetRayForDistance(reticlePointer.ReticleDistanceInMeters).ray;
        return pointerRay;
    }

    public override Vector3 GetHitPosition()
    {
        hitPosition = reticlePointer.GetPointAlongPointer(reticlePointer.ReticleDistanceInMeters);
        return hitPosition;
    }

    public override void EnableGameOverCanvas(float t)
    {
        pauseCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            CheckSpeedTimer();

            if (movement)
            {
                rb.position += (new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z) * Time.deltaTime * movementSpeed);
            }
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

    public void InvokeSpawnBullet()
    {
        if(!gameOver)
        Invoke("SpawnBullet", shootTime);
    }

    public void SpawnBullet()
    {
        gun.SetHitPoint(GetHitPosition());
        gun.SpawnBullet();
        SetMovementSpeed(boostSpeed);
    }

    
}

