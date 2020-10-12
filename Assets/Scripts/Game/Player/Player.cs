using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using GameSystem;

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

        protected Joystick joystick;

        public void Hurt()
        {
            if (control.IsHurt) return;
            life--;
            if (life <= 0) Dead();
            else aniamtor.SetTrigger("hurt");
            control.HurtHandling(life);
        }
        void Dead()
        {
            gm.GameOver();
            //aniamtor.SetTrigger("dead");
        }
        public void AbleToMove(bool state)
        {
            if (state == true)
            {
                joystick = FindObjectOfType<Joystick>();
                Collider2D collider = GetComponent<Collider2D>();
                collider.enabled = true;
                control.Recovery(ref life);
            }
            GetComponent<PlayerInput>().enabled = state;
            if (state == false)
            {
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
                    transform.localScale = new Vector3(transform.localScale.x > 0 ? movement.x >= 0 ? 1 : -1 : movement.x <= 0 ? -1 : 1 ,1,1);
                    break;
                case InputActionPhase.Canceled:
                    movement = context.ReadValue<Vector2>();
                    transform.localScale = new Vector3(transform.localScale.x > 0 ? movement.x >= 0 ? 1 : -1 : movement.x <= 0 ? -1 : 1 ,1,1);
                    break;
            }
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            aniamtor.SetTrigger("attack");
        }
        private void Update()
        {
            if (joystick == null) return;
            //movement = new Vector2(joystick.Horizontal, joystick.Vertical);
            control.BorderHandling(ref movement);
            aniamtor.SetFloat("movement", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
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
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Thunder") Hurt();
        }
    }

}