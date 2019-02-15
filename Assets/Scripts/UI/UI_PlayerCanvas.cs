using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerCanvas : MonoBehaviour {
    public Text timeText;
    public Text remainingCowboysText;
    public Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        if(animator != null)
            animator.SetTrigger("FadeOut");
    }

    public void SetTime(string _timeText)
    {
        timeText.text = _timeText;
    }

    public void SetRemainingCowboys(string _remainingCowboysText)
    {
        remainingCowboysText.text = _remainingCowboysText;
    }

    public void AnimationGameOver()
    {
        if (animator != null)
            animator.SetTrigger("GameOver");
    }
}
