using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Transform cacheTransform;
    private Coroutine moveCoroutine;
    private float actionSpeed;
    public void Awake()
    {
        cacheTransform = transform;
    }
    public void StartMove(Vector3 newPosition,float NewActionSpeed)
    {
        actionSpeed = NewActionSpeed;
        Vector3 normalizedPosition = new Vector3(newPosition.x, cacheTransform.position.y, newPosition.z);
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(Move(normalizedPosition));
    }
    IEnumerator Move(Vector3 point)
    {
        cacheTransform.LookAt(point);
        float distance = Vector3.Distance(point, transform.position);
        float speed = .1f * actionSpeed;
        Vector3 scaledVector = ((point - transform.position).normalized);
        int steps = Mathf.FloorToInt(distance / speed);
        for (int i = 0; i < steps; i++)
        {
            cacheTransform.position += (scaledVector * speed);
            yield return new WaitForEndOfFrame();
        }
    }
}
