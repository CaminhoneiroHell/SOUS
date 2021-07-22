using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalZero.Core
{
    public class HostileBehaviour : MonoBehaviour
    {
        Animator animator;
        [SerializeField]GameObject weapon;

        float lockZAxisRef;
        void Start()
        {
            animator = GetComponent<Animator>();
            lockZAxisRef = transform.position.z;
        }
        void Hit_AnimationEvent()
        {

        }

        private void Update()
        {
            //Lock Z axis
            //gameObject.transform.position = new Vector3(transform.position.x,
            //    transform.position.y,
            //    lockZAxisRef);

            #region OldInputs
            if (Input.GetKeyDown(KeyCode.A))
            {
                TalhoTrigger();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                FendenteTrigger();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                CPernaTrigger();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                RevCPernaTrigger();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                FlancoTrigger();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                RevFlancoTrigger();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChefTrigger();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RevChefTrigger();
            }
            #endregion
        }

        public void RevChefTrigger()
        {
            animator.SetTrigger("AtkRevChef");
        }

        public void ChefTrigger()
        {
            animator.SetTrigger("AtkChef");
        }

        public void RevFlancoTrigger()
        {
            animator.SetTrigger("AtkRevFlanco");
        }

        public void FlancoTrigger()
        {
            animator.SetTrigger("AtkFlanco");
        }

        public void RevCPernaTrigger()
        {
            animator.SetTrigger("AtkRevCortePerna");
        }

        public void CPernaTrigger()
        {
            animator.SetTrigger("AtkCortePerna");
        }

        public void FendenteTrigger()
        {
            animator.SetTrigger("AtkFendente");
        }

        public void TalhoTrigger()
        {
            animator.SetTrigger("AtkTalho");
        }

        #region Broadsword singing

        #endregion


        #region events triggered by character animation 
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
        #endregion
    }

}
