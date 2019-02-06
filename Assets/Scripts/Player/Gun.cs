using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bulletSpawnPoint;
    public Camera cam;
    public GameObject cube;

    void Start () {
		
	}
	
	void Update () {
       
       //Instantiate(cube, reticlePointer.GetPointAlongPointer(reticlePointer.ReticleDistanceInMeters), Quaternion.identity);
        
    }

    public void InvokeFire(float timeToFire)
    {
        Invoke("Fire", timeToFire);
    }

    public void Fire()
    {
       
    }
}
