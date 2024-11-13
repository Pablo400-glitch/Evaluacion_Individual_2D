using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{  
    public float velocity = 5f; // Velocidad de Movimiento
    public float thrust = 5f; // Potencia de Salto
    public int playerHealth = 3;
    public GameObject weapon;
    public AudioSource ambientSound;
    public AudioSource weaponSound;

    private bool isJumping = false;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public delegate void ItemCollected(int totalItemsCollected);
    public event ItemCollected onItemCollected;

    public delegate void HealthLost(int damage);
    public event HealthLost onHealthLost;

    private int itemsCollected = 0;
    private int healtLost;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healtLost = playerHealth;
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float moveH = Input.GetAxis("Horizontal");

        if (Input.GetButton("Jump") && !isJumping)
        {
            rb2D.AddForce(transform.up * thrust);
            isJumping = true;
            animator.SetBool("Jump", true);
        }

        if (Input.GetKey(KeyCode.X))
        {
            weapon.SetActive(true);
            weaponSound.Play();
        }
        else
        {
            weapon.SetActive(false);
        }

        if (moveH < 0 || moveH > 0)
            animator.SetBool("Walk", true);
        else
        {
            animator.SetBool("Walk", false);
        }

        if (moveH > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveH < 0)
        {
            spriteRenderer.flipX = false;
        }

        Vector2 vtranslate = new Vector2(moveH * velocity * Time.deltaTime, 0);
        rb2D.MovePosition(rb2D.position + vtranslate);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
            animator.SetBool("Jump", false);
        }
    }

    private void AddItem(Collider2D item)
    {
        itemsCollected++;
        if (onItemCollected != null)
        {
            onItemCollected(itemsCollected);
        }
        onItemCollected.Invoke(itemsCollected);
        Destroy(item.gameObject);
    }

    private void Damage()
    {
        healtLost--;
        if (onHealthLost != null)
        {
            onHealthLost(healtLost);
        }
        onHealthLost.Invoke(healtLost);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            AddItem(collision);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }

        if (collision.gameObject.CompareTag("Treasure"))
        {
            Time.timeScale = 0;
            ambientSound.Stop();
        }
    }
}
