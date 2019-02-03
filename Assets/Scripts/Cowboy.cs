using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowboy : MonoBehaviour
{

    private SkinnedMeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Vector3[] _baseVertices;
    public Vector3 colliderScale = Vector3.one / 3;
    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshCollider = GetComponentInChildren<MeshCollider>();
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);

        if (_baseVertices == null)
            _baseVertices = colliderMesh.vertices;
        Vector3[] vertices = new Vector3[_baseVertices.Length];
        for (var i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = _baseVertices[i];
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

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        Debug.Log("Hola");
        gameObject.SetActive(false);
    }
}
