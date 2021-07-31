using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniversalZero.Core
{
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }

    public class BaseCombatSystem : MonoBehaviour
    {
        protected IMediator _mediator;

        public BaseCombatSystem(IMediator mediator = null)
        {
            this._mediator = mediator;
        }

        public void SetMediator(IMediator mediator)
        {
            this._mediator = mediator;
        }
    }

    class CombatMediator : IMediator
    {
        private HostileBehaviour _hostile;

        private DefenceBehaviour _defence;

        public CombatMediator(HostileBehaviour hostile, DefenceBehaviour defence)
        {
            this._hostile = hostile;
            this._hostile.SetMediator(this);
            this._defence = defence;
            this._defence.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "AtkTalho")
            {
                this._defence.SetTarget();
                this._defence.GetComponent<HostileBehaviour>().enabled = false;
                //print("Mediator reacts on A and triggers folowing operations:");
                //this._defence.DoC();
            }
            if (ev == "Default")
            {
                this._defence.CancelDefenceBehaviour();
                this._defence.GetComponent<HostileBehaviour>().enabled = true;
                //print("Mediator reacts on D and triggers following operations:");
                //this._hostile.DoB();
                //this._defence.DoC();
            }
        }
    }

    public class HostileBehaviour : BaseCombatSystem
    {
        Animator animator;
        public GameObject weapon, target;
        
        float lockZAxisRef;
        void Start()
        {
            animator = GetComponent<Animator>();
            lockZAxisRef = transform.position.z;
        }

        private void Update()
        {
            if (!target) return;

            transform.LookAt(target.transform);

            //Lock Foward axis
            gameObject.transform.position = new Vector3(transform.position.x,
                transform.position.y,
                lockZAxisRef);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                CancelHostileBehaviour();
            }

            #region OldInputs
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    TalhoTrigger();
            //}
            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    FendenteTrigger();
            //}
            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    CPernaTrigger();
            //}
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    RevCPernaTrigger();
            //}
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    FlancoTrigger();
            //}
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    RevFlancoTrigger();
            //}
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    ChefTrigger();
            //}
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    RevChefTrigger();
            //}
            #endregion
        }

        GameObject targetTemp;
        public void CancelHostileBehaviour()
        {
            targetTemp = target;
            target = null;
            Trigger("OutOfAttack");
            this._mediator.Notify(this, "Default");

        }

        public void SetTargetPlayer()
        {
            if(gameObject.tag == "Thug")
            {
                target = targetTemp;
            }
        }

        public void StartHostileBehaviour(GameObject t){
            //Debug.LogWarning("Hostile Behaviour calling");

            new CombatMediator(this,t.GetComponent<DefenceBehaviour>());

            target = t;
            Trigger("AtkTalho");
            this._mediator.Notify(this, "AtkTalho");
        }

        #region Broadsword Singing Logic
        public void Trigger(string atk)
        {
            animator.SetTrigger(atk);
        }
        #endregion

        #region Events triggered to set character next anim combo on animation event trigger,   
   
        void Combo1Event(){ 
            Trigger("AtkFendente");
        }

        void Combo2Event(){
            Trigger("AtkCortePerna");
        }

        void Combo3Event(){
            Trigger("AtkRevCortePerna");
        }

        void Combo4Event(){
            Trigger("AtkFlanco");
        }

        void Combo5Event(){
            Trigger("AtkRevFlanco");
        }

        void Combo6Event(){
            Trigger("AtkChef");
        }

        void Combo7Event(){
            Trigger("AtkRevChef");
        }

        void Combo8Event(){
            Trigger("AtkTalho");
        }
        #endregion

        #region events triggered to set character animation 
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
