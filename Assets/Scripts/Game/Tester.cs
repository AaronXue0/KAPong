using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class Tester : MonoBehaviour
{
    public float moveDuration;
    Animator animator;
    Vector2 movement;

    bool isAttacking = false;
    int attackState = 0;
    public float attackStateDuration = 1.25f;
    public float attackCounter = 0f;


    public void OnMovement(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
            case InputActionPhase.Performed:
                movement = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                movement = context.ReadValue<Vector2>();
                break;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isAttacking) return;
        isAttacking = true;
        Invoke("AbleToAttack", 0.5f);
        animator.SetTrigger("attack" + attackState.ToString());
        attackState = (attackState + 1) % 3;
        attackCounter = attackStateDuration;
    }

    void AbleToAttack() { isAttacking = false; }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("movement", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        transform.localScale = new Vector3(transform.localScale.x > 0 ? movement.x >= 0 ? 1 : -1 : movement.x <= 0 ? -1 : 1, 1, 1);
        if (attackCounter > 0) attackCounter -= Time.deltaTime;
        if (attackCounter <= 0 && attackState > 0) {
            animator.SetTrigger("reset");
            attackState = 0;
        }
    }
    private void FixedUpdate()
    {
        transform.DOLocalMove(new Vector2(movement.x + transform.position.x, movement.y + transform.position.y),
                                          moveDuration);
    }
}
