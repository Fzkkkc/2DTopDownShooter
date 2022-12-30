using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public GameObject floatingDamage;

    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public int health;
    public float speed;
    public GameObject deathEffect;
    public GameObject deathEffectPlayer;
    public int damage;
    private float stopTime;
    public float startStopTime;

    private Player Player;
    private Animator anim;
    public GameObject sound;
    private Animator camAnim;
    private addRoom room;

    [HideInInspector] public bool playerNotInRoom;
    private bool stopped;

    private void Start()
    {
        anim = GetComponent<Animator>();
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        Player = FindObjectOfType<Player>();
        room = GetComponentInParent<addRoom>();
    }
    private void Update()
    {
        if (!playerNotInRoom)
        {
            if (stopTime <= 0)
            {
                stopped = false;
            }
            else
            {
                stopped = true;
                stopTime -= Time.deltaTime;
            }
        }
        else
        {
            stopped = true;
        }
        

        if (health <=0)
        {          
            Destroy(gameObject);
            room.enemies.Remove(gameObject);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }


        if (Player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (!stopped)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }

       

    }

    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
        Vector2 damagePos = new Vector2(transform.position.x, transform.position.y + 2.75f);
        Instantiate(floatingDamage, damagePos, Quaternion.identity);
        floatingDamage.GetComponentInChildren<FloatingDamage>().damage = damage;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            if ( timeBtwAttack <=0)
            {
                anim.SetTrigger("attackEnemy");
                camAnim.SetTrigger("shake");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    public void OnEnemyAttack()
    {
        Instantiate(deathEffectPlayer, Player.transform.position, Quaternion.identity);
        Instantiate(sound, transform.position, Quaternion.identity);
        Player.ChangeHealth(-damage);
        timeBtwAttack = startTimeBtwAttack;

    }

}
