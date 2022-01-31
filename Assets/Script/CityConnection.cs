using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CityConnection : MonoBehaviour
{
    public Map map;
    public List<MapObject> citiesP1, citiesP2;
    public List<Connection> ConnectionsP1, ConnectionsP2;
    public int playerOneScore { get { return ConnectionsP1.Count; } }
    public int playerTwoScore { get { return ConnectionsP2.Count; } }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            CheckConnection();
        }
    }

    public void Clear() 
    {
        citiesP1.Clear();
        citiesP2.Clear();
    }

    public List<PowerSource> GetCityConnection(MapObject City) 
    {
        Dictionary<Vector2Int, MapObject> _map = new Dictionary<Vector2Int, MapObject>(map.map);
        List<PowerSource> sources = new List<PowerSource>();
        List<ConnectionNode> nodes = new List<ConnectionNode>() { new ConnectionNode(new List<MapObject>() { City }, City.mapPosition) };
        ProcessNodeList(ref _map, nodes, sources, 1);
        return sources;
    }

    public void CheckConnection() 
    {
        Dictionary<Vector2Int, MapObject> _map = new Dictionary<Vector2Int, MapObject>(map.map);
        ConnectionsP1 = new List<Connection>();

        for (int i = 0; i < citiesP1.Count; i++) 
        {
            _map = new Dictionary<Vector2Int, MapObject>(map.map);
            List<ConnectionNode> nodes = new List<ConnectionNode>();
            nodes.Add(new ConnectionNode(new List<MapObject>() { citiesP1[i] }, citiesP1[i].mapPosition));
            ProcessNodeList(ref _map, nodes, ConnectionsP1, 1);
        }

        _map = new Dictionary<Vector2Int, MapObject>(map.map);
        ConnectionsP2 = new List<Connection>();

        for (int i = 0; i < citiesP2.Count; i++)
        {
            _map = new Dictionary<Vector2Int, MapObject>(map.map);
            List<ConnectionNode> nodes = new List<ConnectionNode>();
            nodes.Add(new ConnectionNode(new List<MapObject>() { citiesP2[i] }, citiesP2[i].mapPosition));
            ProcessNodeList(ref _map, nodes, ConnectionsP2, 2);
        }
    }

    public void ProcessNodeList(ref Dictionary<Vector2Int, MapObject> map, List<ConnectionNode> nodes, List<Connection> Connections, int player) 
    {
        List<ConnectionNode> newList = new List<ConnectionNode>();

        foreach(ConnectionNode node in nodes) 
        {
            ProcessNode(ref map, ref newList, node, Connections, player);
        }

        if (newList.Count > 0) ProcessNodeList(ref map, newList, Connections, player);
    }

    public void ProcessNodeList(ref Dictionary<Vector2Int, MapObject> map, List<ConnectionNode> nodes, List<PowerSource> Connections, int player)
    {
        List<ConnectionNode> newList = new List<ConnectionNode>();

        foreach (ConnectionNode node in nodes)
        {
            ProcessNode(ref map, ref newList, node, Connections, player);
        }

        if (newList.Count > 0) ProcessNodeList(ref map, newList, Connections, player);
    }

    public void ProcessNode(ref Dictionary<Vector2Int, MapObject> map, ref List<ConnectionNode> nodes, ConnectionNode node, List<Connection> Connections, int player) 
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
                        List<MapObject> cityToAdd = new List<MapObject>();
                        foreach(MapObject city in node.cities) 
                        {
                            if (city != obj)
                            {
                                City c = obj.GetComponent<City>();
                                if (c.player == player)
                                {
                                    Connection co = new Connection(city, obj);
                                    if (!Connections.Any(c => c.Compare(co)))
                                    {
                                        Connections.Add(co);
                                    }
                                    cityToAdd.Add(obj);
                                }
                            }
                        }
                        node.cities.Concat(cityToAdd);
                        nodes.Add(new ConnectionNode(node.cities, pos));
                        break;
                    case ObjectType.Road :
                    case ObjectType.Power :
                        map.Remove(pos);
                        nodes.Add(new ConnectionNode(node.cities, pos));
                        break;
                }
            }
        }
    }

    public void ProcessNode(ref Dictionary<Vector2Int, MapObject> map, ref List<ConnectionNode> nodes, ConnectionNode node, List<PowerSource> Connections, int player)
    {
        for (int i = 0; i < 4; i++)
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


            if (map.TryGetValue(pos, out MapObject obj))
            {
                switch (obj.type)
                {
                    case ObjectType.Power:
                        Connections.Add(obj.GetComponent<PowerSource>());
                        map.Remove(pos);
                        nodes.Add(new ConnectionNode(node.cities, pos));
                        break;
                    default:
                        map.Remove(pos);
                        nodes.Add(new ConnectionNode(node.cities, pos));
                        break;
                }
            }
        }
    }
}

[Serializable]
public class Connection 
{ 
    public Connection(MapObject A, MapObject B) 
    {
        this.A = A;
        this.B = B;
    }

    public bool Compare(Connection connection) 
    {
        return (this.A == connection.A && this.B == connection.B) ||
               (this.A == connection.B && this.B == connection.A); 
    }

    public MapObject A;
    public MapObject B;
}

public class ConnectionNode
{
    public ConnectionNode (List<MapObject> cities, Vector2Int position) 
    {
        this.cities = cities;
        this.position = position;
    }
    public List<MapObject> cities;
    public Vector2Int position;
}

