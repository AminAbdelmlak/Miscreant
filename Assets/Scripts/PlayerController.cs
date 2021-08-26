using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    private float MoveInput;
    private float HMoveInput;
    private Vector2 MoveVelocity;
    private bool IsRight = true;

    private Rigidbody2D rb;
    private Animator animator;

    private float TimeBtwAttack;
    private float TimeBtwAttackH;
    public float StartTimeBtwAttack;
    public float StartTimeBtwAttackH;
    public Transform AttackPoint;
    public float AttackRange;
    public LayerMask EnemyLayers;
    public int LAttackDamage;
    public int HAttackDamage;

    public GameObject Blocker;

    public float DodgeSpeed;
    private Vector2 MoveVelocityDodge;
    private float TimeBtwDodge;
    public float StartTimeBtwDodge;

    public int Maxhealth = 100;
    public int CurrentHealth;

    public HealthBar healthBar;

    public int Healing = 20;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Blocker.SetActive(false);

        CurrentHealth = Maxhealth;
        healthBar.SetMaxHealth(Maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoveDirection();
        Attack();
        Block();

        MoveInput = MoveVelocity.magnitude;
        HMoveInput = MoveVelocity.x;
        animator.SetFloat("Speed", Mathf.Abs(MoveInput));

        if (Maxhealth == 0)
        {
            animator.SetBool("IsDead", true);
        }
    }
    void FixedUpdate()
    {
        Move();
        Dodge();

        Physics2D.IgnoreLayerCollision(9, 10);
    }

    private void Move()
    {
        Vector2 Moveinput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MoveVelocity = Moveinput.normalized * Speed;

        rb.MovePosition(rb.position + MoveVelocity * Time.deltaTime);
    }

    private void Flip()
    {
        IsRight = !IsRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void CheckMoveDirection()
    {
        if (IsRight && HMoveInput < 0)
        {
            Flip();
        }
        else if (!IsRight && HMoveInput > 0)
        {
            Flip();
        }
    }
    void Attack()
    {
        
        if (TimeBtwAttack <= 0 && TimeBtwAttackH <= 0 && TimeBtwDodge <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
                Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
                foreach(Collider2D Enemy in HitEnemies)
                {
                    Enemy.GetComponent<EnemyController>().TakeDamage(LAttackDamage);
                }

                TimeBtwAttack = StartTimeBtwAttack;
            }
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("AttackH");
                Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
                foreach (Collider2D Enemy in HitEnemies)
                {
                    Enemy.GetComponent<EnemyController>().TakeDamage(HAttackDamage);
                }

                TimeBtwAttackH = StartTimeBtwAttackH;
            }
        }
        else
        {
            TimeBtwAttack -= Time.deltaTime;
            TimeBtwAttackH -= Time.deltaTime;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }
        else
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }
        
    }

    void Dodge()
    {
        if (TimeBtwDodge <= 0)
        {
            if (TimeBtwAttack <= 0 && TimeBtwAttackH <= 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    animator.SetTrigger("Dodge");

                    Vector2 Moveinput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                    MoveVelocityDodge = Moveinput.normalized * DodgeSpeed;
                    rb.MovePosition(rb.position + MoveVelocityDodge * Time.deltaTime);

                    TimeBtwDodge = StartTimeBtwDodge;
                }
            }
            
        }
        else
        {
            TimeBtwDodge -= Time.deltaTime;
        }
    }
    void Block()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("IsBlocking", true);
            Blocker.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("IsBlocking", false);
            Blocker.SetActive(false);
        }
    }
    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        healthBar.SetHealth(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            Invoke("Die", 1.5f);
        }
    }
    void Die()
    {
        SceneManager.LoadScene(4);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bonfire"))
        {
            CurrentHealth += Healing;
            healthBar.SetHealth(CurrentHealth);
            collision.enabled = false;
            Destroy(collision.gameObject);
        }
    }
}
