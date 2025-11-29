using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int ballMovementSpeed;

    private int startingBallX=-1;
    private float ballForceAbove = 1;
    private float ballForceDown = -1f;

    [SerializeField] private Player player;
    [SerializeField] private Player_2 player2;
    public bool ballIsAlive= true;

    private bool playerWon=true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Canvas.Instance.winner == false)
        {
            Vector2 ballForce = new Vector2(-startingBallX, Random.Range(ballForceAbove, ballForceDown)).normalized;
            rb.velocity = ballForce * ballMovementSpeed;
        }
        else
        {
            Vector2 ballForce = new Vector2(startingBallX, Random.Range(ballForceAbove, ballForceDown)).normalized;
            rb.velocity = ballForce * ballMovementSpeed;
        }

    }

    void FixedUpdate()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Vector2 collisionNormal = collision.contacts[0].normal;
            rb.velocity = Vector2.Reflect(rb.velocity, collisionNormal);
            Vector2 baseSpeed = rb.velocity.normalized * ballMovementSpeed;
            if (player.isPressed == true || player2.isPressed == true) 
            {
                rb.velocity = Vector2.Reflect(rb.velocity, collisionNormal);
                rb.velocity *= 2f;
            }
            else
            {
                rb.velocity = Vector2.Reflect(rb.velocity, collisionNormal);
                rb.velocity = baseSpeed;
            }
        }

        if (collision.gameObject.layer == 6)
        {
            ballIsAlive = false;
            rb.velocity *= 0;
            Canvas.Instance.PlayerTwoScore(1);
            Canvas.Instance.PlayerWon("Player-2 Won");
            Canvas.Instance.GameOver();
            Canvas.Instance.winner = true;
        }

        if (collision.gameObject.layer == 7)
        {
            ballIsAlive = false;
            rb.velocity *= 0;
            Canvas.Instance.PlayerOneScore(1);
            Canvas.Instance.PlayerWon("Player-1 Won");
            Canvas.Instance.GameOver();
            Canvas.Instance.winner = false;
        }
        
        

    }
    
   
}
