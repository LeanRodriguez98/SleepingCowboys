using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    public RuntimeAnimatorController fadeAnimationPC;
    public RuntimeAnimatorController fadeAnimationVR;
    public GameObject tutorialImagePC;
    public GameObject tutorialImageVR;


    public GameObject loadingPivot;
    public Image loadingCircle;
    public float loadingCircleSpeed;

    private AsyncOperation asyncLoading;
    private Animator fadeAnimator;


    // Use this for initialization
    void Start()
    {
        fadeAnimator = GetComponent<Animator>();
        SelectAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (loadingPivot.activeSelf)
        {
            loadingPivot.transform.Rotate(0, 0, -loadingCircleSpeed * Time.deltaTime);
        }*/
      
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
        //loadingPivot.SetActive(true);
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
}
    