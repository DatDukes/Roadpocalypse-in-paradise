using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MapObject
{
    public GameObject currentMesh;
    public GameObject IRoad, LRoad, TRoad, XRoad;

    public override void InitTile()
    {
        UpdateMesh(true);
    }

    public override void UpdateTile()
    {
        UpdateMesh();
    }

    void UpdateMesh(bool refreshNeighbor = false)
    {
        bool[] neighbors = new bool[4];
        List<MapObject> objs = new List<MapObject>();
        objs.Add(map.GetMapObject(mapPosition.x + 1, mapPosition.y, out neighbors[0]));
        objs.Add(map.GetMapObject(mapPosition.x, mapPosition.y - 1, out neighbors[1]));
        objs.Add(map.GetMapObject(mapPosition.x - 1, mapPosition.y, out neighbors[2]));
        objs.Add(map.GetMapObject(mapPosition.x, mapPosition.y + 1, out neighbors[3]));

        int neighborsCount = (neighbors[0] ? 1 : 0) + (neighbors[1] ? 1 : 0) + (neighbors[2] ? 1 : 0) + (neighbors[3] ? 1 : 0);
        GameObject road = IRoad;
        float rotation = 0;

        switch (neighborsCount) 
        {
            default:
                road = IRoad;
                break;

            case 1:
                if (neighbors[0] || neighbors[2])
                {
                    road = IRoad;
                    rotation = 90;
                }
                else 
                {
                    road = IRoad;
                    rotation = 0;
                }
                break;

            case 2:
                if(neighbors[0] && neighbors[2]) 
                {
                    road = IRoad;
                    rotation = 90;
                }
                else if (neighbors[1] && neighbors[3]) 
                {
                    road = IRoad;
                    rotation = 0;
                }
                else if (neighbors[3] && neighbors[0])
                {
                    road = LRoad;
                    rotation = 0;
                }
                else if (neighbors[0] && neighbors[1])
                {
                    road = LRoad;
                    rotation = 90;
                }
                else if (neighbors[1] && neighbors[2])
                {
                    road = LRoad;
                    rotation = 180;
                }
                else if (neighbors[2] && neighbors[3])
                {
                    road = LRoad;
                    rotation = 270;
                }
                break;

            case 3:
                if (!neighbors[3])
                {
                    road = TRoad;
                    rotation = 0;
                }
                else if (!neighbors[0])
                {
                    road = TRoad;
                    rotation = 90;
                }
                else if (!neighbors[1])
                {
                    road = TRoad;
                    rotation = 180;
                }
                else if (!neighbors[2])
                {
                    road = TRoad;
                    rotation = 270;
                }
                break;

            case 4:
                road = XRoad;
                break;
        }

        if(currentMesh != null) Destroy(currentMesh);
        currentMesh = Instantiate(road, transform);
        currentMesh.transform.localRotation = Quaternion.Euler(0, rotation, 0);

        if (refreshNeighbor) 
        {
            foreach (MapObject o in objs)
            {
                o?.UpdateTile();
            }
        }
    }
}
