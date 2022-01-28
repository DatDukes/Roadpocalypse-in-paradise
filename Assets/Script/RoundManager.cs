using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public float _roundDuration;
    private float remainingDuration;

    public GameObject _endRoundUI;
    public TextMeshProUGUI _timerText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        remainingDuration = _roundDuration;
    }

    private void Update()
    {
        if(remainingDuration <= 0 && _endRoundUI.activeInHierarchy == false)
        {
            print("round over");
            gameManager._levelController.CityConnection.CheckConnection();
            gameManager._uiManager.DisplayEndRoundUI(gameManager._levelController.CityConnection.playerOneScore, gameManager._levelController.CityConnection.playerTwoScore);
            gameManager.ResetPlayers();
        }
        else
        {
            remainingDuration -= Time.deltaTime;
            _timerText.text = "Timer : " + remainingDuration;
        }
    }

    public void OnNextRoundButton()
    {
        remainingDuration = _roundDuration;
        gameManager._uiManager.DisplayGameUI();
        gameManager._levelController._map.ClearMap();
        gameManager._levelController.CreateLevel();
    }
}
