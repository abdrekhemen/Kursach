using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordVisual : MonoBehaviour
{
    public TMP_Text RecordText;
    public TMP_Text Username;
    public void Setup(Record record)
    {
        int minutes = Mathf.FloorToInt(CRUD.GetRecord(record.Id).Time / 60);
        int seconds = Mathf.FloorToInt(CRUD.GetRecord(record.Id).Time % 60);
        RecordText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        Username.text = EndScreenManager.user.Login;
    }
}
