using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Gun : MonoBehaviour {

    private Vector3 hitPoint;
    private ObjectPooler objectPoolerInstance;
    private PoolManager poolManagerInstance;
    private string[] bulletsTags;
    private string[] shootsTags;

    [Header("The camera of the player")]
    public Camera cam;
    [Space(10)]
    [Header("The point where the bullet spawn")]
    public GameObject bulletSpawnPoint;

    private Animator anim;

    private void Start()
    {
        objectPoolerInstance = ObjectPooler.instance;
        poolManagerInstance = PoolManager.instance;
        bulletsTags = poolManagerInstance.GetTagsGrup("Bullet");
        shootsTags = poolManagerInstance.GetTagsGrup("Shoots");

        anim = GetComponent<Animator>();
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

                FireAnimation(true);

                return true;
            }
            EasterEgg easterEgg = hit.transform.GetComponent<EasterEgg>();
            if (easterEgg != null)
            {
                easterEgg.EasterEggFinded();
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
        //EditorApplication.isPaused = true;
        bulletSpawnPoint.transform.LookAt(hitPoint);
        objectPoolerInstance.SpawnFromPool(bulletsTags[Random.Range(0,bulletsTags.Length)],bulletSpawnPoint.transform.position,bulletSpawnPoint.transform.rotation);
        objectPoolerInstance.SpawnSoundFromPool(shootsTags[Random.Range(0, shootsTags.Length)], bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
    }

    public void FireAnimation(bool active)
    {
        anim.SetBool("Fired", active);
    }

    public void CheckFireAnimation()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Pistol_Fire"))
        {
            FireAnimation(false);
        }
    }
}
