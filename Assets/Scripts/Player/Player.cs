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
        [SerializeField]
        float strength;
        [SerializeField]
        LayerMask boundary;

        [Header("Components")]
        GameManager gm;
        Control control = new Control();
        PlayerControls im;
        Rigidbody2D rb;
        Animator animator;
        [Header("Variables")]
        Vector2 movement = Vector2.zero;

        public void GetHurt(float hp)
        {
            if(hp <= 0) {
                control.DoAnimator(animator, "death");
                Destroy(this);
            }
            else control.DoAnimator(animator, "hurt");
        }
        public void Move(Vector2 force)
        {
            control.DoAnimator(animator, "movement", Mathf.Abs(force.x) + Mathf.Abs(force.y));
            BorderHandling(force);
        }

        public void Attack()
        {
            control.DoAnimator(animator, "attack");
        }

        public void Parry()
        {
            control.DoAnimator(animator, "parry");
        }

        private void Awake()
        {
            im = new PlayerControls();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            gm = FindObjectOfType<GameManager>();
        }
        private void FixedUpdate()
        {
        }
        private void Update()
        {
            Move(movement);
        }
        private void OnEnable()
        {
            im.GamePlay.Enable();
        }
        private void OnDisable()
        {
            im.GamePlay.Disable();
        }
        private void OnMovement(InputValue value)
        {
            Move(value.Get<Vector2>());
        }
        private void OnAttack(InputValue value)
        {
            Attack();
        }
        private void OnParry(InputValue value)
        {
            Parry();
        }
        private void BorderHandling(Vector2 pos)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, pos, 1f, boundary);
            if (hit)
            {
                control.DoMove(rb, new Vector2(transform.position.x, transform.position.y));
            }
            else
            {
                movement = pos.normalized * speed * Time.deltaTime;
                control.DoMove(rb, new Vector2(movement.x + transform.position.x, movement.y + transform.position.y));
            }
        }
    }
}