using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private Enemy_Behaviour movement;
    

    

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        

    }

    void Update()
    {
        
        
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        GetComponent<Enemy_Behaviour>().moveSpeed = 0;
        StartCoroutine(ResetMoveSpeedAfterDelay(0.4f));
        
        

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private IEnumerator ResetMoveSpeedAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay); // Wait for the specified delay
    GetComponent<Enemy_Behaviour>().moveSpeed = 1; // Set the move speed back to 1
}



    void Die()
    {
        Debug.Log("Enemy Died!");

        animator.SetBool("IsDead", true);

        rb2d.gravityScale = 0; //to asign
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        
        
        

    }

    

    // Update is called once per frame
    
}
