using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public string enemyName;
    public float speed;
    public int health, enmeyScore;
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject player;
    public GameObject bulletObjA;
    public GameObject bulletObjB;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }
    
    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0) {
            Player playerLogic = player.GetComponent<Player> ();
            playerLogic.score += enmeyScore;
            Destroy(gameObject);
        }
    }
    void Update()
    {
        Fire();
        Reload();
    }
    void Fire()
    {

        if (curShotDelay < maxShotDelay)
            return;
        if(enemyName == "S") {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
        }
        else if (enemyName == "L") {
            GameObject bulletR = Instantiate(bulletObjB, transform.position +Vector3.right*0.3f, transform.rotation);
            GameObject bulletL = Instantiate(bulletObjB, transform.position+Vector3.left*0.3f, transform.rotation);
            
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position+Vector3.right*0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position+Vector3.left*0.3f);

            rigidR.AddForce(dirVecR.normalized * 2, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 2, ForceMode2D.Impulse);
    

        }
        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border Bullet") 
            Destroy(gameObject);
        
        else if (collision.gameObject.tag == "Player Bullet") {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);
        }

    }


}