using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityConnection : MonoBehaviour
{
    public Map map;
    public List<MapObject> cities;
    public int playerOneScore, playerTwoScore;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            CheckConnection();
        }
    }

    public void CheckConnection() 
    {
        //Dictionary<Vector2Int, MapObject> _map = new Dictionary<Vector2Int, MapObject>(map.map);

        for(int i = 0; i < cities.Count; i++) 
        {
            Dictionary<Vector2Int, MapObject> _map = new Dictionary<Vector2Int, MapObject>(map.map);
            List<ConnectionNode> nodes = new List<ConnectionNode>();
            nodes.Add(new ConnectionNode(cities[i], cities[i].mapPosition));
            ProcessNodeList(ref _map, nodes);
        }
    }

    public void ProcessNodeList(ref Dictionary<Vector2Int, MapObject> map, List<ConnectionNode> nodes) 
    {
        List<ConnectionNode> newList = new List<ConnectionNode>();

        foreach(ConnectionNode node in nodes) 
        {
            ProcessNode(ref map, ref newList, node);
        }

        if (newList.Count > 0) ProcessNodeList(ref map, newList);
    }

    public void ProcessNode(ref Dictionary<Vector2Int, MapObject> map, ref List<ConnectionNode> nodes, ConnectionNode node) 
    { 
        for(int i = 0; i < 4; i++) 
        {
            Vector2Int pos = node.position;

            switch (i) 
            {
                case 0:
                    pos.y += 1;
                    break;
                case 1:
                    pos.x += 1;
                    break;
                case 2:
                    pos.y -= 1;
                    break;
                case 3:
                    pos.x -= 1;
                    break;
            }


            if(map.TryGetValue(pos, out MapObject obj))
            {
                switch (obj.type)
                {
                    case ObjectType.City:
                        if (node.city != obj)
                        {
                            Debug.Log("Connection Found");
                            City OriginCity = node.city.GetComponent<City>();
                            City city = obj.GetComponent<City>();

                            if (city.player == 1) playerOneScore++;
                            else playerTwoScore++;

                            if (OriginCity.player == 1) playerOneScore++;
                            else playerTwoScore++;
                        }
                        break;
                    default:
                        map.Remove(pos);
                        nodes.Add(new ConnectionNode(node.city, pos));
                        break;
                }
            }
        }
    }
}

public class ConnectionNode
{
    public ConnectionNode (MapObject city, Vector2Int position) 
    {
        this.city = city;
        this.position = position;
    }
    public MapObject city;
    public Vector2Int position;
}

