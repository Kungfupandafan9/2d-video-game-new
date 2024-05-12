using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    public HealthBar healthBar;

    public PlayerMovement movespeed;

    

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        

    }

    void Update()
    {
        
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        GetComponent<PlayerMovement>().speed = 0;
        StartCoroutine(ResetMoveSpeedAfterDelay(0.4f));

        

        GetComponent<PlayerMovement>().ResetCooldownTimer();


        animator.SetTrigger("hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
        
    }
        private IEnumerator ResetMoveSpeedAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay); // Wait for the specified delay
    GetComponent<PlayerMovement>().speed = 5; // Set the move speed back to 1
}



    void Die()
    {
        Debug.Log("Player Died!");

        animator.SetBool("Dead", true);

        rb2d.gravityScale = 0; //to asign
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        

    }
    

    

    // Update is called once per frame
    
}
