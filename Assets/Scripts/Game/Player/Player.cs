using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using GameManagerSpace;

namespace Role.Playerspace
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        public bool ableToFlip = false;
        [SerializeField]
        public float moveDuration = 10;
        [SerializeField]
        int life = 3;
        GameManager gm;
        Control control;
        Animator animator;
        Vector2 movement = Vector2.zero;

        public Joystick joystick;
        bool isGameStarted = false;
        bool isAttacking = false;
        int attackState = 0;
        public float attackStateDuration = 1.25f;
        public float attackCounter = 0f;

        public void Revival(System.Action callback)
        {
            float time = 0;
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                if (ac.animationClips[i].name == "UnDoDead")
                {
                    time = ac.animationClips[i].length;
                }
            }
            isGameStarted = false;
            animator.SetTrigger("revival");
            StartCoroutine(MoveToOriginal(callback, time));
        }
        IEnumerator MoveToOriginal(System.Action callback, float sec)
        {
            yield return new WaitForSecondsRealtime(1f);
            while (transform.position != Vector3.zero)
            {
                yield return null;
                transform.DOMove(Vector3.zero - transform.position, 2).SetUpdate(true);
            }
            callback();
        }
        public void Hurt(int n)
        {
            if (control.IsHurt) return;
            for (int i = 0; i < n; i++)
            {
                life--;
                control.HurtHandling(life);
            }
            if (life <= 0) Dead();
            else animator.SetTrigger("hurt");
        }
        void Dead()
        {
            gm.GameOver();
            animator.SetTrigger("dead");
        }
        public void AbleToMove(bool state)
        {
            if (state == true)
            {
                isGameStarted = true;
                Collider2D collider = GetComponent<Collider2D>();
                collider.enabled = true;
                control.Recovery(ref life);
            }
            GetComponent<PlayerInput>().enabled = state;
            if (state == false)
            {
                isGameStarted = false;
                Collider2D collider = GetComponent<Collider2D>();
                collider.enabled = false;
            }
        }
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
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O)) Hurt(3);
            if (Input.GetKeyDown(KeyCode.P)) Revival(AbleToAttack);
            if (isGameStarted == false) return;
            movement = new Vector2(joystick.Horizontal, joystick.Vertical);
            control.BorderHandling(ref movement);
            animator.SetFloat("movement", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
            if (ableToFlip)
                transform.localScale = new Vector3(transform.localScale.x >= 0 ? movement.x >= 0 ? 1 : -1 : movement.x <= 0 ? -1 : 1, 1, 1);
            if (attackCounter > 0) attackCounter -= Time.deltaTime;
            if (attackCounter <= 0 && attackState > 0)
            {
                animator.SetTrigger("reset");
                attackState = 0;
            }
        }
        private void FixedUpdate()
        {
            transform.DOLocalMove(new Vector2(movement.x + transform.position.x, movement.y + transform.position.y),
                                              moveDuration);
        }
        void Awake()
        {
            control = GetComponentInChildren<Control>();
            animator = GetComponent<Animator>();
        }
        void Start()
        {
            gm = FindObjectOfType<GameManager>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Thunder") Hurt(1);
        }
    }

}