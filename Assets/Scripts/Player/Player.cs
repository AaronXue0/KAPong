using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.PlayerSpace
{
    public class Player : MonoBehaviour
    {
        [Header("Attritubes")]
        [SerializeField]
        float speed;

        [Header("Components")]
        Control control = new Control();
        PlayerControls im;
        Rigidbody2D rb;
        Animator animator;

        [Header("Private Variable")]
        Vector2 movement = Vector2.zero;

        public void Move(Vector2 force)
        {
            Vector2 m = new Vector2(movement.x, movement.y) * speed;
            control.DoMove(rb, m);
        }

        public void Attack()
        {
            Debug.Log(1);
            //control.DoAnimator(animator, "Attack");
        }

        private void Awake()
        {
            im = new PlayerControls();
            im.GamePlay.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
            im.GamePlay.Move.canceled += ctx => movement = Vector2.zero;
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
        private void OnEnable()
        {
            im.GamePlay.Enable();
        }
        void OnDisable()
        {
            im.GamePlay.Disable();
        }
        void FixedUpdate()
        {
          if(movement != Vector2.zero)  Move(movement);
        }
        void Update()
        {
        }
    }

}