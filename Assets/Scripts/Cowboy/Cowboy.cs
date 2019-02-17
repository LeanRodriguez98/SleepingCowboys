using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowboy : MonoBehaviour
{

    private SkinnedMeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Vector3[] baseVertices;
    private Player player;
    private Material material;
    private float materialAlpha = 1.0f;
    private bool disappear = false;
    private ObjectPooler objectPoolerInstance;
    private PoolManager poolManagerInstance;
    private GameManager gameManagerInstance;
    private string[] bloodEffectsTags;

    private AudioSource audioSource;

    [System.Serializable]
    public struct RandomVolume
    {
        public bool randomVolume;
        [Range(0.0f, 1.0f)] public float minSnoringVolume;
        [Range(0.0f, 1.0f)] public float maxSnoringVolume;
    }

    [Header("The idle loop clip")]
    public AudioClip snoringClip;
    [Space(10)]
    [Header("The idle loop clip's volume")]
    [Range(0.0f, 1.0f)] public float snoringVolume;
    public RandomVolume randomVolume;

    [Space(10)]
    [Header("The scale's multipler of the collidere")]
    public Vector3 colliderScale = Vector3.one / 3;


    public void SetPlayer(Player playerType)
    {
        player = playerType;
    }

    void Start()
    {
        objectPoolerInstance = ObjectPooler.instance;
        poolManagerInstance = PoolManager.instance;
        gameManagerInstance = GameManager.instance;
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = snoringClip;

        if (randomVolume.randomVolume)
        {
            if (randomVolume.minSnoringVolume < randomVolume.maxSnoringVolume)
            {
                snoringVolume = Random.Range(randomVolume.minSnoringVolume, randomVolume.maxSnoringVolume);
            }
            else
            {
                Debug.LogWarning("The random Volume on " + gameObject.name + " posisionated in " + gameObject.transform.position + " have a minimum greater volume value (" + randomVolume.minSnoringVolume + ") at the maximum value (" + randomVolume.maxSnoringVolume + "). The minimum value it's assigned!");
                snoringVolume = randomVolume.minSnoringVolume;
            }
        }
        
        audioSource.volume = snoringVolume;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1;
        audioSource.loop = true;
        audioSource.Play();


        bloodEffectsTags = poolManagerInstance.GetTagsGrup("Blood");
    }

    public void InvokeDie()
    {
        Invoke("Die", player.shootTime);

    }

    public void Die()
    {
        meshCollider.enabled = false;

        SpawnBlood();
    }

    public void SpawnBlood()
    {

        int index = Random.Range(0, bloodEffectsTags.Length);

        
            objectPoolerInstance.SpawnParticleFromPool(bloodEffectsTags[index], player.GetHitPosition(),
              Quaternion.Euler(player.gun.transform.eulerAngles - new Vector3(-180, 0, -180))); 
        Invoke("Desappear", objectPoolerInstance.GetParticleSistemLengh(bloodEffectsTags[index]));
        gameManagerInstance.UpdateRemainingCowboys(1);
    }

    public void Desappear()
    {
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

    public void GenerateCollider()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);

        baseVertices = colliderMesh.vertices;
        Vector3[] vertices = new Vector3[baseVertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];
            vertex.x = vertex.x * colliderScale.x;
            vertex.y = vertex.y * colliderScale.y;
            vertex.z = vertex.z * colliderScale.z;
            vertices[i] = vertex;
        }

        colliderMesh.vertices = vertices;

        colliderMesh.RecalculateNormals();
        colliderMesh.RecalculateBounds();

        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = colliderMesh;


    }

}