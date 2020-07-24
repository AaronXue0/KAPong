using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Role.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Header("Attritubes")]
        [SerializeField]
        float speed;
        [SerializeField]
        float strength;
        [SerializeField]
        float[] border;

        [Header("Components")]
        GameManager gm;
        Control control = new Control();
        PlayerControls im;
        Rigidbody2D rb;
        Animator animator;

        [Header("Variables")]
        Vector2 movement = Vector2.zero;
        float startAttackTime = 1f;
        float attackTime = 0f;

        public void Move()
        {
            control.DoMove(rb, movement);
        }
        private void Awake()
        {
            im = new PlayerControls();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            gm = FindObjectOfType<GameManager>();
        }
        private void Update()
        {
            if (attackTime > 0) attackTime -= Time.deltaTime;
            if (BallTransformInBorder())
            {
                float distance = Vector2.Distance(transform.position, gm.GetBallTransform().position);
                if (distance < 2f + gm.GetBallMovement().magnitude)
                {
                    DoAttack();
                }
                else transform.position = Vector3.MoveTowards(transform.position, 
                                                            new Vector3(2f, -0.5f, 0f) + predictedPosition(gm.GetBallTransform().position, transform.position, gm.GetBallMovement(), speed), 
                                                            speed * Time.deltaTime);
            }
        }
        private void DoAttack()
        {
            if (attackTime > 0) return;
            attackTime = startAttackTime;
            control.DoAnimator(animator, "attack");
        }
        private bool BallTransformInBorder()
        {
            Vector2 ballPos = gm.GetBallTransform().position;
            if (border[0] > ballPos.y && border[1] > ballPos.x && border[2] < ballPos.y && border[3] < ballPos.x)
            {
                return true;
            }
            return false;
        }
        private Vector3 predictedPosition(Vector3 targetPosition, Vector3 shooterPosition, Vector3 targetVelocity, float projectileSpeed)
        {
            Vector3 displacement = targetPosition - shooterPosition;
            float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;
            //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
            if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > projectileSpeed && Mathf.Sin(targetMoveAngle) / projectileSpeed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
            {
                Debug.Log("Position prediction is not feasible.");
                return targetPosition;
            }
            //also Sine Formula
            float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
            return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
        }
    }

}