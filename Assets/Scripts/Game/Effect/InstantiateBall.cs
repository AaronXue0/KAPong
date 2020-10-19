using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagerSpace;

namespace Role.InstantiateBallSpace
{
    public enum AbilityState { None = 0, SineWave = 1, Transparency = 2, Spin = 3, Separate = 4 };
    [RequireComponent(typeof(Rigidbody2D))]
    public class InstantiateBall : MonoBehaviour
    {
        [Header("Attritubes")]
        [SerializeField]
        float maxSpeed = 5;
        public float SpeedUp { set { maxSpeed += value; } }
        [SerializeField]
        float hitSpeed = 0;
        [SerializeField]
        float constantSpeed = 2;


        [Header("Variables")]
        Vector2 movement = Vector2.zero;
        public Vector2 Movement { get { return movement; } }
        float time;

        GameManager gm;
        Rigidbody2D rb;

        public void Move(Vector2 velocity)
        {
            if (rb == null) return;
            rb.velocity = velocity * constantSpeed;
        }
        public void TowardToMovingDirection()
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player" && movement.magnitude >= 0.35f)
            {
                RecoveryBall();
            }
            switch (other.tag)
            {
                case "Player Sword":
                    movement = transform.position - other.transform.position;
                    hitSpeed = movement.magnitude;
                    break;
            }
            Move(movement * hitSpeed);
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            movement = InstantiateRandomMovement();
            Move(movement);
            gm = FindObjectOfType<GameManager>();
            InvokeRepeating("ConstantSpeedUp", 8f, 0.1f);
        }
        private void Update()
        {
            if (movement != Vector2.zero) TowardToMovingDirection();
            if (constantSpeed >= maxSpeed) constantSpeed = maxSpeed;
            time += Time.deltaTime;
            if (time > 3) RecoveryBall();
        }
        void ConstantSpeedUp()
        {
            constantSpeed += Time.deltaTime;
        }
        public Vector2 InstantiateRandomMovement()
        {
            Vector2 randomMovement;
            float randomX = 0;
            float randomY = 0;
            if (transform.position.x <= -11.65) randomX = Random.Range(0, 5.0f);
            else if (transform.position.x >= 11) randomX = Random.Range(-5.0f, 0);
            else randomX = Random.Range(-5.0f, 5.0f);

            if (transform.position.y <= -5.95) randomY = Random.Range(0, 4.0f);
            else if (transform.position.y >= 8) randomY = Random.Range(-4.0f, 0);
            else randomY = Random.Range(-4.0f, 4.0f);
            randomMovement = new Vector2(randomX, randomY);
            return randomMovement;
        }
        void RecoveryBall()
        {
            FindObjectOfType<GameObjectPool>().Recovery(this.gameObject);
        }
        public void ReUse()
        {
            time = 0;
            CancelInvoke();
            movement = InstantiateRandomMovement();
            Move(movement);
            InvokeRepeating("ConstantSpeedUp", 8f, 0.1f);
        }
    }

}