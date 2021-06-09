using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalZero.Core
{
    public class HostileBehaviour : MonoBehaviour
    {
        Animator animator;
        [SerializeField]GameObject weapon;
        void Start()
        {
            animator = GetComponent<Animator>();
        }
        void Hit_AnimationEvent()
        {

        }

        private void Update()
        {
            //Lock Z axis
            gameObject.transform.position = new Vector3(transform.position.x,
                transform.position.y,
                10.411f);

            if (Input.GetKeyDown(KeyCode.A)){
                animator.SetTrigger("AtkTalho");
            }
            if (Input.GetKeyDown(KeyCode.S)){
                animator.SetTrigger("AtkFendente");
            }
            if (Input.GetKeyDown(KeyCode.D)){ 
                animator.SetTrigger("AtkCortePerna");
            }
            if (Input.GetKeyDown(KeyCode.F)){ 
                animator.SetTrigger("AtkRevCortePerna");
            }
            if (Input.GetKeyDown(KeyCode.Q)){ 
                animator.SetTrigger("AtkFlanco");
            }
            if (Input.GetKeyDown(KeyCode.W)){ 
                animator.SetTrigger("AtkRevFlanco");
            }
            if (Input.GetKeyDown(KeyCode.E)){ 
                animator.SetTrigger("AtkChef");
            }
            if (Input.GetKeyDown(KeyCode.R)){ 
                animator.SetTrigger("AtkRevChef");
            }
        }

        void SetDefaultLayerAnimationEvent(){
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        void SetTalhoLayerAnimationEvent(){
            gameObject.layer = LayerMask.NameToLayer("Talho");
        }

        void SetFendenteLayerAnimationEvent(){
            gameObject.layer = LayerMask.NameToLayer("Fendente");            
        }
        void SetCortePernaLayerAnimationEvent(){
            gameObject.layer = LayerMask.NameToLayer("CortePerna");            
        }
        void SetRevCortePernaLayerAnimationEvent(){
            gameObject.layer = LayerMask.NameToLayer("RevCortePerna");            
        }
        void SetFlancoLayerAnimationEvent(){
            gameObject.layer = LayerMask.NameToLayer("Flanco");            
        }
        void SetRevFlancoLayerAnimationEvent(){
            gameObject.layer = LayerMask.NameToLayer("RevFlanco");            
        }
        void SetChefLayerAnimationEvent(){
            gameObject.layer = LayerMask.NameToLayer("Chef");            
        }
        void SetRevChefLayerAnimationEvent(){
            gameObject.layer = LayerMask.NameToLayer("RevChef");            
        }

    }

}
