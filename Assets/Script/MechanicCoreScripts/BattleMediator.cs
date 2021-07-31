using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMediator
{

    public delegate void StartBattleEvent();
    public static event StartBattleEvent onStartGame;



    public delegate void TakeDamageDelegate(float amt);
    public static TakeDamageDelegate onTakeDamage;


    public static void StartGame()
    {
        Debug.Log("StartGame");
        if (onStartGame != null)
            onStartGame();
    }

    public static void TakeDamage(float percent)
    {
        Debug.Log("TakeDamage" + percent);
        if (onTakeDamage != null)
            onTakeDamage(percent);
    }


    // The Mediator interface declares a method used by components to notify the
    // mediator about various events. The Mediator may react to these events and
    // pass the execution to other components.
    public interface IMediator
    {
        void Notify(object sender, string ev);
    }

    // Concrete Mediators implement cooperative behavior by coordinating several
    // components.
    class CombatMediator : IMediator
    {
        private Hostile _hostile;

        private Defence _defence;

        public CombatMediator(Hostile hostile, Defence defence)
        {
            this._hostile = hostile;
            this._hostile.SetMediator(this);
            this._defence = defence;
            this._defence.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "A")
            {
                //print("Mediator reacts on A and triggers folowing operations:");
                this._defence.DoC();
            }
            if (ev == "D")
            {
                //print("Mediator reacts on D and triggers following operations:");
                this._hostile.DoB();
                this._defence.DoC();
            }
        }
    }

    // The Base Component provides the basic functionality of storing a
    // mediator's instance inside component objects.
    public class BaseCombatSystem: MonoBehaviour
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

    // Concrete Components implement various functionality. They don't depend on
    // other components. They also don't depend on any concrete mediator
    // classes.
    class Hostile : BaseCombatSystem
    {
        public void DoA()
        {
            print("Component 1 does A.");

            this._mediator.Notify(this, "A");
        }

        public void DoB()
        {
            print("Component 1 does B.");

            this._mediator.Notify(this, "B");
        }
    }

    class Defence : BaseCombatSystem
    {
        public void DoC()
        {
            print("Component 2 does C.");

            this._mediator.Notify(this, "C");
        }

        public void DoD()
        {
            print("Component 2 does D.");

            this._mediator.Notify(this, "D");
        }
    }

    private void Start()
    {
        // The client code.
        Hostile component1 = new Hostile();
        Defence component2 = new Defence();
        new CombatMediator(component1, component2);

        //print("Client triggets operation A.");
        component1.DoA();

        //Console.WriteLine();

        //print("Client triggers operation D.");
        component2.DoD();
    }
}
