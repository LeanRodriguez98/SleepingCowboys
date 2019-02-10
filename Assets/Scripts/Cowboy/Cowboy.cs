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
    private string[] bloodEffectsTags;

    public Vector3 colliderScale = Vector3.one / 3;
    // Use this for initialization


    // Update is called once per frame
    void Start()
    {
        player = Player.playerInstacne;
        objectPoolerInstance = ObjectPooler.instance;
        poolManagerInstance = PoolManager.instance;
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshCollider = GetComponentInChildren<MeshCollider>();
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

        RaycastHit hitInfo;
        if (Physics.Raycast(player.GetPointerRay(), out hitInfo, 100f))
            objectPoolerInstance.SpawnParticleFromPool(bloodEffectsTags[index], player.GetHitPosition(),
              //Quaternion.FromToRotation(Vector3.up, hitInfo.normal));// Replace this whit the bottom code's line if the blood don't looks good. This line spawn the blood to the normal of the mesh 
              Quaternion.Euler(player.gun.transform.eulerAngles - new Vector3(-180, 0, -180))); // and this line spawn the blood faceing the gun
        Invoke("Desappear", objectPoolerInstance.GetParticleSistemLengh(bloodEffectsTags[index]));
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