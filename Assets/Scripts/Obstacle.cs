using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float animationDelay = 0f;
    private Animator anim;

    void Start(){
        anim = GetComponentInParent<Animator>();
        if (anim != null){
            Invoke("DelayAnimation", animationDelay);
        }

    }

    void Update(){
        if(anim != null){
            anim.SetBool("stopMovement", GameManager.instance.StopMovement);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.LoseLife();
        }
    }

    void DelayAnimation(){
        anim.Play("Peak_attack");
    }
}
