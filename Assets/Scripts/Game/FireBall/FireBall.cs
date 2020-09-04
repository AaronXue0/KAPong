using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.BallSpace
{
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

        GameManager gm;

        void Update()
        {
            //if (transparency) Transparency();
            //AbilityHandling();
            //Debug.Log(movement);
        }
        void FixedUpdate()
        {
            //if (separate) Separate();
            //if (spin) Spin();
            //if (sinWave) SinWave();
            //else if (movement != Vector2.zero) Move(movement);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            // if (other.tag == "Player")
            // {
            //     if (movement != Vector2.zero && movement.magnitude >= 1f) gm.PlayerHurt();
            // }
            // if (other.tag == "Player Sword" || other.tag == "Enemy Sword") hitCount++;

            // if (other.tag == "" && sinWave) Move(new Vector2(-1, Random.Range(-1, 2)));
            // control.BounceHandling(ref speed, ref movement, transform, other);
            // Move(movement);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            // if (other.collider.tag == "Flag")
            // {
            //     Destroy(rb);
            //     gm.Goal(this.gameObject, other.collider.gameObject);
            //     animator.SetTrigger("explode");
            //     return;
            // }
            // ClearState();
            // bounceCount++;
            // control.BounceHandling(ref speed, ref movement, rb, transform, other);
        }
        // Control control = new Control();
        // Ability ability = new Ability();
        // SeparateBall separateBall;
        // [Header("Components")]
        // Rigidbody2D rb;
        // Animator animator;
        // SpriteRenderer sprite;

        // public void Disregister()
        // {
        //     Destroy(this.gameObject);
        // }
        // public Vector2 GetMovement
        // {
        //     get { return movement; }
        // }
        // public Transform GetBallTransform
        // {
        //     get { return transform; }
        // }
        // public bool GetSinWave
        // {
        //     get { return sinWave; }
        // }
        // public bool GetSeparate
        // {
        //     get { return separate; }
        // }
        // public bool GetSpin
        // {
        //     get { return spin; }
        // }
        // public void SinWave()
        // {
        //     ability.DoSinWave(ref movement, transform, waveFrequency, waveMagnitude);
        //     Move(movement);
        // }

        // public void Separate()
        // {
        //     float randomAngle;
        //     randomAngle = Random.Range(-1f, 1f);
        //     ability.DoSeparate(ref movement, transform, randomAngle);
        //     GameObject go = (GameObject)Instantiate(separateball, new Vector2(transform.position.x, transform.position.y), transform.rotation);
        //     //Instantiate(separateball, new Vector2(transform.position.x+1, transform.position.y+1), Quaternion.Euler(new Vector3(0, 0, 0)));
        //     separateBall = go.GetComponent<SeparateBall>();
        //     initSeparateBall(randomAngle);
        //     Move(movement);
        //     separate = false;
        // }
        // public void initSeparateBall(float angle)
        // {
        //     separateBall.initBall(movement, angle);
        // }
        // public void Transparency()
        // {
        //     if (color.a <= 10) isDecreasing = !isDecreasing;
        //     if (isDecreasing) ability.DoTransparency(ref color, decreaseValue);
        //     else ability.DoOpacity(ref color, increaseValue);
        //     sprite.color = color;
        //     if (color.a >= 255)
        //     {
        //         transparency = false;
        //         isDecreasing = true;
        //     }
        // }
        // public void Divide()
        // {

        // }
        // public void Spin()
        // {
        //     float spinAngle;
        //     spinAngle = 0.5f;
        //     ability.DoSpin(ref movement, transform, spinAngle);
        //     countSpinTime += Time.deltaTime;
        //     if (countSpinTime > HowMuchSpinTime)
        //     {
        //         countSpinTime = 0;
        //         spin = false;
        //     }
        // }
        // public void Move(Vector2 velocity)
        // {
        //     if (rb == null) return;
        //     Vector2 m = (velocity + velocity) * speed;
        //     TowardToMovingDirection();
        //     if (speed > maxSpeed) speed = maxSpeed;
        //     control.DoMove(rb, m);
        // }
        // public void TowardToMovingDirection()
        // {
        //     float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        //     transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // }
        // public void ClearState()
        // {
        //     sinWave = false;
        //     separate = false;
        //     spin = false;
        // }
        // void Update()
        // {
        //     if (transparency) Transparency();
        //     AbilityHandling();
        //     //Debug.Log(movement);
        // }
        // void FixedUpdate()
        // {
        //     if (separate) Separate();
        //     if (spin) Spin();
        //     if (sinWave) SinWave();
        //     else if (movement != Vector2.zero) Move(movement);
        // }
        // void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.tag == "Player")
        //     {
        //         if (movement != Vector2.zero && movement.magnitude >= 1f) gm.PlayerHurt();
        //     }
        //     if (other.tag == "Player Sword" || other.tag == "Enemy Sword") hitCount++;

        //     if (other.tag == "" && sinWave) Move(new Vector2(-1, Random.Range(-1, 2)));
        //     control.BounceHandling(ref speed, ref movement, transform, other);
        //     Move(movement);
        // }
        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     if (other.collider.tag == "Flag")
        //     {
        //         Destroy(rb);
        //         gm.Goal(this.gameObject, other.collider.gameObject);
        //         animator.SetTrigger("explode");
        //         return;
        //     }
        //     ClearState();
        //     bounceCount++;
        //     control.BounceHandling(ref speed, ref movement, rb, transform, other);
        // }
        // void Awake()
        // {
        //     SetComponents();
        // }
        // void SetComponents()
        // {
        //     rb = GetComponent<Rigidbody2D>();
        //     animator = GetComponent<Animator>();
        //     sprite = GetComponent<SpriteRenderer>();
        // }
        // void Start()
        // {
        //     gm = FindObjectOfType<GameManager>();
        //     //maxSpeed = 4;
        // }
        // void AbilityHandling()
        // {
        //     if (hitCount % hitCountTriggerAbilityTimes != 0 && bounceCount % bounceCountTriggerAbilityTimes != 0) isDoingAbility = false;
        //     if (hitCount == 0 || bounceCount == 0 || isDoingAbility) return;
        //     if (hitCount % hitCountTriggerAbilityTimes == 0 ||
        //         bounceCount % bounceCountTriggerAbilityTimes == 0)
        //     {
        //         isDoingAbility = true;
        //         gm.ShuffleFireBallAbility();
        //     }
        // }
    }

}