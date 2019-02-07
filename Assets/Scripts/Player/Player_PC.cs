
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PC : Player
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override Ray GetPointerRay()
    {
        return pointerRay;
    }

    public override Vector3 GetHitPosition()
    {
        return hitPosition;
    }
}
