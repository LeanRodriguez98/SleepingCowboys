using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [System.Serializable]
    public struct Timer
    {
        public float minutes;
        public float seconds;
        public float miliseconds;
    }

    [System.Serializable]
    public struct PlayerData
    {
        public Player player;
        public UI_PlayerCanvas playerCanvas;
        public GameObject[] playerElements;
    }

    [Header("Elements of the VR game")]
    public PlayerData player_VR;
    [Space(10)]
    [Header("Elements of the PC game")]
    public PlayerData player_PC;
    [Space(20)]

    public Timer gameTime;


    public List<Cowboy> cowboys;
    public int cantOfCowboysInGame;
    private int auxCantOfCowboysInGame;

    private UI_PlayerCanvas canvasToUpdate;
    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

//#if UNITY_STANDALONE_WIN
        Application.targetFrameRate = 120;
        player_PC.player.gameObject.SetActive(true);
        player_PC.playerCanvas.gameObject.SetActive(true);
        canvasToUpdate = player_PC.playerCanvas;
        for (int i = 0; i < player_PC.playerElements.Length; i++)
		{
            player_PC.playerElements[i].SetActive(true);
		}
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        for (int i = 0; i < cowboys.Count; i++)
        {
            cowboys[i].SetPlayer(player_PC.player);
        }

/*#endif

#if UNITY_ANDROID
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        player_VR.player.gameObject.SetActive(true);
        player_VR.playerCanvas.gameObject.SetActive(true);
        canvasToUpdate = player_VR.playerCanvas;
        for (int i = 0; i < player_VR.playerElements.Length; i++)
        {
            player_VR.playerElements[i].SetActive(true);

        }

        for (int i = 0; i < cowboys.Count; i++)
        {
            cowboys[i].SetPlayer(player_VR.player);
        }
        
#endif*/

        

        for (int i = 0; i < cantOfCowboysInGame; i++)
        {
            int r = Random.Range(0, cowboys.Count);
            cowboys.Remove(cowboys[r]);
        }

        for (int i = 0; i < cowboys.Count; i++)
        {
            cowboys[i].gameObject.SetActive(false);
        }

        auxCantOfCowboysInGame = cantOfCowboysInGame;
        UpdateRemainingCowboys(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        canvasToUpdate.SetTime(UpdateTime());
    }

    public void FindCowboys()
    {
        cowboys = new List<Cowboy>();
        Cowboy[] c = FindObjectsOfType<Cowboy>();
        for (int i = 0; i < c.Length; i++)
            cowboys.Add(c[i]);
    }

    private string UpdateTime()
    {
        if (gameTime.miliseconds <= 0)
        {
            if (gameTime.seconds <= 0)
            {
                gameTime.minutes--;
                gameTime.seconds = 59;
            }
            else if (gameTime.seconds >= 0)
            {
                gameTime.seconds--;
            }

            gameTime.miliseconds = 100;
        }

        gameTime.miliseconds -= Time.deltaTime * 100;
        if (gameTime.miliseconds < 0)
            gameTime.miliseconds = 0;
        return string.Format("{0}:{1}:{2}", gameTime.minutes.ToString("00"), gameTime.seconds.ToString("00"), ((int)(gameTime.miliseconds)).ToString("00"));
    }

    public void UpdateRemainingCowboys(int cant)
    {
        auxCantOfCowboysInGame-= cant;
        canvasToUpdate.SetRemainingCowboys(auxCantOfCowboysInGame.ToString() + " / " + cantOfCowboysInGame.ToString());
    }
}