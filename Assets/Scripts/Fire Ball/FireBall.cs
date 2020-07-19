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
        [Header("Abilities Attritubes")]
        [SerializeField]
        float hitCount;
        [SerializeField]
        float bounceCount;
        [SerializeField]
        float hitCountTriggerAbilityTimes;
        [SerializeField]
        float bounceCountTriggerAbilityTimes;
        [Header("Abilities: Sin Wave")]
        [SerializeField]
        public bool sinWave;
        [SerializeField]
        float waveMagnitude = 0.5f;
        [SerializeField]
        float waveFrequency = 20f;
        [Header("Abilities: Transparency")]
        [SerializeField]
        public bool transparency;
        [SerializeField]
        byte increaseValue = 5;
        [SerializeField]
        byte decreaseValue = 5;
        [SerializeField]
        bool isDecreasing = true;
        [SerializeField]
        Color32 color;
        [Header("Classes")]
        GameManager gm;
        Control control = new Control();
        Ability ability = new Ability();
        [Header("Components")]
        Rigidbody2D rb;
        Animator animator;
        SpriteRenderer sprite;
        [Header("Variables")]
        Vector2 movement = Vector2.zero;
        bool isDoingAbility = false;

        public void Disregister()
        {
            Destroy(this.gameObject);
        }
        public Vector2 GetMovement
        {
            get
            {
                return movement;
            }
        }
        public void SinWave()
        {
            ability.DoSinWave(ref movement, transform, waveFrequency, waveMagnitude);
            Move(movement);
        }
        public void Transparency()
        {
            if (color.a <= 10) isDecreasing = !isDecreasing;
            if (isDecreasing) ability.DoTransparency(ref color, decreaseValue);
            else ability.DoOpacity(ref color, increaseValue);
            sprite.color = color;
            if (color.a >= 255)
            {
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
            if(rb == null) return;
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
            if (transparency) Transparency();
            AbilityHandling();
        }
        void FixedUpdate()
        {
            if (sinWave) SinWave();
            else if (movement != Vector2.zero) Move(movement);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                if (movement != Vector2.zero && movement.magnitude >= 1f) gm.PlayerHurt();
            }
            if (other.tag == "Player Sword") hitCount++;
            control.BounceHandling(ref speed, ref movement, transform, other);
            Move(movement);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.tag == "Flag")
            {
                Destroy(rb);
                gm.Goal(this.gameObject, other.collider.gameObject); 
                animator.SetTrigger("explode");
                return;
            }
            ClearState();
            bounceCount++;
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
        void Start()
        {
            gm = FindObjectOfType<GameManager>();
        }
        void AbilityHandling()
        {
            if (hitCount % hitCountTriggerAbilityTimes != 0 && bounceCount % bounceCountTriggerAbilityTimes != 0) isDoingAbility = false;
            if (hitCount == 0 || bounceCount == 0 || isDoingAbility) return;
            if (hitCount % hitCountTriggerAbilityTimes == 0 ||
                bounceCount % bounceCountTriggerAbilityTimes == 0)
            {
                isDoingAbility = true;
                gm.ShuffleFireBallAbility();
            }
        }
    }

}