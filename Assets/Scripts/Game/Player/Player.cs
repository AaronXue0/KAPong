using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Role.Playerspace
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        public float moveDuration = 10;
        [SerializeField]
        int life = 3;
        GameManager gm;
        Control control;
        Animator aniamtor;
        Vector2 movement = Vector2.zero;

        public void Hurt()
        {
            if (control.IsHurt) return;
            life--;
            if (life <= 0) Dead();
            else aniamtor.SetTrigger("hurt");
            control.HurtHandling();
        }
        void Dead()
        {
            gm.GameOver();
            //aniamtor.SetTrigger("dead");
        }
        public void AbleToMove()
        {
            GetComponent<PlayerInput>().enabled = true;
        }
        public void OnMovement(InputAction.CallbackContext context)
        {
            aniamtor.SetFloat("movement", Mathf.Abs(context.ReadValue<Vector2>().x) + Mathf.Abs(context.ReadValue<Vector2>().y));
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
            aniamtor.SetTrigger("attack");
        }
        private void Update()
        {
            control.BorderHandling(ref movement);
        }
        private void FixedUpdate()
        {
            transform.DOLocalMove(new Vector2(movement.x + transform.position.x, movement.y + transform.position.y),
                                              moveDuration);
        }
        void Awake()
        {
            control = GetComponentInChildren<Control>();
            aniamtor = GetComponent<Animator>();
        }
        void Start()
        {
            gm = FindObjectOfType<GameManager>();
        }
    }

}