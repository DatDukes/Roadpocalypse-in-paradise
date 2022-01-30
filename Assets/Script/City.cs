using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MapObject
{
    public int player;
    public List<PowerSource> sources;

    public void UpdatePowerSource(CityConnection connection)
    {
        sources = connection.GetCityConnection(this);
    }
}
