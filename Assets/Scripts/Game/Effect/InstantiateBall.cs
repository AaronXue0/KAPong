using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

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
        [Header("Abilities")]
        [SerializeField]
        float hitCount = 0;
        [SerializeField]
        float bounceCount = 0;
        [SerializeField]
        float sineDuration = 5f;

        [SerializeField]
        GameObject goalEffect = null;

        [Header("Variables")]
        Vector2 movement = Vector2.zero;
        public Vector2 Movement { get { return movement; } }
        public bool isDoingAbility = false;
        public bool IsDoingAbility { get { return isDoingAbility; } }
        public AbilityState state;
        float time;
        [SerializeField]
        GameObject insBall;

        GameManager gm;
        Rigidbody2D rb;

        public void callbackEarse()
        {
            state = AbilityState.None;
            Move(movement.normalized * constantSpeed);
            isDoingAbility = false;
        }
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
            if (other.tag == "Player" && movement.magnitude >= 0.35f) { gm.PlayerHurt(); }
            if (other.tag == "Player Sword" || other.tag == "Enemy Sword") hitCount++;
            //control.BounceHandling(ref hitSpeed, ref movement, transform, other);
            switch (other.tag)
            {
                case "Player":
                    if (Mathf.Abs(movement.x) <= 0.2f) return;
                    movement = new Vector2(0.2f, 0);
                    break;
                case "Player Sword":
                    movement = transform.position - other.transform.position;
                    hitSpeed = movement.magnitude;
                    break;
            }
            Move(movement * hitSpeed);
        }
        /*private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag == "Flag")
            {
                if (other.collider.name == "Right")
                {
                    Invoke("Goal", 0.5f);
                    Instantiate(goalEffect, (Vector2)transform.position, Quaternion.identity);
                }
                if (other.collider.name == "Left") gm.LostPoint();
            }
            bounceCount++;
            //control.BounceHandling(ref hitSpeed, ref movement, transform, other);
        }*/
        void Goal()
        {
            gm.Goal(hitSpeed, (int)state);
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            //ability = GetComponent<Ability>();
        }
        private void Start()
        {
            movement = InstantiateRandomMovement();
            Move(movement);
            gm = FindObjectOfType<GameManager>();
            InvokeRepeating("ConstantSpeedUp", 8f, 0.1f);
        }
        private void FixedUpdate()
        {

        }
        private void Update()
        {
            if (movement != Vector2.zero) TowardToMovingDirection();
            if (constantSpeed >= maxSpeed) constantSpeed = maxSpeed;
            time += Time.deltaTime;
            if (time > 10) Destroy(insBall);
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
            else if (transform.position.x >=11) randomX = Random.Range(-5.0f, 0);
            else randomX = Random.Range(-5.0f, 5.0f);

            if (transform.position.y <= -5.95) randomY = Random.Range(0, 4.0f);
            else if (transform.position.y >= 8) randomY = Random.Range(-4.0f, 0);
            else randomY = Random.Range(-4.0f, 4.0f);
            randomMovement = new Vector2(randomX, randomY);
            return randomMovement;
        }
    }

}