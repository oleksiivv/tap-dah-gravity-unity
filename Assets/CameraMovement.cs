using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;


    void Start(){
        offset=transform.position-player.transform.position;
    }

    void Update(){
        transform.position=new Vector3((player.transform.position+offset).x, transform.position.y, transform.position.z);
    }
}
