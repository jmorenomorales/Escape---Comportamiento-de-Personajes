using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    #region Singleton

    public static PlayerSingleton instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
}
