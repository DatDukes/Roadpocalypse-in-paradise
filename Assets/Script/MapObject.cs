using UnityEngine;

public class MapObject : MonoBehaviour
{
    public Map map;
    public Vector2Int mapPosition;
    public ObjectType type;

    public virtual void InitTile()
    {

    }

    public virtual void UpdateTile() 
    { 
        
    }
}

public enum ObjectType 
{ 
    City,
    Road,
    Power,
    Other
}
