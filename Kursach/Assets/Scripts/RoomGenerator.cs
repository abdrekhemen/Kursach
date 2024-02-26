using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomGenerator : MonoBehaviour
{
    public Direction direction;
    public enum Direction
    {
        TopRight,
        TopLeft,
        TopAny,
        Right,
        Left,
        Any
    }

    private RoomTemplates templates;
    private int rand;
    private bool isSpawned = false;
    private int roomCounts;
    public float waitTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        roomCounts = GameObject.FindGameObjectsWithTag("Room").Length;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }
    
    // Update is called once per frame
    void Spawn()
    {   
        if (isSpawned==false)
        { 
            if(direction==Direction.TopRight)
            {
                if(roomCounts < 10)
                    rand = Random.Range(2, templates.topRightRooms.Length);
                else
                    rand = Random.Range(0, templates.topRightRooms.Length);
                 
                 Instantiate(templates.topRightRooms[rand],transform.position, templates.topRightRooms[rand].transform.rotation);
            }
            else if(direction == Direction.TopLeft)
            {
                if(roomCounts<10)
                    rand = Random.Range(2, templates.topLeftRooms.Length);
                else
                    rand = Random.Range(0, templates.topLeftRooms.Length);
                Instantiate(templates.topLeftRooms[rand], transform.position, templates.topLeftRooms[rand].transform.rotation);
            }
            else if (direction == Direction.TopAny)
            {
                if (roomCounts < 10)
                {
                    rand = Random.Range(2, templates.topAnyRooms.Length);
                }
                else
                    rand = Random.Range(0, templates.topAnyRooms.Length);
                Instantiate(templates.topAnyRooms[rand], transform.position, templates.topAnyRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                if (roomCounts < 10)
                {
                    rand = Random.Range(1, templates.leftRooms.Length);
                }
                else
                    rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                if (roomCounts < 10)
                {
                    rand = Random.Range(1, templates.rightRooms.Length);
                }
                else
                    rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            isSpawned = true;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint") && collision.GetComponent<RoomGenerator>().isSpawned==true)
        {
            Destroy(gameObject);
        }
    }
}
