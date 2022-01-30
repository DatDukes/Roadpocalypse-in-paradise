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
    public City _mainCity;
    public int _currentPower;

    private List<City> allCities = new List<City>();
    private int currentNumberOfWell;
    private float newSpawnMinDistance;
    private float newSpawnMaxDistance;

    private int powerDepletionValue;

    void Start()
    {
        currentNumberOfWell = _levelSettings._numberOfWell;
        newSpawnMinDistance = _levelSettings._citySpawnMinDistance;
        newSpawnMaxDistance = _levelSettings._citySpawnMaxDistance;
        //CreateLevel();
        StartCoroutine(CityExpensionCouroutine());
        StartCoroutine(PowerCouroutine());
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
        return _currentPower <= 0;
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

    public IEnumerator CityExpensionCouroutine()
    {
        _mainCity = _map.AddObject(Vector3.zero, _cityPrefab).GetComponent<City>();
        allCities.Add(_mainCity);

        ///Ressources wells
        for (int i = 0; i < currentNumberOfWell; i++)
        {
            Vector2 newPos = ReturnRandomPos(_levelSettings._minSpaceingWell);
            _map.AddObject(new Vector3(newPos.x, 0, newPos.y), _ressourcePointPrefab);
        }

        while (true)
        {
            yield return new WaitForSeconds(_levelSettings._citySpawnInterval);
            ///New city spawn logic
            Vector2 newPos = Random.insideUnitCircle.normalized * Random.Range(newSpawnMinDistance, newSpawnMaxDistance); //new Vector2(Random.Range(newSpawnMinDistance, newSpawnMaxDistance), Random.Range(newSpawnMinDistance, newSpawnMaxDistance));
            while (_map.IsCellEmpty(newPos) == false)
            {
                newPos = new Vector2(Random.Range(newSpawnMinDistance, newSpawnMaxDistance), Random.Range(newSpawnMinDistance, newSpawnMaxDistance));
            }
            
            Vector3 posTranslated = new Vector3(newPos.x, 0, newPos.y);
            //CityConnection.citiesP1.Add(_map.AddObject(posTranslated, _cityPrefab));
            allCities.Add(_map.AddObject(posTranslated, _cityPrefab).GetComponent<City>());

            newSpawnMinDistance += _levelSettings._citySpawnDistanceIncrement;
            newSpawnMaxDistance += _levelSettings._citySpawnDistanceIncrement;

            ///New power source spawn logic
            MapObject selectedCity = allCities[Random.Range(0, allCities.Count)];

            newPos = Random.insideUnitCircle.normalized * Random.Range(2, 4);
            newPos += new Vector2(selectedCity.transform.position.x, selectedCity.transform.position.z);
            while (_map.IsCellEmpty(newPos) == false)
            {
                newPos = Random.insideUnitCircle.normalized * Random.Range(2, 4);
                newPos += new Vector2(selectedCity.transform.position.x, selectedCity.transform.position.z);
            }

            _map.AddObject(new Vector3(newPos.x, 0, newPos.y), _powerSource);

            powerDepletionValue++;
        }
    }

    private IEnumerator PowerCouroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_levelSettings._powerSourceDepletionInterval);

            _currentPower += _mainCity.sources.Count;

            _currentPower -= powerDepletionValue;


            GameManager.Instance._uiManager.RefreshCurrentPowerDisplay(_currentPower);
        }
    }
}
