using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 80f;
    float horizontalMove = 0f;
    bool jump = false;
    bool dead = false;

    bool canJump = false;

    public int heronum = 0;
    float dieAtY = -4f;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        GetComponent<PlayerSkinController>().ChangeHeroSkin(gameManager.heroNum);
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(dead)
            return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump") && canJump)
        {
            jump = true;
            canJump = false;
            animator.SetBool("IsJumping", true);
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(gameObject.transform.position.y < dieAtY)
        {
            gameManager.addLife(-1);
        }
    }

    void FixedUpdate()
    {
        if(dead)
            return;

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void OnLanding2()
    {
        canJump = true;
        // Debug.Log("OnLanding2");
        if(dead)
            return;

        animator.SetBool("IsJumping", false);
    }

    public void OnDead()
    {
        jump = false;
        dead = true;
        horizontalMove = 0;
        animator.StopPlayback();
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsDead", true);
    }

    public void Restart()
    {
        if(gameManager.isGameOver)
        {
            return;
        }

        Debug.Log("RESTART() () ()");
        animator.SetBool("IsDead", false);
        dead = false;
    }
}
