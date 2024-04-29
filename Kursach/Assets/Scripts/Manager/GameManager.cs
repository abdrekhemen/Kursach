using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private float respawnTime;

    private float respawnTimeStart;

    private bool respawn;

    private CameraFollow CF;

    private void Start()
    {
        CF = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }
    private void Update()
    {
        CheckRespawn();
    }
    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= respawnTimeStart+respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CF.PlayerFocus();
            respawn = false;
        }
    }
}
