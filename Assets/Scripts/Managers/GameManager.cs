


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameManager : MonoBehaviour
{
    [Header("Elements of the VR game")]
    public Player player_VR;
    public GameObject[] player_VR_Elements;
    [Space(10)]
    [Header("Elements of the PC game")]
    public Player player_PC;
    public GameObject[] player_PC_Elements;
    [Space(20)]
    public List<Cowboy> cowboys;
    public int cantOfCowboysInGame;
    // Use this for initialization
    void Start()
    {

#if UNITY_STANDALONE_WIN
        Application.targetFrameRate = 120;
        player_PC.gameObject.SetActive(true);

        for (int i = 0; i < player_PC_Elements.Length; i++)
		{
            player_PC_Elements[i].SetActive(true);
            
		}
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        for (int i = 0; i < cowboys.Count; i++)
        {
            cowboys[i].SetPlayer(player_PC);
        }

#endif

#if UNITY_ANDROID
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        player_VR.gameObject.SetActive(true);
        for (int i = 0; i < player_VR_Elements.Length; i++)
        {
            player_VR_Elements[i].SetActive(true);

        }

        for (int i = 0; i < cowboys.Count; i++)
        {
            cowboys[i].SetPlayer(player_VR);
        }

#endif

        

        for (int i = 0; i < cantOfCowboysInGame; i++)
        {
            int r = Random.Range(0, cowboys.Count);
            cowboys.Remove(cowboys[r]);
        }

        for (int i = 0; i < cowboys.Count; i++)
        {
            cowboys[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FindCowboys()
    {
        cowboys = new List<Cowboy>();
        Cowboy[] c = FindObjectsOfType<Cowboy>();
        for (int i = 0; i < c.Length; i++)
            cowboys.Add(c[i]);
    }


}