using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]public float speed;

    [SerializeField]public float jumpHeight;


    
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;


    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform attackPoint;

    public float attackRate = 0.5f;

    
    
    float nextAttackTime = 0f;
    private float movementCooldown = 0.4f; // Cooldown time in seconds
    private float cooldownTimer = 0.0f; // Timer to track cooldown
    


   
   void Awake() // Awake is better than start if you want to create references between other things, ie in this case referencing the tag enemy
{
    
    // Ignore collisions with objects tagged as "Enemy"
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //declares the gameObject enemies(variable name) to be any gameobject with the tag Enemy
    foreach (GameObject enemy in enemies) // in english terms, this code says for every gameObject enemy(another variable) that is in enemies (saying if the GameObject enemy has a tag Enemy)
    {
        Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // this code gets the colliders of one enemy and another enemy and simply ignores the collisions
    }

    // Rest of your Awake code...
}


    
    // Start is called before the first frame update
    void Start()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Enemy");     
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // same as before
        // Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer <= 0)
        {
            float horizontalInput = Input.GetAxis("Horizontal"); // getaxis gets input of input control, in this case horizontal movement, being A and D, the value ranges from 1 to -1
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); // body.velocity is rate of change of rigidbody. Using New Vector2 (vectors control player position), we are saying the velocity is equal to the vector. First parameter is x direction, which takes horizontal input (needed for direction because A and D correspond to -1 and 1) * speed. This means that
        // flip the player when moving left-right
            if(horizontalInput > 0.01f) // bascially just saying if input is D because value of the input of D is 1 with getAxis("Horizontal")
            {
                transform.localScale = Vector3.one; // Vector3.one sets x y and z coordinates to be 1. This leads to the player in every point facing right, the positive direction because east is normaly notated as positive in physics
            }
            else if(horizontalInput < -0.01f) // same as before, just saying that if input is A because value of A is -1
            {
                transform.localScale = new Vector3(-1, 1, 1); // keeps everything the same except x point, flipping it to the west, because west is normally notated to be negative in physics
            }
            if(Input.GetKey(KeyCode.Space) && grounded) // if input is the spacebar and the grounded boolean is true
            {
                Jump();
            }
            
        
        
        

        // set animator parameters
            anim.SetBool("run", horizontalInput != 0); // first parameter is just using the animator setup in unity to control which animation we are setting parameters for, second parameter is saying if horizontalInput isnt equal to 0, then the animation is true. 
            anim.SetBool("grounded", grounded);

        }
            
            

        
        

        // set animator parameters
            
        
        if(Time.time >= nextAttackTime)
            {
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    anim.SetTrigger("attack");

                
                    nextAttackTime = Time.time + 0.6f / attackRate;
                }

            }
       
        

        
    }

    

    
    private void Jump()
    {
        
        body.velocity = new Vector2(body.velocity.x, jumpHeight); // keeps x value as the same, but wants to change y value by jump height. IE if jump height was 5, the y velocity will change by 5, therefore would move up
        anim.SetTrigger("jump"); // animations
        
        grounded = false; // so that you can't do multiple jumps in the air
        
    }
    private IEnumerator SetGroundedWithDelay()
{
    yield return new WaitForSeconds(0f); // Wait for 1 second
    grounded = true; // Set grounded to true after the delay 
}

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.tag == "ground") // checks if the players collision detects  the ground, goes to the IEnumerator
    {
        StartCoroutine(SetGroundedWithDelay()); // Start the coroutine
    }
}

    public void ResetCooldownTimer()
{
    cooldownTimer = movementCooldown;
}


}
