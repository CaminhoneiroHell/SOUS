using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UniversalZero.Core
{
    public class DefenceBehaviour : BaseCombatSystem
    {
        Animator animator;
        [SerializeField] GameObject target;
        [SerializeField] GameObject weapon;
        [SerializeField] GameObject puppet;

        [SerializeField] float distanceDebug;

        float lockZAxisRef;

        public int stamina = 2;

        void Start()
        {
            animator = GetComponent<Animator>();
            lockZAxisRef = transform.position.z;
        }

        [SerializeField] bool defenceFlag;
        public void ParryCaster()
        {
            if (stamina > 0)
            {
                //print("Distance: " + (gameObject.transform.position.x - target.transform.position.x));

                gameObject.transform.position = new Vector3(transform.position.x,
                                                            transform.position.y,
                                                            lockZAxisRef);


                if (OnGuard()) defenceFlag = false;
                
                if (ParryMoveReader("Talho")){

                    //Must move out of here
                    if(gameObject.tag == "Thug")
                    {
                        GetComponent<RPG.Control.AIController>().enabled = false;
                    }

                    GetComponent<RPG.Combat.Fighter>().enabled = false;
                    GetComponent<RPG.Movement.Mover>().enabled = false;
                    GetComponent<NavMeshAgent>().enabled = false;

                    foreach (RootMotion.Dynamics.Muscle m in puppet.GetComponent<RootMotion.Dynamics.PuppetMaster>().muscles)
                    {
                        m.props.pinWeight = 1f;
                        m.props.muscleWeight = 1f;
                        m.props.muscleDamper = 1f;
                    }

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

        public void SetTarget()
        {
            switch (gameObject.tag)
            {
                case "Player":
                    target = GameObject.FindGameObjectWithTag("Thug");
                    break;
                case "Thug":
                    target = GameObject.FindGameObjectWithTag("Player");
                    break;
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


        public void CancelDefenceBehaviour()
        {
            target = null;
            if (gameObject.tag == "Thug")
            {
                GetComponent<RPG.Control.AIController>().enabled = true;
            }

            GetComponent<RPG.Combat.Fighter>().enabled = true;
            GetComponent<RPG.Movement.Mover>().enabled = true;
            GetComponent<NavMeshAgent>().enabled = true;
        }

        void Update()
        {
            if (target == null) 
                return; 
            transform.LookAt(target.transform);
            ParryCaster();



            //distanceDebug = (gameObject.transform.position.x - target.transform.position.x);
            //if (distanceDebug < 2f)
            //transform.Translate(new Vector3(transform.position.x, transform.position.y, transform.forward.z * backWardMoveDistance * Time.deltaTime), Space.World);

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