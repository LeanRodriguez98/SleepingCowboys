using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour {

    public Animator fadeAnimator;
    public Image loadingCircle;
    private AsyncOperation asyncLoading;
	// Use this for initialization
	void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void LoadScene()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }


  

    IEnumerator LoadAsyncScene(string sceneName)
    {
        asyncLoading = SceneManager.LoadSceneAsync(sceneName);
        loadingCircle.gameObject.SetActive(true);
        while (!asyncLoading.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoading.progress / 0.9f);
            loadingCircle.fillAmount = progress;
            yield return new WaitForSeconds(1f);
        }
    }
}
