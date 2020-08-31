using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    public class SeparateBall : MonoBehaviour
    {
        [Header("Attritubes")]
        [SerializeField]
        float maxSpeed;
        [SerializeField]
        Rigidbody2D rb;
        [SerializeField]
        float speed;
        Vector2 movement;
        private float time;
        GameManager gm;
        [SerializeField]
        GameObject fireball;

        public void initBall(Vector2 movement,float angle)
        {            
            this.movement = movement;
            this.movement = transform.right - transform.up * angle;
        }
        public Vector2 GetMovement
        {
            get { return movement; }
        }
        public Transform GetBallTransform
        {
            get { return transform; }
        }
        public void Divide()
        {

        }
        public void Spin()
        {

        }
        public void Move(Vector2 velocity)
        {
            if (rb == null) return;
            Vector2 m = (velocity + velocity) * speed;
            TowardToMovingDirection();
            if (speed > maxSpeed) speed = maxSpeed;
            rb.velocity = m;
        }
        public void TowardToMovingDirection()
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        void Update()
        {
            time += Time.deltaTime;
            if (time >= 5)
            {
                Destroy(gameObject);
            }
        }
        void FixedUpdate()
        {
            if (movement != Vector2.zero) Move(movement);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                if (movement != Vector2.zero && movement.magnitude >= 1f) gm.PlayerHurt();
            }
            switch (other.tag)
            {
                case "Player":
                case "Enemy":
                    if (movement == Vector2.zero) return;
                    movement = new Vector2(movement.x > 0 ? -0.1f : 0.1f, 0);
                    Destroy(gameObject);
                    break;
                case "Player Sword":
                    float distance = movement.magnitude;
                    movement = transform.position - other.transform.position;
                    speed = 10 / (Mathf.Abs(distance - 2) + 1);
                    break;
                case "Enemy Sword":
                    if (movement.x == 0)
                    {
                        movement = transform.position - other.transform.position;
                    }
                    else
                        movement = Vector2.Reflect(movement * 0.7f, new Vector2(-1, 0));
                    break;
            }
            Move(movement);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            switch (other.collider.tag)
            {
                case "Player":
                case "Enmey":
                    if (movement == Vector2.zero) return;
                    movement = new Vector2(movement.x > 0 ? -0.1f : 0.1f, 0);
                    break;
                default:
                    Vector2 inDirection = movement;
                    Vector2 inNormal = other.contacts[0].normal;
                    movement = Vector2.Reflect(inDirection, inNormal);
                    break;
            }
        }
        void Awake()
        {
            SetComponents();
        }
        void SetComponents()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void Start()
        {
            gm = FindObjectOfType<GameManager>();
            
        }
    }
}
