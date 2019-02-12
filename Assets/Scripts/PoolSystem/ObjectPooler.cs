using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    [HideInInspector] public string tag;
    public GameObject prefab;
    public int size;
}

[System.Serializable]
public class PoolOfParticles
{
    [HideInInspector] public string tag;
    public ParticleSystem particlesPrefab;
    public int size;
}

[System.Serializable]
public class PoolOfSounds
{
    [HideInInspector] public string tag;
    public PoolableAudio soundPrefab;
    public int size;
}

public class ObjectPooler : MonoBehaviour
{
    [Header("Objects")]
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    [Space(10)]
    [Header("Particles")]
    public List<PoolOfParticles> particlesPools;
    public Dictionary<string, Queue<ParticleSystem>> particlesPoolDictionary;
    [Space(10)]
    [Header("Sounds")]
    public List<PoolOfSounds> soundsPools;
    public Dictionary<string, Queue<PoolableAudio>> soundsPoolsDictionary;

    public static ObjectPooler instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        particlesPoolDictionary = new Dictionary<string, Queue<ParticleSystem>>();
        soundsPoolsDictionary = new Dictionary<string, Queue<PoolableAudio>>();
        foreach (Pool pool in pools)
        {
            pool.tag = pool.prefab.gameObject.name;

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab);
                go.transform.SetParent(this.gameObject.transform);
                go.SetActive(false);
                objectPool.Enqueue(go);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

        foreach (PoolOfParticles poolParticle in particlesPools)
        {
            poolParticle.tag = poolParticle.particlesPrefab.gameObject.name;

            Queue<ParticleSystem> particlePool = new Queue<ParticleSystem>();

            for (int i = 0; i < poolParticle.size; i++)
            {
                ParticleSystem ps = Instantiate(poolParticle.particlesPrefab);
                ps.transform.SetParent(this.gameObject.transform);
                ps.gameObject.SetActive(false);
                particlePool.Enqueue(ps);
            }

            particlesPoolDictionary.Add(poolParticle.tag, particlePool);
        }

        foreach (PoolOfSounds poolSound in soundsPools)
        {
            poolSound.tag = poolSound.soundPrefab.gameObject.name;

            Queue<PoolableAudio> soundPool = new Queue<PoolableAudio>();

            for (int i = 0; i < poolSound.size; i++)
            {
                PoolableAudio aso = Instantiate(poolSound.soundPrefab);
                aso.transform.SetParent(this.gameObject.transform);
                aso.gameObject.SetActive(false);
                soundPool.Enqueue(aso);
            }

            soundsPoolsDictionary.Add(poolSound.tag, soundPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " dosen't exist in " + gameObject.name);
            return null;
        }
        GameObject goToSpawn = poolDictionary[tag].Dequeue();
        if (goToSpawn != null)
        {
            goToSpawn.SetActive(true);
            goToSpawn.transform.position = position;
            goToSpawn.transform.rotation = rotation;
            poolDictionary[tag].Enqueue(goToSpawn);
            return goToSpawn;
        }
        
        return null;
    }

    public ParticleSystem SpawnParticleFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!particlesPoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool of particles with tag " + tag + " dosen't exist in " + gameObject.name);
            return null;
        }
        ParticleSystem goToSpawn = particlesPoolDictionary[tag].Dequeue();
        if (goToSpawn != null)
        {
            goToSpawn.gameObject.SetActive(true);
            goToSpawn.gameObject.transform.position = position;
            goToSpawn.gameObject.transform.rotation = rotation;
            particlesPoolDictionary[tag].Enqueue(goToSpawn);
            goToSpawn.Play();
            return goToSpawn;
        }
        return null;

    }

    public PoolableAudio SpawnSoundFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!soundsPoolsDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool of sounds with tag " + tag + " dosen't exist in " + gameObject.name);
            return null;
        }
        PoolableAudio goToSpawn = soundsPoolsDictionary[tag].Dequeue();
        if (goToSpawn != null)
        {
            goToSpawn.gameObject.SetActive(true);
            goToSpawn.gameObject.transform.position = position;
            goToSpawn.gameObject.transform.rotation = rotation;
            soundsPoolsDictionary[tag].Enqueue(goToSpawn);
            goToSpawn.PlayOneShot();
            return goToSpawn;
        }
        return null;

    }

    public PoolableAudio SpawnSoundFromPool(string tag, Vector3 position, Quaternion rotation, float time)
    {

        if (!soundsPoolsDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool of sounds with tag " + tag + " dosen't exist in " + gameObject.name);
            return null;
        }
        PoolableAudio goToSpawn = soundsPoolsDictionary[tag].Dequeue();
        if (goToSpawn != null)
        {
            goToSpawn.gameObject.SetActive(true);
            goToSpawn.gameObject.transform.position = position;
            goToSpawn.gameObject.transform.rotation = rotation;
            soundsPoolsDictionary[tag].Enqueue(goToSpawn);
            goToSpawn.PlayOneShot(time);
            return goToSpawn;
        }
        return null;

    }

    public float GetParticleSistemLengh(string tag)
    {
        if (!particlesPoolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool of particles with tag " + tag + " dosen't exist in " + gameObject.name);
            return 0.0f;
        }

        ParticleSystem ps = particlesPoolDictionary[tag].Dequeue();

        float t = ps.main.duration;

        particlesPoolDictionary[tag].Enqueue(ps);

        return t;

    }

    public float GetSoundDurationh(string tag)
    {
        if (!soundsPoolsDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool of sounds with tag " + tag + " dosen't exist in " + gameObject.name);
            return 0.0f;
        }

        PoolableAudio aso = soundsPoolsDictionary[tag].Dequeue();

        float t = aso.GetSoundDuration();

        soundsPoolsDictionary[tag].Enqueue(aso);

        return t;

    }
}

