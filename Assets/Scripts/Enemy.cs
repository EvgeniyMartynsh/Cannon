using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected virtual int Health { get; set; } = 100;
    protected virtual float Speed { get; set; } = 1f;
    protected virtual int Coins { get; set; } = 1;
    protected virtual int Damage { get; set; } = 10;

    protected Vector2 playerPosition = new Vector2(0.24f, 2.03f);

    public Rigidbody2D enemyRb;


    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        RenameEnemyGameObject();
    }

    private void Update()
    {
        //Move();
    }



    protected virtual void Move()
    {
        Vector2 position2d = transform.position;

        Vector2 direction = playerPosition - position2d;
        direction.Normalize();

        transform.rotation = Quaternion.FromToRotation(Vector2.up, direction);

        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cannon"))
        {
            Destroy(gameObject);
            GameManager.PlayerHealth -= Damage;

        }

        else if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Cannon.isDistanceToNearestTargenInFireRange = false;
        }

        GameManager.PlayerScore += Coins;

    }

    void RenameEnemyGameObject()
    {
        if (transform.childCount == 0)
        {
            gameObject.name = GameManager.EnemyCount.ToString();
            Debug.Log(gameObject.name);
        }

        else
        {
            gameObject.name = GameManager.EnemyCount.ToString();
            gameObject.transform.GetChild(0).name = GameManager.EnemyCount.ToString();
            Debug.Log(gameObject.name + " " + gameObject.transform.GetChild(0).name);
        }
    }



}
