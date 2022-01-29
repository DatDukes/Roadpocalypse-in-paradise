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
    public int RoundWonP1, RoundWonP2;
    public int RoundCount;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        remainingDuration = _roundDuration;
    }

    private void Update()
    {
        if (remainingDuration <= 0 && _endRoundUI.activeInHierarchy == false)
        {
            print("round over");
            gameManager._levelController.CityConnection.CheckConnection();
            if (gameManager._levelController.CityConnection.playerOneScore == gameManager._levelController.CityConnection.playerTwoScore)
            {
                RoundWonP1++;
                RoundWonP2++;
            }
            else if (gameManager._levelController.CityConnection.playerOneScore > gameManager._levelController.CityConnection.playerTwoScore)
            {
                RoundWonP1++;
            }
            else
            {
                RoundWonP2++;
            }
            RoundCount++;
            bool b = gameManager._levelController.IsGameOver();
            gameManager._uiManager.DisplayEndRoundUI(gameManager._levelController.CityConnection.playerOneScore, gameManager._levelController.CityConnection.playerTwoScore, RoundWonP1, RoundWonP2, RoundCount > 10 || b, !b);
            gameManager.ResetPlayers();

            _timerText.text = "Timer : 0";
        }
        else if (remainingDuration > 0)
        {
            remainingDuration -= Time.deltaTime;
            _timerText.text = "Timer : " + (int)remainingDuration;
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
