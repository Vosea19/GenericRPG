using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform enemyTransform;
    public int aggroRadius;
    private Transform nearestPlayer;
    private Coroutine moveCo;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = transform;
        StartCoroutine(FindPlayers());
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nearestPlayer != null)
        {
            if (moveCo != null)
            {
                StopCoroutine(moveCo);
            }
            moveCo = StartCoroutine(Move(nearestPlayer.position));
        }

    }

    IEnumerator Move(Vector3 point)
    {
        float distance = Vector3.Distance(point, enemyTransform.position);
        float speed = .25f;
        Vector3 scaledVector = ((point - enemyTransform.position).normalized);
        int steps = Mathf.FloorToInt(distance / speed);
        for (int i = 0; i < steps; i++)
        {
            rb.MovePosition(enemyTransform.position + (scaledVector * speed));
            //yield return new WaitForSeconds(0.02f);
            //yield return new WaitForEndOfFrame();
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = Vector3.zero;
    }
    IEnumerator FindPlayers()
    {
        while (true)
        {
            if (nearestPlayer == null)
            {
                Collider[] players = Physics.OverlapSphere(enemyTransform.position, aggroRadius, LayerMask.GetMask("Player"));
                if (players.Length > 0)
                {
                    nearestPlayer = players[0].transform;
                }
                

            }
            yield return new WaitForSeconds(.25f);

        }
    }
}
