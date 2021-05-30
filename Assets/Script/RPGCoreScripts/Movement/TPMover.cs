using RPG.Combat;
using RPG.Core;
using RPG.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TPMover : MonoBehaviour//, IAction
{
    bool isRunning = false; bool isAttacking = false;
    float RunningFactor { get { return isRunning ? 1f : 0.5f; } }
    Vector3 direction;
    Animator anim;
    Fighter warrior;
    Health target;
    ActionScheduler scheduler;

    float rotateSpeed = 5f, fowardSpeed = 8f;


    void Start()
    {
        anim = GetComponent<Animator>();
        warrior = GetComponent<Fighter>(); 
        scheduler = GetComponent<ActionScheduler>();

    }

    float horizontal;
    float vertical; 
    void PlayerController_TPC()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isAttacking = Input.GetKeyDown(KeyCode.Space);
        direction = new Vector3(horizontal, 0f, vertical).normalized;
    }

    void Animation_TPC()
    {
        if (horizontal != 0) return;
        anim.SetFloat("fowardSpeed", direction.z * RunningFactor);
    }

    void Move_TPC()
    {
        transform.Translate(Vector3.forward * direction.z * (RunningFactor * fowardSpeed) * Time.deltaTime);
        transform.Rotate(new Vector3(0, direction.x * rotateSpeed, 0), Space.World);
    }


    [SerializeField] float timeAttackOffset = 3f;
    [SerializeField] float timeSinceLastAttack = Mathf.Infinity;
    // Update is called once per frame
    void Update()
    {
        PlayerController_TPC();
        Animation_TPC();
        Move_TPC();
        TargetIsInRange();

        if (target != null && target.GetComponent<Health>().IsDead())
        {
            anim.SetTrigger("OutOfAttack");
            target = null;
        }

        Fighter_TPC();
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
    void TargetIsInRange()
    {
        if(target == null)
        {
            // Cast a sphere wrapping character controller 4 meters forward
            // to see if it is about to hit anything.
            if (Physics.SphereCast(transform.position, 2, transform.forward, out hit, violenceRadius))
            {
                if(hit.collider.gameObject.tag == "Thug")
                {
                    target = hit.collider.gameObject.GetComponent<Health>();
                    print("Thug processing" + target.gameObject.name);
                }
            }
        }

    }

    [SerializeField] float violenceRadius = 5f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, violenceRadius);
    }

    void Hit_AnimationEvent()
    {
        if (target != null)
        {
            Debug.Log("Punched");
            target.TakeDamage(gameObject, 20f);
        }
    }
}
