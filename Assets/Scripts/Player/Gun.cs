using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public GameObject bulletSpawnPoint;
    public Camera cam;
    private Vector3 hitPoint;
    void Start () {
		
	}
	
	void Update () {

    }

  

    public bool Fire()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100.0f))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 100, Color.green, 10.0f);
            hitPoint = hit.point;
            Cowboy cowboy = hit.transform.GetComponentInParent<Cowboy>();
            if (cowboy != null)
            {
                cowboy.Die();
                Debug.Log("Fire");
                return true;
            }
        }
        return false;
    }

    public Vector3 GetHitPoint()
    {
        return hitPoint;
    }

    public void SpawnBullet()
    {
        Debug.Log("BulletSpawned");
    }
}
