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

    public void AddObject(Vector3 position, GameObject prefab) 
    {
        position = transform.InverseTransformPoint(position);
        Vector2Int mapPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        if (!map.ContainsKey(mapPosition)) 
        {
            MapObject obj = Instantiate(prefab, transform).GetComponent<MapObject>();
            obj.transform.localPosition = new Vector3(mapPosition.x, 0, mapPosition.y);
            map.Add(mapPosition, obj);
        }
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
}
