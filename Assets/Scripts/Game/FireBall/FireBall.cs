using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FireBall : MonoBehaviour
    {
        [Header("Attritubes")]
        [SerializeField]
        float maxSpeed = 0;
        public float MaxSpeed { get { return maxSpeed; } }
        [SerializeField]
        float speed = 0;
        public float Speed { get { return speed; } }

        [Header("Abilities Attritubes")]
        [SerializeField]
        float hitCount = 0;
        public float HitCount { get { return hitCount; } }
        [SerializeField]
        float bounceCount = 0;
        public float BounceCount { get { return bounceCount; } }
        [SerializeField]
        float hitCountTriggerAbilityTimes = 0;
        public float HitCountTriggerAbilityTimes { get { return hitCountTriggerAbilityTimes; } }
        [SerializeField]
        float bounceCountTriggerAbilityTimes = 0;
        public float BounceCountTriggerAbilityTimes { get { return bounceCountTriggerAbilityTimes; } }
        
        [Header("Variables")]
        Vector2 movement = Vector2.zero;
        public Vector2 Movement { get { return movement; } }
        bool isDoingAbility = false;
        public bool IsDoingAbility { get { return isDoingAbility; } }

        [Header("Components")]
        Control control = new Control();
        Rigidbody2D rb;

        public void Move(Vector2 velocity)
        {
            if (rb == null) return;
            if (speed > maxSpeed) speed = maxSpeed;
            TowardToMovingDirection();
            Vector2 m = (velocity + velocity) * speed;
            rb.velocity = m;
        }
        public void TowardToMovingDirection()
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player Sword" || other.tag == "Enemy Sword") hitCount++;
            control.BounceHandling(ref speed, ref movement, transform, other);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            bounceCount++;
            control.BounceHandling(ref speed, ref movement, transform, other);
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            if (movement != Vector2.zero) Move(movement);
        }
    }

}