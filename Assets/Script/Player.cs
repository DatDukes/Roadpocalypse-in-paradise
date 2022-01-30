using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Index;
    public float Speed;
    public float ToqueSpeed;
    public GameObject ObjectToSpawn;
    public Map Map;
    public LevelController Level;

    public int _maxRessources;
    public int _currentResources 
    {
        get    
        {
            return ressources;
        }

        set 
        {
            ressources = value;
            for(int i = 0; i < Boxes.Length; i++) 
            {
                Boxes[i].SetActive(i < ressources);
            }
        }
    }
    public TextMeshPro _resourceText;
    public GameObject[] Boxes;

    private string horizontal, vertical, action, gather;
    private new Rigidbody rigidbody;

    private bool wellAvailable;
    private ResourcesWell connectedWell;
    private int ressources;

    void Start()
    {
        horizontal = "HorizontalP" + Index;
        vertical = "VerticalP" + Index;
        action = "ActionP" + Index;
        gather = "GatherP" + Index;
        rigidbody = GetComponent<Rigidbody>();
        _currentResources = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown(action) && _currentResources > 0) 
        {
            AddRoad();
            Level.RefreshCities();
        }

        if (Input.GetButtonDown(gather) && wellAvailable)
        {
            if(GetRessource())
                connectedWell.ModifyRessourceCount(-1);
        }
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis(horizontal), 0, Input.GetAxis(vertical));

        if(move.magnitude > 0) 
        {
            rigidbody.AddForce(transform.forward * Speed, ForceMode.VelocityChange);
            move = move.normalized;
            float dir = Mathf.Sign(Vector3.Dot(move, transform.right));
            float rot = Vector3.Dot(move, transform.forward) * -0.5f + 0.5f;

            rigidbody.AddTorque(Vector3.up * rot * dir * ToqueSpeed, ForceMode.Impulse);
        }
    }

    private void AddRoad()
    {
        if(Map.IsCellEmpty(transform.position))
        {
            Map.AddObject(transform.position, ObjectToSpawn);
            RemoveRessource(1);
        }
        
    }

    public bool RemoveRessource(int value)
    {
        if (_currentResources > 0)
        {
            _currentResources -= value ;
            _resourceText.text = _currentResources.ToString();
            return true;
        }
        return false;
    }

    public bool GetRessource()
    {
        if(_currentResources < _maxRessources)
        {
            _currentResources++;
            _resourceText.text = _currentResources.ToString();
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Well")
        {
            connectedWell = other.GetComponent<ResourcesWell>();
            wellAvailable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Well")
        {
            wellAvailable = false;
        }
    }

}
