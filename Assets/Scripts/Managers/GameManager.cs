


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] player_VR;
    public GameObject[] player_PC;
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#if UNITY_STANDALONE_WIN
        Application.targetFrameRate = 120;
        for (int i = 0; i < player_PC.Length; i++)
		{
            player_PC[i].SetActive(true);
            
		}
#endif

#if UNITY_ANDROID
        Application.targetFrameRate = 60;
        for (int i = 0; i < player_VR.Length; i++)
        {
           // player_VR[i].SetActive(true);

        }
#endif


    }

    // Update is called once per frame
    void Update()
    {

    }
}
