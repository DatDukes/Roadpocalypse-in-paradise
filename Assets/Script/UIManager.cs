using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject _endRoundUI;
    public TextMeshProUGUI _timerText;
    public TextMeshProUGUI _currentPowerTest;

    public GameObject Win, Lose, Score, Button, Timer;
    public TextMeshProUGUI _scoreP1;
    public TextMeshProUGUI _scoreP2;
    public TextMeshProUGUI _roundP1;
    public TextMeshProUGUI _roundP2;
    public TextMeshProUGUI _scoreTotal;
    public GameObject _crownP1;
    public GameObject _crownP2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RefreshCurrentPowerDisplay(int value)
    {
        _currentPowerTest.text = "Power remaining = " + value;
    }

    public void DisplayGameUI()
    {
        _endRoundUI.SetActive(false);
    }

    public void DisplayEndRoundUI(int score1, int score2, int roundScore1, int roundScore2, bool End, bool win)
    {
        if (End) 
        {
            if (win)
            {
                Win.SetActive(true);
            }
            else
            {
                Lose.SetActive(true);
                Score.SetActive(false);
            }
            Button.SetActive(false);
            Timer.SetActive(false);
        }
        else 
        {
            Score.SetActive(true);
            Button.SetActive(true);
            Timer.SetActive(true);
        }

        _endRoundUI.SetActive(true);
        _scoreP1.text = "City connected : " + score1;
        _scoreP2.text = "City connected : " + score2;
        _roundP1.text = "Round Won : " + roundScore1;
        _roundP2.text = "Round Won : " + roundScore2;

        _scoreTotal.text = (score1 + score2).ToString();

        if(score1 > score2)
        {
            _crownP1.SetActive(true);
            _crownP2.SetActive(false);
        }
        else if (score1 < score2)
        {
            _crownP1.SetActive(false);
            _crownP2.SetActive(true);
        }
        else
        {
            _crownP1.SetActive(true);
            _crownP2.SetActive(true);
        }
    }

}
