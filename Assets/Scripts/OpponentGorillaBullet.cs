using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletState
{
    IDLE,
    AIMING,
    SHOOTING,
    DESTROY
}
public class OpponentGorillaBullet : MonoBehaviour
{
    public float speed = 30f;    
    public Rigidbody2D rb;

    GameManager gameManager;

    public GameObject rotationPoint;

    public LayerMask groundLayerMask;
    private CharacterController2D charController;

    private GameObject opponent;
    private OpponentGorillaShooting opponentScript;

    public bool isShooted = false;
    public BulletState shootState;

    private Animator bulletAnimator;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        shootState = BulletState.IDLE;

        bulletAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        charController = gameManager.hero.GetComponent<CharacterController2D>();
    }

    void Update()
    {
        if(opponentScript == null)
            return;

        // if(isShooted == false)
        // {
        //     transform.position = opponentScript.idlePoint.transform.position;
        // }
        
    }
    public void Shoot()
    {
        isShooted = true;
        shootState = BulletState.SHOOTING;

        // rotate bullet
        if(charController != null)
        {
            Transform heroGroundCheck = gameManager.hero.transform;// charController.m_GroundCheck;
            
            Vector3 dir = heroGroundCheck.position - rotationPoint.transform.position;
            dir = heroGroundCheck.InverseTransformDirection(dir);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -54f, -13f);

            rotationPoint.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
        }

        bulletAnimator.SetBool("IsFlying", true);
        rb.velocity = rotationPoint.transform.right * speed;
    }
    
    public void SetOpponentGameObject(GameObject _opponent)
    {
        opponent = _opponent;
        opponentScript = opponent.GetComponent<OpponentGorillaShooting>();
    }

    void FixedUpdate()
    {
        var collider = GetComponent<BoxCollider2D>();
        var glm = 1 << LayerMask.NameToLayer("GROUND");

        if(collider.IsTouchingLayers(glm))
        {
            shootState = BulletState.DESTROY;
            rb.velocity = Vector2.zero;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            bulletAnimator.SetTrigger("Boom");
            bulletAnimator.SetBool("IsFlying", false);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {   
        // return;
        if(shootState != BulletState.SHOOTING)
            return;

        PlayerMovement playerMovement = hitInfo.GetComponent<PlayerMovement>();
        if(playerMovement != null)
        {
            if(!gameManager){
                return;
            }

            if(gameManager.hero.GetComponent<CharacterController2D>().isDead)
                return;

            gameManager.addLife(-1);

            Destroy(gameObject);
        }
    }

    public void BoomAnimOverEvent()
    {
        Debug.Log("BoomAnimOverEvent");
        Destroy(gameObject);
    }
}

