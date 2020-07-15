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
        [Header("Abilities")]
        [SerializeField]
        bool sinWave;
        [SerializeField]
        float waveMagnitude = 0.5f;
        [SerializeField]
        float waveFrequency = 20f;
        [SerializeField]
        bool transparency;
        [SerializeField]
        byte increaseValue = 5;
        [SerializeField]
        byte decreaseValue = 5;
        [SerializeField]
        bool isDecreasing = true;
        [SerializeField]
        Color32 color;
        [Header("Classes")]
        Control control = new Control();
        Ability ability = new Ability();
        [Header("Components")]
        Rigidbody2D rb;
        Animator animator;
        SpriteRenderer sprite;
        [Header("Variables")]
        Vector2 movement = Vector2.zero;

        public void SinWave()
        {
            ability.DoSinWave(ref movement, transform, waveFrequency, waveMagnitude);
            Move(movement);
        }
        public void Transparency()
        {
            if(color.a <= 10) isDecreasing = !isDecreasing;
            if(isDecreasing) ability.DoTransparency(ref color, decreaseValue);
            else ability.DoOpacity(ref color, increaseValue);
            sprite.color = color;
            if(color.a >= 255) {
                transparency = false;
                isDecreasing = true;
            }
        }
        public void Divide()
        {

        }
        public void Spin()
        {

        }
        public void Move(Vector2 velocity)
        {
            Vector2 m = (velocity + velocity) * speed;
            TowardToMovingDirection();
            if (speed > maxSpeed) speed = maxSpeed;
            control.DoMove(rb, m);
        }
        public void TowardToMovingDirection()
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        public void ClearState()
        {
            sinWave = false;
        }
        void Update()
        {
            if(transparency) Transparency();
        }
        void FixedUpdate()
        {
            if (sinWave) SinWave();
            else if (movement != Vector2.zero) Move(movement);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            control.BounceHandling(ref speed, ref movement, transform, other);
            Move(movement);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            ClearState();
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
            sprite = GetComponent<SpriteRenderer>();
        }
    }

}