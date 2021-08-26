using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    private Transform Player;
    private Vector2 Target;

    private bool IsFacingRight = true;

    public int Damage;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Target = new Vector2(Player.position.x, Player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
        if (transform.position.x == Target.x && transform.position.y == Target.y)
        {
            Destroy(gameObject);
        }
        CheckDirection();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player.GetComponent<PlayerController>().TakeDamage(Damage);
        }
        if (collision.CompareTag("Block"))
        {
            Destroy(gameObject);
            Debug.Log("Blocked");
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
}
