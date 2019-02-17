using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [Header("The bullet speed")]
    public float bulletSpeed;
    [Space(10)]
    [Header("The time between the bullet spawn and disapear")]
    public float TimeOfLife;
	
    private void OnEnable()
    {
        Invoke("Disable", TimeOfLife);
    }
    void Update () {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
	}
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
