using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject _endRoundUI;
    public TextMeshProUGUI _timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayGameUI()
    {
        _endRoundUI.SetActive(false);
    }

    public void DisplayEndRoundUI()
    {
        _endRoundUI.SetActive(true);
    }
}
