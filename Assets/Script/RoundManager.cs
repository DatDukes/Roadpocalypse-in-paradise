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

    private void Start()
    {
        remainingDuration = _roundDuration;
    }

    private void Update()
    {
        if(remainingDuration <= 0)
        {
            print("round over");
            _endRoundUI.SetActive(true);
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
        _endRoundUI.SetActive(false);
        GameManager.Instance._levelController._map.ClearMap();
        GameManager.Instance._levelController.CreateLevel();
    }
}
