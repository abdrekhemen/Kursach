using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private SpriteRenderer doorRenderer;
    [SerializeField] private bool inside;

    private void Update()
    {
       if (inside && Input.GetKeyUp(KeyCode.E) && doorCollider.enabled==true)
        {
            doorCollider.enabled = false;
            doorRenderer.color = Color.blue;
        }
       else if(inside && Input.GetKeyUp(KeyCode.E) && doorCollider.enabled==false)
        {
            doorCollider.enabled = true;
            doorRenderer.color = Color.red;
        }

    }
    void OnTriggerStay2D(Collider2D other)
    {
        inside = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        inside = false;
    }
}
