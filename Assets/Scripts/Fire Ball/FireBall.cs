using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    public class FireBall : MonoBehaviour
    {
        [Header("Attritubes")]
        [SerializeField]
        float maxSpeed;
        [SerializeField]
        float speed;

        [Header("Classes")]
        Control control = new Control();
        //DoAbility doAbility = new DoAbility();

        [Header("Components")]
        Rigidbody2D rb;
        Animator animator;

        [Header("Variables")]
        Vector2 movement = Vector2.zero;


        public void DoSin()
        {

        }
        public void DoCopy()
        {

        }
        public void DoSpin()
        {

        }
        public void Move(Vector2 velocity)
        {
            Vector2 m = new Vector2(movement.x + velocity.x, movement.y + velocity.y) * speed;
            TowardToMovingDirection();
            if(speed > maxSpeed) speed = maxSpeed;
            control.DoMove(rb, m);
        }
        public void TowardToMovingDirection()
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        void Update()
        {

        }
        void FixedUpdate()
        {
            if (movement != Vector2.zero) Move(movement);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            control.BounceHandling(ref speed, ref movement, transform, other);
        }
        private void OnCollisionEnter2D(Collision2D other) {
            control.BounceHandling(ref speed, ref movement, rb, transform, other);
        }
        void Awake()
        {
            SetComponents();
        }
        void SetComponents()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
        void Start()
        {

        }
    }

}