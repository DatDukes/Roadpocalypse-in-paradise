using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public LevelSettings _levelSettings;

    public Map _map;

    public GameObject _cityPrefab;
    public GameObject _city2Prefab;
    public GameObject _obstaclePrefab;
    public GameObject _ressourcePointPrefab;
    public GameObject _powerSource;

    public CityConnection CityConnection;

    private List<City> allCities = new List<City>();
    private int currentNumberOfWell;

    void Start()
    {
        currentNumberOfWell = _levelSettings._numberOfWell;
        CreateLevel();
    }

    public void CreateLevel()
    {
        CityConnection.Clear();
        allCities.Clear();

        ///Player 1 cities
        for (int i = 0; i < _levelSettings._numberOfCity; i++)
        {
            Vector2 newPos = ReturnRandomPos(_levelSettings._minSpacingCity);
            Vector3 posTranslated = new Vector3(newPos.x, 0, newPos.y);
            //CityConnection.citiesP1.Add(_map.AddObject(posTranslated, _cityPrefab));
            allCities.Add(_map.AddObject(posTranslated, _cityPrefab).GetComponent<City>());
        }

        ///Player 2 cities
        for (int i = 0; i < _levelSettings._numberOfCity; i++)
        {
            Vector2 newPos = ReturnRandomPos(_levelSettings._minSpacingCity);
            Vector3 posTranslated = new Vector3(newPos.x, 0, newPos.y);
            //CityConnection.citiesP2.Add(_map.AddObject(posTranslated, _city2Prefab));
            allCities.Add(_map.AddObject(posTranslated, _city2Prefab).GetComponent<City>());
        }

        ///Obstacles
        for (int i = 0; i < _levelSettings._numberOfObstacle; i++)
        {
            Vector2 newPos = ReturnRandomPos();

            _map.AddObject(new Vector3(newPos.x, 0, newPos.y), _obstaclePrefab);
        }

        ///PowerSources
        for (int i = 0; i < _levelSettings._numberOfPowerSources; i++)
        {
            Vector2 newPos = ReturnRandomPos();

            _map.AddObject(new Vector3(newPos.x, 0, newPos.y), _powerSource);
        }

        ///Ressources wells
        for (int i = 0; i < currentNumberOfWell; i++)
        {
            Vector2 newPos = ReturnRandomPos(_levelSettings._minSpaceingWell);
            _map.AddObject(new Vector3(newPos.x, 0, newPos.y), _ressourcePointPrefab);
        }
    }

    public void RefreshCities() 
    { 
        foreach(City c in allCities)
        {
            c.UpdatePowerSource(CityConnection);
        }
    }

    public bool IsGameOver() 
    {
        return currentNumberOfWell == 0;
    }

    public void ReduceWellCount()
    {
        currentNumberOfWell--;
    }

    public Vector2Int ReturnRandomPos(int aloneRange = 0)
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt( Random.Range(-_levelSettings._mapSize.x / 2, _levelSettings._mapSize.x / 2)), Mathf.RoundToInt( Random.Range(-_levelSettings._mapSize.y / 2, _levelSettings._mapSize.y / 2)));
        if ( _map.IsCellAloneInRange(pos, aloneRange))
            return pos;
        else
        {
            while(_map.IsCellAloneInRange(pos, aloneRange)==false)
            {
                pos = new Vector2Int(Mathf.RoundToInt(Random.Range(-_levelSettings._mapSize.x / 2, _levelSettings._mapSize.x / 2)), Mathf.RoundToInt(Random.Range(-_levelSettings._mapSize.y / 2, _levelSettings._mapSize.y / 2)));
                if (_map.IsCellAloneInRange(pos, aloneRange))
                    return pos;
            }
        }

        return Vector2Int.zero;
    }
}
