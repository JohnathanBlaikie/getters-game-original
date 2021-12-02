using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

/**
TODO:
1. opponent - gorilla
    - prevent shooting while hero is dead
    - faster moving, so opponent doesn't left behind
    - extra items on the floor (upgrade difficulty)
2. opponent - kid
    - shooting - line renderer (from eye to opponent)
    - moving up/down
    - proper animations depending on state


FIXME:
1. jump, when already in the middle of air
2. sides of the screen (visible end of background and foreground)
*/
public class GameManager : MonoBehaviour
{
    public List<GameObject> heroes = new List<GameObject>();
    public GameObject heroesCtr;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject hero;
    public bool isDead = false;
    public int maxLives = 3;
    int lives = 1;

    public int heroNum = 2;
    public GameObject heroPortrait;
    public bool isGameOver = false;

    public List<GameObject> heartsList = new List<GameObject>();

    private Vector3 heroeStartPos = new Vector3(-0.67f, -1.47f, 0f);

    

    // Start is called before the first frame update
    void Awake()
    {
        lives = maxLives;
        isGameOver = false;
        heroNum = GameSettings.heroNum;
    }
    void Start()
    {
        ResetHearts();
        AnimatePortrait();

        // if(!hero)
        // {
            // hero = heroes[0];
            // hero.SetActive(true);
            // hero = Instantiate<GameObject>(heroes[0], heroesCtr.transform);
            // hero.transform.position = heroeStartPos;
        // }

        // virtualCamera = GetComponent<CinemachineVirtualCamera>();
        // virtualCamera.Follow = hero.transform;
        // virtualCamera.LookAt = hero.transform;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void addLife(int _b)
    {
        lives += _b;

        if(_b < 0)
        {
            virtualCamera.Follow = null;
            hero.GetComponent<PlayerMovement>().OnDead();
            hero.GetComponent<CharacterController2D>().Dead();
            
            AnimateHeart();
        }
        if(lives <= 0)
        {
            Debug.Log("GAME OVER");
            isGameOver = true;
        }
    }

    private void AnimateHeart()
    {
        if(heartsList == null || heartsList.Count == 0)
        {
            Debug.LogError("Missing UI hearts in the list");
            return;
        }

        int heartIndex = maxLives - lives;
        GameObject heart = heartsList[heartIndex - 1];

        if(!heart)
        {
            Debug.LogError("Missing single UI heart in the list");
            return;
        }

        heart.GetComponent<Animator>().SetBool("IsDead", true);
        heart.GetComponent<Animator>().SetTrigger("ShowAnim");
    }

    private void ResetHearts()
    {
        Animator heartAnimatorController;
        for(int i = 0; i < heartsList.Count; i++)
        {
            heartAnimatorController = heartsList[i].GetComponent<Animator>();
            heartAnimatorController.SetBool("IsDead", false);
        }
    }

    private void AnimatePortrait()
    {
        heroPortrait.GetComponent<Animator>().SetInteger("HeroNum", heroNum);
        heroPortrait.GetComponent<Animator>().SetTrigger("ChangeAnim");
    }
    public void Restart()
    {
        virtualCamera.Follow = hero.transform;
    }
}
