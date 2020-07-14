using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Role.PlayerSpace
{
    public class Player : MonoBehaviour
    {
        public PlayerInput playerInput;
        [Header("Attritubes")]
        [SerializeField]
        float speed;

        [Header("Components")]
        Control control = new Control();
        PlayerControls im;
        Rigidbody2D rb;
        Animator animator;

        [Header("Variables")]
        Vector2 movement = Vector2.zero;

        public void Move(Vector2 force)
        {
            movement = force * speed * Time.deltaTime;
            movement = movement.normalized * speed * Time.deltaTime;
            control.DoMove(rb, new Vector2(movement.x + transform.position.x, movement.y + transform.position.y));
            control.DoAnimator(animator, "movement", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        }

        public void Attack()
        {
            Debug.Log("Attack");
            //control.DoAnimator(animator, "Attack");
        }

        private void Awake()
        {
            im = new PlayerControls();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
        void FixedUpdate()
        {
        }
        void Update()
        {
            Move(movement);
        }
        private void OnEnable()
        {
            im.GamePlay.Enable();
        }
        void OnDisable()
        {
            im.GamePlay.Disable();
        }
        private void OnAttack(InputValue value)
        {
            Attack();
        }
        private void OnMovement(InputValue value)
        {
            Move(value.Get<Vector2>());
        }
    }
}