using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectHeroManager : MonoBehaviour
{
    private void SelectHero(int _heroNum)
    {
        GameSettings.SetHeroNum(_heroNum);
        SceneManager.LoadSceneAsync("SampleScene");
        SceneManager.UnloadSceneAsync("HeroSelect");
        
    }
    public void SelectHero1()
    {
        SelectHero(1);
    }

    public void SelectHero2()
    {
        SelectHero(2);
    }

    public void SelectHero3()
    {
        SelectHero(3);
    }
}
