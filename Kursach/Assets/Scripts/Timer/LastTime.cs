using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LastTime : MonoBehaviour
{
    [SerializeField] TMP_Text time;
    // Start is called before the first frame update
    void Start()
    {
        int minutes = Mathf.FloorToInt(TimeManager.timer / 60);
        int seconds = Mathf.FloorToInt(TimeManager.timer % 60);
        time.text = string.Format("Last Time Recorded: {0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
