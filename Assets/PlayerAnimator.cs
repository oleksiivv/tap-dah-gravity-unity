using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    public GameObject[] skins;

    void Start(){
        foreach (var item in skins)
        {
            item.SetActive(false);
        }

        skins[PlayerPrefs.GetInt("current",0)].SetActive(true);

        anim=skins[PlayerPrefs.GetInt("current",0)].GetComponent<Animator>();
    }


    public void run(){
        anim.SetBool("sit",false);
        anim.SetBool("idle",false);
        anim.SetBool("run",true);
    }

    public void idle(){
        anim.SetBool("run",false);
        anim.SetBool("idle",true);
    }

    public void sit(){
        anim.SetBool("run",false);
        anim.SetBool("idle",false);
        anim.SetBool("sit",true);
    }

    public void fall(){
        anim.SetBool("run",false);
        anim.SetBool("idle",false);
        anim.SetBool("fall",true);
    }
}
