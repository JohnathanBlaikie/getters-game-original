using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    public static int heroNum = 2;

    // void Awake()
    // {
    //     DontDestroyOnLoad(transform.gameObject);
    // }
    public static void SetHeroNum(int _heroNum = 1)
    {
        heroNum = _heroNum;
        Debug.Log("Select hero: " + heroNum);
    }
}
