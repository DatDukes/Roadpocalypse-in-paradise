using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class City : MapObject
{
    public int ressources;
    public int baseConsuption;
    public float consuptionFrequency;
    public int player;
    public List<PowerSource> sources;
    public TextMeshProUGUI text;

    private float timer;

    public void UpdatePowerSource(CityConnection connection)
    {
        sources = connection.GetCityConnection(this);
        text.text = ressources.ToString();
    }

    private void Start()
    {
        text.text = ressources.ToString();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > consuptionFrequency) 
        {
            ressources -= baseConsuption - sources.Count;
            text.text = ressources.ToString();
            timer = 0;
            if (ressources <= 0) gameObject.SetActive(false);
        }
    }
}
