using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Camera playerCamera;
    Coroutine moveCoroutine;
    private Rigidbody rb;
    public float speed;
    private Animator animator;
    private Transform playerTransform;
    private void Start()
    {
        playerTransform = transform;
        animator = GetComponent<Animator>();
        playerCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray pointRay = playerCamera.ScreenPointToRay(mousePos); 
            if (Physics.Raycast(pointRay, out RaycastHit point, 100f))
            {
                Vector3 position = new Vector3(point.point.x, playerTransform.position.y, point.point.z);
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(Move(position));
            }   
        }
        
        animator.speed = Mathf.Approximately(rb.velocity.magnitude , 0f) ?  1 : 2;
        

    }
    IEnumerator Move(Vector3 point)
    {
        playerTransform.LookAt(point);
        float distance = Vector3.Distance(point, transform.position);
        float speed = .4f;
        Vector3 scaledVector = ((point - transform.position).normalized);
        int steps = Mathf.FloorToInt(distance / speed);
        for (int i = 0; i < steps; i++)
        { 
            rb.MovePosition(transform.position + (scaledVector * speed));  
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = Vector3.zero;

    }
}
