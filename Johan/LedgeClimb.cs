using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeClimb : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _ledgeCheck1;
    [SerializeField] private Transform _ledgeCheck2;
    [SerializeField] private LayerMask _ledgeRight;
    [SerializeField] private LayerMask _ledgeLeft;
    [SerializeField] private Animator _anim;
    [SerializeField] private Rigidbody2D rb;

    [Header("Scripts")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AxeThrow axeThrow;

    [Header("Hit Right")]
    private bool _hit1R;
    private bool _hit2R;
    private RaycastHit2D hitRight1;
    private RaycastHit2D hitRight2;


    [Header("Hit Left")]
    [SerializeField] private bool _hit1L;
    [SerializeField] private bool _hit2L;
    private RaycastHit2D hitLeft1;
    private RaycastHit2D hitLeft2;

    [Header("Variables")]
    [SerializeField] private float _checkDistanceDown = 0.5f;
    [SerializeField] private float _checkDistanceUp = 0.53f;

    private bool _isClimbing = false;
    [SerializeField] private float _legdeOffsetX = 1f;
    [SerializeField] private float _legdeOffsetY = 0f;
    [SerializeField] private float _finishOffsetX = 0f;
    [SerializeField] private float _finishOffsetY = 1f;
    private int _climbState = 0;

    // Update is called once per frame
    void Update()
    {
        CheckForCollision();
        AnimationStateCheck();
        if (_hit1R == false && _hit2R == true && _isClimbing == false)
        {
            FreezePlayer();
            transform.position = new Vector2(hitRight2.transform.position.x - _legdeOffsetX, hitRight2.transform.position.y + _legdeOffsetY);
        }

        if (_hit1L == false && _hit2L == true && _isClimbing == false)
        {
            FreezePlayer();
            transform.position = new Vector2(hitLeft2.transform.position.x + _legdeOffsetX, hitLeft2.transform.position.y + _legdeOffsetY);
        }

        if(_climbState == 2)
        {
            _climbState = 0;

            switch(PlayerMovement.facingRight)
            {
                case true:
                    transform.position = new Vector2(hitRight2.transform.position.x - _finishOffsetX, hitRight2.transform.position.y + _finishOffsetY);
                    break;
                case false:
                    transform.position = new Vector2(hitLeft2.transform.position.x + _finishOffsetX, hitLeft2.transform.position.y + _finishOffsetY);
                    break;
            }
            UnfreezePlayer();
        }

        if (transform.rotation != Quaternion.identity)
        {
            transform.rotation = Quaternion.identity;
        }
    }


    private void FreezePlayer()
    {
        _isClimbing = true;
        playerMovement.enabled = false;
        axeThrow.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        _anim.SetTrigger("Climbing");
    }

    private void UnfreezePlayer()
    {
        _isClimbing = false;
        playerMovement.enabled = true;
        axeThrow.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void AnimationStateCheck()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Climb") && _climbState == 0)
        {
            _climbState = 1;
        }
        else if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Climb") && _climbState == 1)
        {
            _climbState = 2;
        }
    }

    private void CheckForCollision()
    {
        switch (PlayerMovement.facingRight)
        {
            case true:
                hitRight1 = Physics2D.Raycast(new Vector2(_ledgeCheck1.transform.position.x, _ledgeCheck1.transform.position.y), Vector2.right, _checkDistanceUp, _ledgeRight);

                if (hitRight1)
                {
                    _hit1R = true;
                }
                else
                {
                    _hit1R = false;
                }


                hitRight2 = Physics2D.Raycast(new Vector2(_ledgeCheck2.transform.position.x, _ledgeCheck2.transform.position.y), Vector2.right, _checkDistanceDown, _ledgeRight);

                if (hitRight2)
                {
                    _hit2R = true;
                }
                else
                {
                    _hit2R = false;
                }
                return;

            case false:
                hitLeft1 = Physics2D.Raycast(new Vector2(_ledgeCheck1.transform.position.x, _ledgeCheck1.transform.position.y), Vector2.left, _checkDistanceUp, _ledgeLeft);

                if (hitLeft1)
                {
                    _hit1L = true;
                }
                else
                {
                    _hit1L = false;
                }

                hitLeft2 = Physics2D.Raycast(new Vector2(_ledgeCheck2.transform.position.x, _ledgeCheck2.transform.position.y), Vector2.left, _checkDistanceDown, _ledgeLeft);

                if (hitLeft2)
                {
                    _hit2L = true;
                }
                else
                {
                    _hit2L = false;
                }
                return;
        }
    }

    private void OnDrawGizmos()
    {
        switch (PlayerMovement.facingRight)
        {
            case true:
                Gizmos.DrawLine(new Vector3(_ledgeCheck1.transform.position.x, _ledgeCheck1.transform.position.y, _ledgeCheck1.transform.position.z),
            new Vector3(_ledgeCheck1.transform.position.x + _checkDistanceDown, _ledgeCheck1.transform.position.y, _ledgeCheck1.transform.position.z));

                Gizmos.DrawLine(new Vector3(_ledgeCheck2.transform.position.x, _ledgeCheck2.transform.position.y, _ledgeCheck2.transform.position.z),
            new Vector3(_ledgeCheck2.transform.position.x + _checkDistanceDown, _ledgeCheck2.transform.position.y, _ledgeCheck2.transform.position.z));
                break;
            case false:
                Gizmos.DrawLine(new Vector3(_ledgeCheck1.transform.position.x, _ledgeCheck1.transform.position.y, _ledgeCheck1.transform.position.z),
            new Vector3(_ledgeCheck1.transform.position.x - _checkDistanceDown, _ledgeCheck1.transform.position.y, _ledgeCheck1.transform.position.z));

                Gizmos.DrawLine(new Vector3(_ledgeCheck2.transform.position.x, _ledgeCheck2.transform.position.y, _ledgeCheck2.transform.position.z),
            new Vector3(_ledgeCheck2.transform.position.x - _checkDistanceDown, _ledgeCheck2.transform.position.y, _ledgeCheck2.transform.position.z));
                break;
        }
    }

}
