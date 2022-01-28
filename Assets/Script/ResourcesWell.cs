using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesWell : MapObject
{
    public int _maxRessources;
    public int _remainingRessources;

    public TextMeshPro _text;

    public void Start()
    {
        ModifyRessourceCount(_maxRessources);
    }

    /// <summary>
    /// Add or remove ressource from de remaining ressources
    /// </summary>
    /// <param name="value"></param>
    public void ModifyRessourceCount(int value)
    {
        _remainingRessources += value;
        _text.text = _remainingRessources.ToString();
        if(_remainingRessources == 0)
        {
            GameManager.Instance._levelController.ReduceWellCount();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && _remainingRessources > 0)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player._currentResources < player._maxRessources)
            {
                ModifyRessourceCount(-1);
                player.GetRessource();
            }
            
        }
    }
}
