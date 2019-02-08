using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bulletSpawnPoint;
    public Camera cam;

    void Start () {
		
	}
	
	void Update () {
       
        
    }

    public void InvokeFire(float timeToFire)
    {
        Invoke("Fire", timeToFire);
    }

    public void Fire()
    {
       
    }
}
