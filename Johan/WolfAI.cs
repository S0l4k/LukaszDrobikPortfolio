using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] GameObject wolf;
    //[SerializeField] Animator animator;

    [Header("Options")]
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float speed;
    [SerializeField] float pauseTime;

    [Header("Raycast")]
    [SerializeField] LayerMask player;
    [SerializeField] float viewingDistance;

    private int pointIndex;
    private float raycastIndex;

    private float waitTime;
    private bool isWaiting = false;

    private bool isAttack;
    private bool Dead;
    private int attackTime = 1;

    void Start()
    {
        raycastIndex = 1f;
    }

    void Update()
    {
        if (isAttack)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= attackTime)
            {
                if (Dead)
                {
                    //animator.SetTrigger("Attack");
                }
                else
                {
                    Follow();
                    //animator.SetTrigger("Run");
                }
            }
            else
            {
                //animator.SetTrigger("Trigger");
            }
        }
        else
        {
            Walk();
            RaycastHit();
        }

        RaycastBack();
    }

    private void Walk()
    {
        if (isWaiting)
        {
            //animator.SetFloat("Speed", 0);

            waitTime += Time.deltaTime;
            if (waitTime >= pauseTime)
            {
                isWaiting = false;
                waitTime = 0f;

                SwitchPoint();
            }
        }
        else
        {
            Vector2 move = (patrolPoints[pointIndex].position - transform.position).normalized;
            wolf.transform.Translate(move * speed * Time.deltaTime);

            //animator.SetFloat("Speed", 1);

            if (Vector2.Distance(wolf.transform.position, patrolPoints[pointIndex].position) < 0.1f)
            {
                isWaiting = true;
            }
        }
    }

    private void SwitchPoint()
    {
        Flip();

        switch (pointIndex)
        {
            case 0:
                raycastIndex = -1f;
                break;
            case 1:
                raycastIndex = 1f;
                break;
        }

        pointIndex = (pointIndex + 1) % patrolPoints.Length;
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void RaycastHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(wolf.transform.position, Vector2.right * new Vector2(raycastIndex, 0f), viewingDistance, player);
        Debug.DrawRay(wolf.transform.position, Vector2.right * viewingDistance * new Vector2(raycastIndex, 0f), Color.green);

        if (hit.collider != null)
        {
            isAttack = true;
        }
    }

    private void RaycastBack()
    {
        RaycastHit2D back = Physics2D.Raycast(wolf.transform.position, -Vector2.right * new Vector2(raycastIndex, 0f), 0.6f, player);

        if (back.collider != null)
        {
            Vector3 localScale = transform.localScale;

            if (raycastIndex < 0)
            {
                localScale.x = 1f;
            }
            else
            {
                localScale.x = -1f;
            }

            transform.localScale = localScale;

            isAttack = true;
        }
    }

    private void Follow()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            speed = 1f;

            Vector3 playerDirection = playerObject.transform.position - wolf.transform.position;
            wolf.transform.Translate(playerDirection.normalized * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player == (player | (1 << other.gameObject.layer)))
        {
            Dead = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(patrolPoints[0].transform.position, 0.1f);
        Gizmos.DrawWireSphere(patrolPoints[1].transform.position, 0.1f);
        Gizmos.DrawLine(patrolPoints[0].transform.position, patrolPoints[1].transform.position);
    }
}
