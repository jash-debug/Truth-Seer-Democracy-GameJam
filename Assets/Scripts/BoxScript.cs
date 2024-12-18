using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb;
    private float xLimit = 1.9f;  // Limit for horizontal movement
    private float moveSpeed = 2f;  // Speed for horizontal movement
    //private bool isFalling = false; // Track whether the box should fall

    private bool canMove;
    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Start()
    {
        canMove = true;

        GameplayController.instance.currentBox = this;

    }

    void Update()
    {
        //if (!isFalling)
        //{
        //    MoveBox();
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartFalling();
        //}

        MoveBox();
    }

    // Move the box horizontally between limits
    void MoveBox()
    {
        //// Move the box left and right
        //rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        //// Reverse direction when limits are reached
        //if (transform.position.x > xLimit)
        //{
        //    moveSpeed = -Mathf.Abs(moveSpeed); // Move left
        //}
        //else if (transform.position.x < -xLimit)
        //{
        //    moveSpeed = Mathf.Abs(moveSpeed); // Move right
        //}

        if (canMove)
        {
            Vector3 temp = transform.position;

            temp.x += moveSpeed * Time.deltaTime;

            if (temp.x > xLimit)
            {
                moveSpeed = -Mathf.Abs(moveSpeed);
            }

            else  if (temp.x < -xLimit)
                {
                    moveSpeed =  Mathf.Abs(moveSpeed);
            }

            transform.position = temp;
        }
    }

    public void DropBox()
    {
        canMove = false;
        rb.gravityScale = Random.Range(2, 4);
    }

    void Landed()
    {
        if (gameOver)
            return;

        ignoreCollision = true;
        ignoreTrigger = true;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.AddScore();
        GameplayController.instance.MoveCamera();
    }

    public void RestartGame()
    {
        GameplayController.instance.RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.CompareTag("Platform") || target.gameObject.CompareTag("Box"))
        {
            Invoke("Landed", 0.5f);
            AudioManager.Instance.PlaySfx("landed");
            ignoreCollision = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;
        if (target.CompareTag("GameOver"))
        {
            Debug.Log("game over");
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;

            Invoke("RestartGame", 2f);
        }
        
    }



}
