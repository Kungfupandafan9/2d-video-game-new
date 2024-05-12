using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField]private BoxCollider2D boxCollider;
    [SerializeField]private LayerMask PlayerLayer;

    private PlayerMovement health;

    public Animator anim;

    void start()
    {
        anim = GetComponent<Animator>();
        
    }

    void update()
    {
        cooldownTimer += Time.deltaTime;
        //attack only when player is in sight
        if(PlayerInSight())
        {
            
            
            cooldownTimer = 0;
            anim.SetTrigger("meleeAttack");
            //Attack

            

        }

        

    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0);

        


        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            //Damage player health
        }
    }
}
