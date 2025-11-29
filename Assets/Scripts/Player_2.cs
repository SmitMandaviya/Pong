using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : MonoBehaviour
{

    [SerializeField] private int playerTwoMoveSpeed;

    private BallScript ball;

    private float initialPosX = 17.4f;

    [SerializeField] private Transform gizmo;
    [SerializeField] private float gizmoradious;
    private bool isTouching = false;
    [SerializeField] private LayerMask player_2;

    public bool isForced = false;
    public bool isPressed=false;

    [SerializeField] private BallScript ballScript;

    private void Start()
    {
        ball = GetComponent<BallScript>(); 
    }
    private void Update()
    {
        TransformPlayerTwoMovement();
        CollissionCheck();
    }

    private void TransformPlayerTwoMovement()
    {
        Vector2 inputVector = new Vector2(transform.position.x, 0);
        if (ballScript.ballIsAlive)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (transform.position.y < 8.44f)
                    inputVector.y += 1.0f;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (transform.position.y > -8.44f)
                    inputVector.y -= 1.0f;
            }
            if (Input.GetKeyDown(KeyCode.RightControl))
            {

                if (isTouching == true)
                {

                    StartCoroutine(PlateForce());

                }

                IEnumerator PlateForce()
                {
                    isPressed = true;
                    float delay = 0.5f;
                    inputVector.x -= 1;
                    Vector3 force = new Vector3(-inputVector.x, 0f, 0f).normalized;
                    transform.position += force * playerTwoMoveSpeed * Time.deltaTime;
                    yield return new WaitForSeconds(delay);
                    isPressed = false;

                    transform.position = new Vector2(initialPosX, transform.position.y);
                }
            }

            Vector3 movedir = new Vector3(0f, inputVector.y, 0f).normalized;

            transform.position += movedir * playerTwoMoveSpeed * Time.deltaTime;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gizmo.position, gizmoradious);
    }

    private void CollissionCheck()
    {
        isTouching = Physics2D.OverlapCircle(gizmo.position, gizmoradious, player_2);
    }

}
