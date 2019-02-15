using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour {

    [Header("This varibles it's only used in VR mode")]
    public Image buttonImage;
    public Sprite PauseSprite;
    public Sprite PlaySprite;
    [Header("This varibles it's only used in PC mode")]
    public KeyCode pauseButton;
    private void Start()
    {
        if(buttonImage != null)
        buttonImage.sprite = PauseSprite;


    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(pauseButton))
        {
            Pause();
            if (Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            Cursor.visible = !Cursor.visible;
        }
#endif
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            if (buttonImage != null)
                buttonImage.sprite = PauseSprite;

        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            if (buttonImage != null)
                buttonImage.sprite = PlaySprite;

        }
        
    }

  
}
