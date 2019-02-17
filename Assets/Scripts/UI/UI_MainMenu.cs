using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [Header("Fade animatior")]
    public Animator fadeAnimator;
    [Space(10)]
    [Header("Fade animations of:")]
    [Header("   PC")]
    public RuntimeAnimatorController fadeAnimationPC;
    [Header("   VR")]
    public RuntimeAnimatorController fadeAnimationVR;
    [Header("Tutorial Image of:")]
    [Header("   PC")]
    public GameObject tutorialImagePC;
    [Header("   VR")]
    public GameObject tutorialImageVR;





    private AsyncOperation asyncLoading;


    // Use this for initialization
    private void Awake()
    {
#if UNITY_ANDROID
        UnityEngine.XR.XRSettings.enabled = false;
#endif
    }

    void Start()
    {
        fadeAnimator = GetComponent<Animator>();
        SelectAnimation();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
    
      
    }

    public void AllowSceneActivationTrue()
    {
        asyncLoading.allowSceneActivation = true;
    }

    public void LoadScene()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    public void SelectAnimation()
    {
#if UNITY_STANDALONE_WIN
        fadeAnimator.runtimeAnimatorController = fadeAnimationPC;
#endif
#if UNITY_ANDROID
        fadeAnimator.runtimeAnimatorController = fadeAnimationVR;
#endif

    }


    IEnumerator LoadAsyncScene(string sceneName)
    {
        asyncLoading = SceneManager.LoadSceneAsync(sceneName);
        asyncLoading.allowSceneActivation = false;
        yield return new WaitForSeconds(1.0f);
    }

    public void EnableGameObject(GameObject go)
    {
        go.SetActive(true);
    }

    public void DisableGameObject(GameObject go)
    {
        go.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

}
    