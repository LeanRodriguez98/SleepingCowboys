using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{



    [HideInInspector] public Gun gun;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Ray pointerRay;
    [HideInInspector] public Vector3 hitPosition;
    [HideInInspector] public bool gameOver = false;
    [Header("The player's base data")]
    [Header("   The delay of the shoot (Only VR)")]
    public float shootTime;
    [Header("   The movement spped")]
    public float movementSpeed;
    [Header("   The delay between the time it's out our the player kill all cowboys and the fade image")]
    public float gameOverDelay = 0;

    [Header("   The boost of movementSpeed")]
    public float boostSpeed;
    [Header("   The time the boost of movementSpeed last")]
    public float boostTimer;

    public float speed;
    public float speedTimer;

    public virtual Ray GetPointerRay()
    { return pointerRay; }

    public virtual Vector3 GetHitPosition()
    { return hitPosition; }

    public virtual void EnableGameOverCanvas(float t)
    { }

    public void SetMovementSpeed(float _movementSpeed)
    {
        movementSpeed = _movementSpeed;
    }

    public void ResetMovementSpeed()
    {
        movementSpeed = speed;
    }

    public void CheckSpeedTimer()
    {
        if (movementSpeed != speed)
        {
            speedTimer += Time.deltaTime;

            if (speedTimer >= boostTimer)
            {
                speedTimer = 0;
                SetMovementSpeed(speed);
            }
        }
    }
}

