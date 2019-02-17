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
    public float shootTime;
    public float movementSpeed;


    public virtual Ray GetPointerRay()
    { return pointerRay; }

    public virtual Vector3 GetHitPosition()
    { return hitPosition; }

    public virtual void EnableGameOverCanvas()
    { }

}

