using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UniversalZero.Core
{
    public class DefenceBehaviour : MonoBehaviour
    {
        Animator animator;
        LayerMask talho;

        float lockZAxisRef;
        void Start()
        {
            animator = GetComponent<Animator>();
            talho = LayerMask.GetMask("Talho");

            lockZAxisRef = transform.position.z;
        }


        void Update()
        {
            //Lock  Z axis
            gameObject.transform.position = new Vector3(transform.position.x,
                transform.position.y,
                lockZAxisRef);
        }

        public void DefTalho()
        {
            animator.SetTrigger("DefTalho");
        }

        public void DefFendente()
        {
            print("Attacked in DefFendente!");
            animator.SetTrigger("DefFendente");
        }
        public void DefCortePerna()
        {
            print("DefCortePerna defense called");
            animator.SetTrigger("DefCortePerna");
        }
        public void DefRevPerna()
        {
            animator.SetTrigger("DefRevCortePerna");
        }
        public void DefFlanco()
        {
            animator.SetTrigger("DefFlanco");
        }
        public void DefRevFlanco()
        {
            animator.SetTrigger("DefRevFlanco");
        }
        public void DefChef()
        {
            animator.SetTrigger("DefChef");
        }
        public void DefReversoChef()
        {
            animator.SetTrigger("DefRevChef");
        }


        private void OnTriggerEnter(Collider other)
        {
            if((other.gameObject.layer << talho) != 0){
                //print("Attacked in Talho!");
                //animator.SetTrigger("DefTest");
            }
        }
    }
}