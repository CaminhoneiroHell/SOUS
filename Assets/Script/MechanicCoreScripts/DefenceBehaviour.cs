using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalZero.Core
{
    public class DefenceBehaviour : MonoBehaviour
    {
        Animator animator;
        [SerializeField]GameObject target;
        [SerializeField] GameObject weapon;


        float lockZAxisRef;
        void Start()
        {
            animator = GetComponent<Animator>();
            
            lockZAxisRef = transform.position.z;
        }

        [SerializeField] bool defenceFlag;
        public void ParryCaster()
        {
            if (target != null)
            {
                print("Distance: " + (gameObject.transform.position.x - target.transform.position.x));

                if (OnGuard()) defenceFlag = false;
                
                if (ParryMoveReader("Talho")){
                    DefTalho();
                    defenceFlag = true;
                }

                if (ParryMoveReader("Fendente"))
                {   
                    DefFendente();
                    defenceFlag = true;
                }

                if (ParryMoveReader("CortePerna"))
                {
                    DefCortePerna();
                    defenceFlag = true;
                }

                if (ParryMoveReader("RevCortePerna"))
                {
                    DefRevPerna();
                    defenceFlag = true;
                }

                if (ParryMoveReader("Flanco"))
                {
                    DefFlanco();
                    defenceFlag = true;
                }

                if (ParryMoveReader("RevFlanco"))
                {
                    DefRevFlanco();
                    defenceFlag = true;
                }

                if (ParryMoveReader("Chef"))
                {
                    DefChef();
                    defenceFlag = true;
                }

                if (ParryMoveReader("RevChef"))
                {
                    DefReversoChef();
                    defenceFlag = true;
                }

            }
            else
            {
                defenceFlag = false;
                print("Defence behaviour missed target");
            }    
        }

        private bool OnGuard()
        {
            return defenceFlag && (LayerMask.GetMask("Default") & (1 << target.layer)) > 0;
        }

        bool ParryMoveReader(string atkType)
        {
            return !defenceFlag && (LayerMask.GetMask(atkType) & (1 << target.layer)) > 0;
        }

        void Update()
        {
            

            ParryCaster();
        }

        public void DefTalho()
        {
            animator.SetTrigger("DefTalho");
        }

        public void DefFendente()
        {
            animator.SetTrigger("DefFendente");
        }
        public void DefCortePerna()
        {
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
    }
}