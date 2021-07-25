using RPG.Combat;
using RPG.Core;
using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TPMover : MonoBehaviour//, IAction
{
    bool isRunning = false; bool isAttacking = false;
    Vector3 direction;
    Animator anim;
    Fighter warrior;

    [SerializeField] GameObject target;
    [SerializeField]float rotateSpeed = 2f, fowardSpeed = 1f;

    //ActionScheduler scheduler;
    //[SerializeField] float timeAttackOffset = 3f;
    //[SerializeField] float timeSinceLastAttack = Mathf.Infinity;


    float horizontal;
    float vertical;

    [SerializeField]
    int enemy = 0;

    private void Awake()
    {
        enemy = LayerMask.GetMask("Enemy");
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        warrior = GetComponent<Fighter>(); 
        //scheduler = GetComponent<ActionScheduler>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            anim.SetTrigger("Attack");
        if (Input.GetMouseButtonDown(1))
            anim.SetTrigger("Kick");

        PlayerController_TPC();
        MovementAnimation_TPC();
        MoveOperation_TPC();
        //TargetIsInRange();

        if (target != null /*&& target.GetComponent<Health>().IsDead()*/)
        {
            anim.SetTrigger("OutOfAttack");
            //target = null;
        }

        Fighter_TPC();
    }
    float RunningFactor { get { return isRunning ? 1f : 0.5f; } }
    void PlayerController_TPC()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isAttacking = Input.GetKeyDown(KeyCode.Space);
        direction = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void MovementAnimation_TPC()
    {
        if (horizontal != 0) return;
        anim.SetFloat("fowardSpeed", direction.z * RunningFactor);
    }

    void MoveOperation_TPC()
    {
        transform.Translate(Vector3.forward * direction.z * (RunningFactor * fowardSpeed) * Time.deltaTime);
        transform.Rotate(new Vector3(0, direction.x * rotateSpeed, 0), Space.World);
    }

    private void Fighter_TPC()
    {
        if (target == null) return;

        if (isAttacking)
            Attack_TPC();
    }

    void Attack_TPC()
    {
        //scheduler.StartAction(this);
        anim.SetTrigger("Attack");
        //transform.LookAt(target.transform);
    }

    RaycastHit hit;
    //[SerializeField] float violenceRadius = 5f;
    //void TargetIsInRange()
    //{
    //    if(target == null)
    //    {
            // Cast a sphere wrapping character controller 4 meters forward
            // to see if it is about to hit anything.
            //if (Physics.SphereCast(transform.position, 5, transform.forward, out hit, hit.collider.gameObject.layer))
            //{
            //    print("Thug processing" + target.gameObject.name);
            //    target = hit.collider.gameObject;
            //    //target = hit.collider.gameObject.GetComponent<Health>();
            //}
    //    }

    //}

    void Hit_AnimationEvent()
    {
        if (target != null)
        {
            Debug.Log("Punched");
            //target.TakeDamage(gameObject, 20f);
        }
    }
}
