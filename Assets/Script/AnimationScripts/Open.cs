using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{

  public Animator animator;

    void Start()
    {
      animator = GetComponent<Animator>();  
    }

    void OnEnable(){

        animator.SetBool("CanvasStart",true);

    }
}
