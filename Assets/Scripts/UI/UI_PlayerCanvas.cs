using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerCanvas : MonoBehaviour {
    public Text timeText;
    public Text remainingCowboysText;

    public void SetTime(string _timeText)
    {
        timeText.text = _timeText;
    }

    public void SetRemainingCowboys(string _remainingCowboysText)
    {
        remainingCowboysText.text = _remainingCowboysText;
    }
}
