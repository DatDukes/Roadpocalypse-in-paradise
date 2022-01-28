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
        if(remainingDuration <= 0)
        {
            print("round over");
            gameManager._uiManager.DisplayEndRoundUI();
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