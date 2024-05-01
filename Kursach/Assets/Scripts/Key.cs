using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isPicked;
    private Door door;

    private void Start()
    {
        door = GameObject.Find("ExitDoor").GetComponent<Door>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        door.isKeyPicked = true;
        Destroy(gameObject);
    }
}
