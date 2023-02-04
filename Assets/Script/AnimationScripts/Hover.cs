using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Animator animator;

    void Start()
    {
       animator = GetComponent<Animator>();

    }
     
     void Update()
     {
     }

    public void OnPointerEnter(PointerEventData eventData)
    {
         animator.SetBool("IsHovered",true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
         animator.SetBool("IsHovered",false);
         
    }

    void OnDisable(){
          animator.SetBool("IsHovered",false);
    }


}
