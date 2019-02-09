
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VR : Player
{

    private bool movement = false;

    public GvrReticlePointer reticlePointer;
    public Camera cam;

    private void Awake()
    {
        playerInstacne = this;

    }

    public void ShootBullet()
    {
        gun.SpawnBullet();
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();
        pointerRay = reticlePointer.GetRayForDistance(reticlePointer.ReticleDistanceInMeters).ray;
        hitPosition = reticlePointer.GetPointAlongPointer(reticlePointer.ReticleDistanceInMeters);
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
    // Update is called once per frame
    void Update()
    {
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

