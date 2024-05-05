using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenManager : MonoBehaviour
{
    public static EndScreenManager Instance;

    public static User user;

    public Transform RecordTransformParent;
    public GameObject RecordSlotPrefab;

    private List<Record> records;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
        if (user != null) 
        {
            Record record = new Record()
            {
                Time = TimeManager.timer
            };
            record.Login = user.Login;
            CRUD.CreateRecord(record); 
        }
        ShowSaves();
    }

    public void ShowSaves()
    {
        if (records == null)
        {
            records = CRUD.GetAllRecords(user);
            foreach (var record in records)
            {
                GameObject recordObject = Instantiate(RecordSlotPrefab, RecordTransformParent);
                recordObject.GetComponent<RecordVisual>().Setup(record);
            }
        }
    }
}
