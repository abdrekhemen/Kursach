using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private bool inside;
    public bool isKeyPicked;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (inside && Input.GetKeyUp(KeyCode.E) && isKeyPicked)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            inside = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject==player)
        {
            inside = false;
        }  
    }
}
