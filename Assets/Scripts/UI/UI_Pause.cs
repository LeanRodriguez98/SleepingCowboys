using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour {

    public GameObject pauseButton;
    public Image buttonImage;
    public Sprite PauseSprite;
    public Sprite PlaySprite;
    private void Start()
    {
        buttonImage.sprite = PauseSprite;

    }

    public void Pause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            buttonImage.sprite = PauseSprite;

        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            buttonImage.sprite = PlaySprite;

        }
        
    }

  
}
