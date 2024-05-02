using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    private GameObject player;
    private PlayerStats stats;
    private void Start()
    {
        player = GameObject.Find("Player");
        stats = player.GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            stats.DecreaseHealth(100);
        }
    }
}
