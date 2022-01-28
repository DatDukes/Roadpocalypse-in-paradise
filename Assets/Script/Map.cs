using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Dictionary<Vector2Int, MapObject> map;

    private void Start()
    {
        map = new Dictionary<Vector2Int, MapObject>();
    }

    public MapObject AddObject(Vector3 position, GameObject prefab) 
    {
        position = transform.InverseTransformPoint(position);
        Vector2Int mapPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        if (!map.ContainsKey(mapPosition)) 
        {
            MapObject obj = Instantiate(prefab, transform).GetComponent<MapObject>();
            obj.map = this;
            obj.mapPosition = mapPosition;
            obj.transform.localPosition = new Vector3(mapPosition.x, 0, mapPosition.y);
            map.Add(mapPosition, obj);
            obj.InitTile();
            return obj;
        }
        return null;
    }

    public bool IsCellEmpty(int x, int y) 
    {
        return IsCellEmpty(new Vector2Int(x, y));
    }

    public bool IsCellEmpty(Vector3 pos)
    {
        return IsCellEmpty(new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z)));
    }

    public bool IsCellEmpty(Vector2Int cell)
    {
        if(map.ContainsKey(cell) == true)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public MapObject GetMapObject(int x, int y, out bool found)
    {
        return GetMapObject(new Vector2Int(x, y), out found);
    }

    public MapObject GetMapObject(Vector2Int cell, out bool found)
    {
        MapObject obj = null;
        found = map.TryGetValue(cell, out obj);
        return obj;
    }

    public bool IsCellAloneInRange(Vector2Int cell, int range)
    {
        for(int x = -range; x < range; x++)
        {
            for(int y = -range; y < range; y++)
            {
                Vector2Int posToCheck = new Vector2Int(cell.x + x, cell.y+ y);
                if(IsCellEmpty(posToCheck) == false)
                {
                    return false;
                }
            }
        }

        return true;
    }


    public void ClearMap()
    {
        foreach(KeyValuePair<Vector2Int, MapObject> i in map)
        {
            Destroy(i.Value.gameObject);
        }

        map = new Dictionary<Vector2Int, MapObject>();
    }

}
