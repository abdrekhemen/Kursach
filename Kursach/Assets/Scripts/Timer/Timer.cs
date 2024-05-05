using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    void Update()
    {
        TimeManager.timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(TimeManager.timer / 60);
        int seconds = Mathf.FloorToInt(TimeManager.timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

