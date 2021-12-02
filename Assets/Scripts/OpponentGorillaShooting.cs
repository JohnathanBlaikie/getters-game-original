using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentGorillaShooting : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject idlePoint;
    public GameObject bullet;

    GameManager gameManager;
    private GameObject hero;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float minDistanceToHero = 1.0f;
    [SerializeField] private float maxDistanceToHero = 5f;

    [SerializeField] private float chaseHeroStart = 150f;
    [SerializeField] private float chaseHeroDist = 130f;

    public GameObject armObject;

    private Animator armAnimator;
    private Animator bulletAnimator;
    private bool isDistanceToHeroOk = false;

    private GameObject currentBullet; 
    private OpponentGorillaBullet bulletScript;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        hero = gameManager.hero;

        armAnimator = armObject.GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        isDistanceToHeroOk = false;
        

        InvokeRepeating("CreateBullet", 2, 3);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetButtonDown("Fire1"))
        // {
        //     CreateBullet();
        // }
    }

    void FixedUpdate()
    {
        if(currentBullet)
        {
            if(bulletScript)
            {
                if(bulletScript.shootState == BulletState.IDLE)
                {
                    currentBullet.transform.position = idlePoint.transform.position;    
                }
                else if(bulletScript.shootState == BulletState.AIMING)
                {
                    currentBullet.transform.position = firePoint.transform.position;
                }
            }
        }

        float currentDistanceToHero = hero.transform.position.x - transform.position.x;

        if(hero.transform.position.x < chaseHeroStart || hero.transform.position.x > (chaseHeroStart + chaseHeroDist))
        {
            isDistanceToHeroOk = false;
            return;
        }
        
        isDistanceToHeroOk = true;

        if(currentDistanceToHero < minDistanceToHero)
        {
            transform.position = new Vector3( hero.transform.position.x - minDistanceToHero, transform.position.y, transform.position.z);    
        }
        else if(currentDistanceToHero > maxDistanceToHero)
        {
            transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        }
    }

    void CreateBullet()
    {
        if(currentBullet != null)
            return;

        currentBullet = Instantiate(bullet, idlePoint.transform.position, idlePoint.transform.rotation);
        bulletAnimator = currentBullet.GetComponent<Animator>();
        bulletScript = currentBullet.GetComponent<OpponentGorillaBullet>();

        Invoke("Aim", 1);
    }
    void Aim()
    {
        if(currentBullet == null)
            return;

        bulletScript.shootState = BulletState.AIMING;

        // if(!isDistanceToHeroOk)
        //     return;

        currentBullet.transform.position = firePoint.transform.position;

        armAnimator.SetTrigger("Shoot");
        bulletAnimator.SetTrigger("Shoot"); 
    }

    public void ShootBullet()
    {
        if(currentBullet == null)
            return;

        if(bulletScript == null)
            return;

        bulletScript.shootState = BulletState.SHOOTING;
        bulletScript.Shoot();

        currentBullet = null;
    }

}
