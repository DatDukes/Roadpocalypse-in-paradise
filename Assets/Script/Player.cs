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

    public int _maxRessources;
    public int _currentResources;
    public TextMeshPro _resourceText;

    private string horizontal, vertical, action;
    private new Rigidbody rigidbody;

    void Start()
    {
        horizontal = "HorizontalP" + Index;
        vertical = "VerticalP" + Index;
        action = "ActionP" + Index;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown(action) && _currentResources > 0) 
        {
            AddRoad();
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

    public void RemoveRessource(int value)
    {
        if (_currentResources > 0)
        {
            _currentResources -= value ;
            _resourceText.text = _currentResources.ToString();
        }
    }

    public void GetRessource()
    {
        if(_currentResources < _maxRessources)
        {
            _currentResources++;
            _resourceText.text = _currentResources.ToString();
        }
    }
}
