
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PC : Player
{
    public Camera cam;
    public float cameraSpeedHorizontal = 1.0f;
    public float cameraSpeedVertical = 1.0f;
    public float maxCameraAngleUp = 90.0f;
    public float maxCameraAngleDown = -90.0f;
    private float cameraRotationX = 0.0f;
    private float cameraRotationY = 0.0f;
   
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(gun.Fire())
                gun.SpawnBullet();
        }
    }

    private void FixedUpdate()
    {
        
        cameraRotationX -= Input.GetAxis("Mouse Y") * cameraSpeedVertical * Time.deltaTime;
        cameraRotationY += Input.GetAxis("Mouse X") * cameraSpeedHorizontal * Time.deltaTime;
        if (cameraRotationX > maxCameraAngleUp)
            cameraRotationX = maxCameraAngleUp;
        if (cameraRotationX < maxCameraAngleDown)
            cameraRotationX = maxCameraAngleDown;

        cam.transform.eulerAngles = new Vector3(cameraRotationX, cam.transform.eulerAngles.y, 0.0f);
        transform.eulerAngles = new Vector3(0.0f, cameraRotationY, 0.0f);
        rb.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime) +
                                             (transform.right * Input.GetAxis("Horizontal") * movementSpeed) * Time.deltaTime);

    }

    public override Ray GetPointerRay()
    {
        pointerRay = new Ray(cam.transform.position, cam.transform.forward);
        return pointerRay;
    }

  

    public override Vector3 GetHitPosition()
    {
        hitPosition = gun.GetHitPoint();

        return hitPosition;
    }
}
