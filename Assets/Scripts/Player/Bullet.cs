using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    public float TimeOfLife;
	void Start () {
		
	}
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
