using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour {

    private GameManager gameManagerInstance;
 
    private ObjectPooler objectPoolerInstance;
    private PoolManager poolManagerInstance;
    private string[] easterEggsTags;
    private float materialAlpha = 1.0f;
    private bool disappear = false;
    [Header("The time of the player's gain if find it")]
    public GameManager.Timer timeToAdd;
    [Space(10)]
    [Header("The particles thet spawn when the player's find it")]
    public ParticleSystem particles;
    [Space(10)]
    [Header("The collider of this object")]
    public Collider capsuleCollider;
    [Space(10)]
    [Header("The mesh renderer of this object")]
    public MeshRenderer meshRenderer;
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
