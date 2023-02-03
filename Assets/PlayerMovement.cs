using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    private RigidbodyConstraints rbStartConstraints;

    public float speed;
    private int dir=1;

    private bool started=false;
    public GameObject startPanel;
    public PlayerAnimator animator;

    void Start(){
        startPanel.SetActive(true);
        
        if(rb==null){
            rb=gameObject.GetComponent<Rigidbody>();
        }

        rbStartConstraints=rb.constraints;
        started=false;
        animator.sit();
    }


    void Update(){
        if(started)transform.Translate(new Vector3(1,0,0)*speed*Time.timeScale*-1/80);

        transform.position=new Vector3(transform.position.x, transform.position.y, 0.36f);
    }


    void OnCollisionEnter(Collision other){
        rb.useGravity=false;
        rb.velocity=Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    void OnCollisionExit(Collision other){
        rb.useGravity=true;
        rb.constraints=rbStartConstraints;
        //transform.Rotate(-180,0,0);
    }

    public void makeMovement(){
        if(!started){
            started=true;
            startPanel.SetActive(false);
            animator.run();
            return;
        }
        if(dir%2==1){
            rb.AddForce(Vector3.up*12, ForceMode.Impulse);
            transform.Rotate(180,0,0);
        }
        else{
            rb.useGravity=true;
            rb.AddForce(Vector3.up*-12, ForceMode.Impulse);
            transform.Rotate(-180,0,0);
        }

        dir++;
    }
}
