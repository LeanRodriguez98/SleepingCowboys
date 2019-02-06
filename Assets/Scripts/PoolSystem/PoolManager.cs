using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    public static PoolManager instance;
    [System.Serializable]
    public struct PoolGrup
    {
        public string grupTag;
        public GameObject[] grupOffPool;
    }

    public PoolGrup[] poolGrups;

    private void Awake()
    {
        instance = this;
    }

    public string[] GetTagsGrup(string tag)
    {
        List<string> tags = new List<string>();

        for (int i = 0; i < poolGrups.Length; i++)
        {
            if (poolGrups[i].grupTag == tag)
            {
                for (int j = 0; j < poolGrups[i].grupOffPool.Length; j++)
                {
                    tags.Add(poolGrups[i].grupOffPool[j].name);
                }
            }
        }

        if (tags.Count == 0)
        {
            Debug.LogError("The tag " + tag + "dosen't match whit any tag in " + gameObject.name);
        }

        return tags.ToArray();
    }
}
