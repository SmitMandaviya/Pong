using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private int playerOneMoveSpeed;

    private float initialPosX = -17.4f;

    [SerializeField] private Transform gizmo;

    [SerializeField] private float gizmoradious;

    private bool isTouching = false;

    public bool isPressed = false;
    [SerializeField] private LayerMask player_1;

    [SerializeField] private BallScript ballScript;

    private void Start()
    {
    }
    private void Update()
    {
        TransformPlayerOneMovement();
        CollissionCheck();
    }

    private void TransformPlayerOneMovement()
    {
        Vector2 inputVector = new Vector2(transform.position.x, 0);
        if (ballScript.ballIsAlive == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (transform.position.y < 8.44f)
                    inputVector.y += 1.01f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (transform.position.y > -8.44f)
                    inputVector.y -= 1.01f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
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
                    transform.position += force * playerOneMoveSpeed * Time.deltaTime;

                    yield return new WaitForSeconds(delay);
                    isPressed = false;

                    transform.position = new Vector2(initialPosX, transform.position.y);
                }
            }

            Vector3 movedir = new Vector3(0f, inputVector.y, 0f).normalized;

            transform.position += movedir * playerOneMoveSpeed * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gizmo.position, gizmoradious);
    }

    private void CollissionCheck()
    {
        isTouching= Physics2D.OverlapCircle(gizmo.position, gizmoradious, player_1);
    }
}
