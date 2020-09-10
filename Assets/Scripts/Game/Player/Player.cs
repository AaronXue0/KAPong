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
        public Transform checkPos;
        [SerializeField]
        public float radius;
        [SerializeField]
        public LayerMask whatIsBorder;
        [SerializeField]
        public float moveDuration = 10;

        Control control = new Control();
        Animator aniamtor;
        Vector2 movement = Vector2.zero;

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
            BorderHandling();
            transform.DOLocalMove(new Vector2(movement.x + transform.position.x, movement.y + transform.position.y),
                                              moveDuration);
        }
        void BorderHandling()
        {
            Vector2 pos = new Vector2(checkPos.position.x, checkPos.position.y) + movement;
            if (Physics2D.OverlapCircle(pos, radius, whatIsBorder))
            {
                Vector2 posX = new Vector2(movement.x + checkPos.position.x, 0);
                Vector2 posY = new Vector2(0, movement.y + checkPos.position.y);
                if (Physics2D.OverlapCircle(posX, radius, whatIsBorder)) movement -= new Vector2(movement.x, 0);
                if (Physics2D.OverlapCircle(posY, radius, whatIsBorder)) movement -= new Vector2(0, movement.y);
            }
        }
        void Awake()
        {
            aniamtor = GetComponent<Animator>();
        }
    }

}