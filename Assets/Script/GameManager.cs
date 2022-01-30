using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelController _levelController;
    public RoundManager _roundManager;
    public UIManager _uiManager;
    public CityConnection _cityConnection;

    public Player _player1;
    public Player _player2;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  ResetPlayers()
    {
        _player1.RemoveRessource(_player1._currentResources);
        _player2.RemoveRessource(_player2._currentResources);
    }
}
