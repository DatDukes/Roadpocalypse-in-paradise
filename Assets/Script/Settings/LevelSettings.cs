using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{
    [SerializeField]
    public Vector2 _mapSize;

    public int _numberOfCity;
    public int _minSpacingDistance;
    public int _numberOfObstacle;
}
