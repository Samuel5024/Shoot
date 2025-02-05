using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour
{
    private int _itemsCollected = 0;
    public int Items
    {
        //get returns the value stored in _itemsCollected
        get { return _itemsCollected; }
        
        //set assigns _itemsCollected to the new value of Items whenever updated
        set
        {
            _itemsCollected = value;
            Debug.LogFormat ("Items: {0}", _itemsCollected);
        }
    }

    private int _playerHP = 10;
    public int HP
    {
        //get and set complements the private _playerHP backing varaible
        get { return _playerHP;  }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }
}
