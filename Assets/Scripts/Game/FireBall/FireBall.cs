using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
    public enum AbilityState { None, SineWave, Transparency, Spin, Separate };
    [RequireComponent(typeof(Rigidbody2D))]
    public class FireBall : MonoBehaviour
    {
        [Header("Attritubes")]
        [SerializeField]
        float maxSpeed = 0;
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

        [Header("Variables")]
        Vector2 movement = Vector2.zero;
        public Vector2 Movement { get { return movement; } }
        public bool isDoingAbility = false;
        public bool IsDoingAbility { get { return isDoingAbility; } }
        public AbilityState state;

        [Header("Components")]
        Control control = new Control();
        Ability ability;
        GameManager gm;
        Rigidbody2D rb;

        public void SineWave()
        {
            isDoingAbility = true;
            state = AbilityState.SineWave;
            StartCoroutine(ability.DoEarse(sineDuration, callbackEarse));
        }
        public void callbackEarse()
        {
            isDoingAbility = false;
            state = AbilityState.None;
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
            if (other.tag == "Player" && movement.magnitude >= 0.3f) gm.PlayerHurt();
            if (other.tag == "Player Sword" || other.tag == "Enemy Sword") hitCount++;
            control.BounceHandling(ref hitSpeed, ref movement, transform, other);
            Move(movement * hitSpeed);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            bounceCount++;
            control.BounceHandling(ref hitSpeed, ref movement, transform, other);
        }
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            ability = GetComponent<Ability>();
        }
        private void Start()
        {
            gm = FindObjectOfType<GameManager>();
            InvokeRepeating("SpeedUp", 0f, 0.1f);
        }
        private void FixedUpdate()
        {
            switch (state)
            {
                case AbilityState.SineWave:
                    Move(movement + ability.DoSineWave());
                    break;
                case AbilityState.Transparency:
                    break;
                case AbilityState.Spin:
                    break;
                case AbilityState.Separate:
                    break;
                default:
                    if (movement != Vector2.zero) Move(movement);
                    break;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K)) SineWave();
            if (movement != Vector2.zero) TowardToMovingDirection();
            if (constantSpeed >= maxSpeed) constantSpeed = maxSpeed;
        }
        void SpeedUp()
        {
            constantSpeed += Time.deltaTime;
        }
    }

}