using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour
{
    private int dir;

    public Vector3 axis;

    void Start(){
        if(axis.y>0){
            Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.AddComponent<BoxCollider>();
        }
        dir=1;
        StartCoroutine(changeDir());
    }

    void Update(){
        transform.Translate(axis*dir);
    }



    IEnumerator changeDir(){
        while(true){
            yield return new WaitForSeconds(1.75f);
            dir*=-1;
        }
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.name.ToUpper().Contains("PLAYER")){
            other.gameObject.transform.parent=gameObject.transform;
        }
    }

    void OnCollisionExit(Collision other){
        if(other.gameObject.name.ToUpper().Contains("PLAYER")){
            other.gameObject.transform.parent=null;
        }
    }
}
