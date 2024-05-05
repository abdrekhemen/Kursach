using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private bool inside;
    public bool isKeyPicked;
    public bool isKeyOnLevel;

    private GameObject player;
    private GameObject key;

    private void Start()
    {
        player = GameObject.Find("Player");
        key = GameObject.Find("Key");
        if(key != null) 
        {
            isKeyOnLevel = true;
        }
        else
        {
            isKeyOnLevel = false;
        }
    }
    private void Update()
    {
        if (inside && Input.GetKeyUp(KeyCode.E) && !isKeyOnLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(inside && Input.GetKeyUp(KeyCode.E) && isKeyOnLevel && isKeyPicked)
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
