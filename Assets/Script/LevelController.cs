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

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateLevel()
    {
        ///Player 1 cities
        for(int i = 0; i < _levelSettings._numberOfCity; i++)
        {
            Vector2 newPos = ReturnRandomPos(_levelSettings._minSpacingCity);
            Vector3 posTranslated = new Vector3(newPos.x, 0, newPos.y);
            _map.AddObject(posTranslated, _cityPrefab);
            
        }

        ///Player 2 cities
        for (int i = 0; i < _levelSettings._numberOfCity; i++)
        {
            Vector2 newPos = ReturnRandomPos(_levelSettings._minSpacingCity);
            Vector3 posTranslated = new Vector3(newPos.x, 0, newPos.y);
            _map.AddObject(posTranslated, _city2Prefab);
        }

        ///Obstacles
        for (int i = 0; i < _levelSettings._numberOfObstacle; i++)
        {
            Vector2 newPos = ReturnRandomPos();

            _map.AddObject(new Vector3(newPos.x, 0, newPos.y), _obstaclePrefab);
        }

        ///Ressources wells
        for(int i = 0; i < _levelSettings._numberOfWell; i++)
        {
            Vector2 newPos = ReturnRandomPos(_levelSettings._minSpaceingWell);

            _map.AddObject(new Vector3(newPos.x, 0, newPos.y), _ressourcePointPrefab);
        }
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
