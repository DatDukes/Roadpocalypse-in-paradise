using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject _endRoundUI;
    public TextMeshProUGUI _timerText;

    public TextMeshProUGUI _scoreP1;
    public TextMeshProUGUI _scoreP2;
    public TextMeshProUGUI _scoreTotal;
    public GameObject _crownP1;
    public GameObject _crownP2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayGameUI()
    {
        _endRoundUI.SetActive(false);
    }

    public void DisplayEndRoundUI(int score1, int score2)
    {
        _endRoundUI.SetActive(true);
        _scoreP1.text = "Score : " + score1;
        _scoreP2.text = "Score : " + score2;

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
