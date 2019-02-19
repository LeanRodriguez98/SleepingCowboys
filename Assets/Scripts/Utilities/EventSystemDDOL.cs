using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystemDDOL : MonoBehaviour {
    private static EventSystemDDOL instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    
}
