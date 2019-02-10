using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Gun : MonoBehaviour {

    public GameObject bulletSpawnPoint;
    public Camera cam;
    private Vector3 hitPoint;
    private ObjectPooler objectPoolerInstance;
    private PoolManager poolManagerInstance;
    private string[] bulletsTags;
    private void Start()
    {
        objectPoolerInstance = ObjectPooler.instance;
        poolManagerInstance = PoolManager.instance;
        bulletsTags = poolManagerInstance.GetTagsGrup("Bullet");
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
                return true;
            }
        }
        return false;
    }

    public Vector3 GetHitPoint()
    {
        return hitPoint;
    }

    public void SetHitPoint(Vector3 point)
    {
        hitPoint = point;
    }

    public void SpawnBullet()
    {
        bulletSpawnPoint.transform.LookAt(hitPoint);
        objectPoolerInstance.SpawnFromPool(bulletsTags[Random.Range(0,bulletsTags.Length)],bulletSpawnPoint.transform.position,bulletSpawnPoint.transform.rotation);
    }
}
