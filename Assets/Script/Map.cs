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
        if (map.ContainsKey(mapPosition)) 
        {
            MapObject obj = Instantiate(prefab, transform).GetComponent<MapObject>();
            obj.transform.localPosition = new Vector3(mapPosition.x, 0, mapPosition.y);
            map.Add(mapPosition, obj);
        }
    }
}
