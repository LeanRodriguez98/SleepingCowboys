using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{



    public static Player playerInstacne;
    [HideInInspector] public Gun gun;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Ray pointerRay;
    [HideInInspector] public Vector3 hitPosition;

    public float shootTime;
    public float movementSpeed;


    public virtual Ray GetPointerRay()
    { return pointerRay; }

    public virtual Vector3 GetHitPosition()
    { return hitPosition; }

   
}

