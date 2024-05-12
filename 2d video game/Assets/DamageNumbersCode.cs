using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class DamageNumbersCode : MonoBehaviour


{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "HitPoint")
        {
            anim.SetTrigger("DamageTrigger");
        }
    }
    
}
