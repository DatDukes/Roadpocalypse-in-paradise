using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Index;
    public float Speed;
    public GameObject ObjectToSpawn;
    public Map Map;
    private string horizontal, vertical, action;

    void Start()
    {
        horizontal = "HorizontalP" + Index;
        vertical = "VerticalP" + Index;
        action = "Fire1";
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical));
        transform.position += move.normalized * Time.deltaTime * Speed;

        if (Input.GetButtonDown(action)) 
        {
            Map.AddObject(transform.position, ObjectToSpawn);
        }
    }
}
