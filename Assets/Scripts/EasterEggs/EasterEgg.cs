using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour {

    private GameManager gameManagerInstance;
    public GameManager.Timer timeToAdd;
    public ParticleSystem particles;
    public Collider capsuleCollider;
    public MeshRenderer meshRenderer;
    private ObjectPooler objectPoolerInstance;
    private PoolManager poolManagerInstance;
    private string[] easterEggsTags;
    private float materialAlpha = 1.0f;
    private bool disappear = false;
    private void Start()
    {
        gameManagerInstance = GameManager.instance;
        objectPoolerInstance = ObjectPooler.instance;
        poolManagerInstance = PoolManager.instance;

        easterEggsTags = poolManagerInstance.GetTagsGrup("EasterEggs");
    }

    public void InvokeEasterEggFinded(float t)
    {
        Invoke("EasterEggFinded", t);
    }

    public void EasterEggFinded()
    {
        gameManagerInstance.gameTime.minutes += timeToAdd.minutes;
        gameManagerInstance.gameTime.seconds += timeToAdd.seconds;
        gameManagerInstance.gameTime.miliseconds += timeToAdd.miliseconds;
        objectPoolerInstance.SpawnSoundFromPool(easterEggsTags[Random.Range(0, easterEggsTags.Length)], transform.position, Quaternion.identity);
        particles.Play();
        capsuleCollider.enabled = false;
        disappear = true;
    }

    private void Update()
    {
        if (disappear)
        {
            materialAlpha -= Time.deltaTime;
            if (materialAlpha > 0)
            {
                meshRenderer.material.SetFloat("_Alpha", materialAlpha);
            }
            else if (materialAlpha <= 0)
            {
                materialAlpha = 0.0f;
                meshRenderer.material.SetFloat("_Alpha", materialAlpha);
                gameObject.SetActive(false);
            }
        }
    }
}
