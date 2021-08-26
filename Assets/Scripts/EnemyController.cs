using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public int MaxHealth = 100;
    public int CurrentHealth;

    public float StoppingDistance;
    public float RetreatDistance;

    private Transform Player;

    private bool IsFacingRight = true;

    public float StartTimeBtwShots;
    private float TimeBtwShots;
    public Transform ShootingPoint;
    public GameObject Projectile;

    public bool IsBoss = false;
    public float StartTimeBtwShots1;
    private float TimeBtwShots1;
    public float StartTimeBtwShots2;
    private float TimeBtwShots2;
    public float StartTimeBtwShots3;
    private float TimeBtwShots3;
    public float StartTimeBtwShots4;
    private float TimeBtwShots4;
    public float StartTimeBtwShots5;
    private float TimeBtwShots5;

    public Transform ShootingPoint1;
    public Transform ShootingPoint2;
    public Transform ShootingPoint3;
    public Transform ShootingPoint4;
    public Transform ShootingPoint5;

    public GameObject Projectile1;
    public GameObject Projectile2;
    public GameObject Projectile3;
    public GameObject Projectile4;
    public GameObject Projectile5;

    public int Phase2;
    public int Phase3;
    private Animator animator;

    public HealthBar BossHealthBar;

    public GameObject Head;


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        TimeBtwShots = StartTimeBtwShots;

        TimeBtwShots1 = StartTimeBtwShots1;
        TimeBtwShots2 = StartTimeBtwShots2;
        TimeBtwShots3 = StartTimeBtwShots3;
        TimeBtwShots4 = StartTimeBtwShots4;
        TimeBtwShots5 = StartTimeBtwShots5;
        if (IsBoss == true)
        {
            BossHealthBar = GetComponent<HealthBar>();
            BossHealthBar = GameObject.Find("BossHealthBar").GetComponent<HealthBar>();
            BossHealthBar.SetMaxHealth(MaxHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            return;
        }
        
        Move();
        CheckDirection();
        Shoot();
        
    }

    void Move()
    {
        if (Vector2.Distance(transform.position, Player.position) > StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, Player.position) < StoppingDistance && Vector2.Distance(transform.position, Player.position) > RetreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, Player.position) < RetreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, -Speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = collision.transform;

            if (IsBoss == true)
            {
                BossHealthBar.GetComponent<Animator>().SetTrigger("Appear");
                BossHealthBar.GetComponent<Animator>().SetBool("Idle", true);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player = null;

            if (IsBoss == true)
            {
                BossHealthBar.GetComponent<Animator>().SetTrigger("Dissappear");
                BossHealthBar.GetComponent<Animator>().SetBool("Hidden", true);
            }
        }
    }

    void CheckDirection()
    {
        if (Player.transform.position.x < gameObject.transform.position.x && IsFacingRight)
            Flip();
        if (Player.transform.position.x > gameObject.transform.position.x && !IsFacingRight)
            Flip();
    }
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }

    public void TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
        if (IsBoss == true)
        {
            BossHealthBar.SetHealth(CurrentHealth);
        }
    }
    void Die()
    {
        Destroy(gameObject);
        if (IsBoss == true)
        {
            BossHealthBar.GetComponent<Animator>().SetBool("Idle", false);
            BossHealthBar.GetComponent<Animator>().SetTrigger("Dissappear");
            Instantiate(Head, transform.position, Quaternion.identity);
        }
    }

    void Shoot()
    {
        if (IsBoss == false)
        {
            if (TimeBtwShots <= 0)
            {
                Instantiate(Projectile, ShootingPoint.position, Quaternion.identity);
                TimeBtwShots = StartTimeBtwShots;
            }
            else
            {
                TimeBtwShots -= Time.deltaTime;
            }
        }
        else
        {
            animator = GetComponent<Animator>();
            if (CurrentHealth > Phase2)
            {
                if (TimeBtwShots1 <= 0)
                {
                    Instantiate(Projectile1, ShootingPoint1.position, Quaternion.identity);
                    TimeBtwShots1 = StartTimeBtwShots1;
                }
                else
                {
                    TimeBtwShots1 -= Time.deltaTime;
                }
            }
            if (CurrentHealth <= Phase2)
            {
                animator.SetTrigger("Phase2");
                if (TimeBtwShots2 <= 0)
                {
                    Instantiate(Projectile2, ShootingPoint2.position, Quaternion.identity);
                    TimeBtwShots2 = StartTimeBtwShots2;
                }
                else
                {
                    TimeBtwShots2 -= Time.deltaTime;
                }

                if (TimeBtwShots3 <= 0)
                {
                    Instantiate(Projectile3, ShootingPoint3.position, Quaternion.identity);
                    TimeBtwShots3 = StartTimeBtwShots3;
                }
                else
                {
                    TimeBtwShots3 -= Time.deltaTime;
                }

                if (TimeBtwShots4 <= 0)
                {
                    Instantiate(Projectile4, ShootingPoint2.position, Quaternion.identity);
                    TimeBtwShots4 = StartTimeBtwShots4;
                }
                else
                {
                    TimeBtwShots4 -= Time.deltaTime;
                }

                if (TimeBtwShots5 <= 0)
                {
                    Instantiate(Projectile5, ShootingPoint5.position, Quaternion.identity);
                    TimeBtwShots5 = StartTimeBtwShots5;
                }
                else
                {
                    TimeBtwShots5 -= Time.deltaTime;
                }
            }
            if (CurrentHealth <= Phase3)
            {
                animator.SetTrigger("Phase3");
                if (TimeBtwShots1 <= 0)
                {
                    Instantiate(Projectile1, ShootingPoint1.position, Quaternion.identity);
                    TimeBtwShots1 = StartTimeBtwShots1;
                }
                else
                {
                    TimeBtwShots1 -= Time.deltaTime;
                }

                if (TimeBtwShots2 <= 0)
                {
                    Instantiate(Projectile2, ShootingPoint2.position, Quaternion.identity);
                    TimeBtwShots2 = StartTimeBtwShots2;
                }
                else
                {
                    TimeBtwShots2 -= Time.deltaTime;
                }

                if (TimeBtwShots3 <= 0)
                {
                    Instantiate(Projectile3, ShootingPoint3.position, Quaternion.identity);
                    TimeBtwShots3 = StartTimeBtwShots3;
                }
                else
                {
                    TimeBtwShots3 -= Time.deltaTime;
                }

                if (TimeBtwShots4 <= 0)
                {
                    Instantiate(Projectile4, ShootingPoint2.position, Quaternion.identity);
                    TimeBtwShots4 = StartTimeBtwShots4;
                }
                else
                {
                    TimeBtwShots4 -= Time.deltaTime;
                }

                if (TimeBtwShots5 <= 0)
                {
                    Instantiate(Projectile5, ShootingPoint5.position, Quaternion.identity);
                    TimeBtwShots5 = StartTimeBtwShots5;
                }
                else
                {
                    TimeBtwShots5 -= Time.deltaTime;
                }
            }
        }
    }
}
