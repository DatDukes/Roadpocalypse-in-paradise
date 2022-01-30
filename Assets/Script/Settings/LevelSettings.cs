using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{
    [SerializeField]
    public Vector2 _mapSize;

    public int _numberOfCity;
    public int _minSpacingCity;

    public int _numberOfWell;
    public int _minSpaceingWell;

    public int _numberOfObstacle;
    public int _numberOfPowerSources;
    public float _powerSourceDepletionInterval;

    public float _citySpawnInterval;
    public int _citySpawnMinDistance;
    public int _citySpawnMaxDistance;
    public float _citySpawnDistanceIncrement;
}
